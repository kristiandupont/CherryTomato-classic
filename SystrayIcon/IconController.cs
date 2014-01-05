using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.WindowsController;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.SystrayIcon
{
    public partial class IconController : 
        IPlugin, IDisposable, ICherryCommandsProvider, ICherryEventsProvider
    {
        private const string InPomodoroTextTemplate = "In pomodoro. {0} left";
        private const string OutOfPomoTextTemplate = "Out of pomodoro for {0}";
        private const string ProductivityTodayTemplate = "\nProductivity today: {0} ({1} ps)";
        
        public IconController()
        {
            this.flasher = new Flasher(this);
            this.renderer = new Renderer(this);

            this.flashIconCommand = new CherryCommand(
                "Flash Icon",
                fica => this.StartFlashing((fica as FlashIconCommandArgs).FlashingSettings),
                "Start icon flashing. The flashing settings should be set with command arguments.");

            this.showBaloonCommand = new CherryCommand(
                "Show Balloon Tip",
                btca => this.renderer.ShowBalloonTip((btca as BaloonTipCommandArgs).BaloonSettings),
                "Shows standard System Tray balloon notification. The information about balloon should be set with command arguments.");
        }

        public string PluginName { get { return "Icon Controller"; } }

        public bool ShowTimeInTray { get { return true; } }   // For now, this is always enabled. Should be configurable

        private const string StartPomodoroMenuText = "Start &Pomodoro";
        private const string VoidPomodoroMenuText = "&Void Pomodoro";

        private ToolStripMenuItem startOrVoidPomodoroIconMenuItem;
        private ToolStripMenuItem settingsIconMenuItem;

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.setActiveWindowForegroundCommand == null)
            {
                // The plugin still not initialized. Just forget about this click.
                return;
            }

            if ((bool)this.setActiveWindowForegroundCommand.Do(null))
            {
                // The active windows was set foreground, thus don't do anything else.
                return;
            }

            if (this.flasher.IsFlashing)
            {
                this.StopFlashing();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                this.leftButtonClickedEvent.Fire(null);
                return;
            }
        }

        private void PomodoroMenuClicked()
        {
            if (!(bool)this.isInPomodoroCommand.Do(null))
            {
                this.startPomodoroCommand.Do(null);
            }
            else
            {
                this.voidPomodoroCommand.Do(null);
            }
        }

        public void Dispose()
        {
            this.renderer.Dispose();
        }

        private void InitializeMenuItems()
        {
            this.startOrVoidPomodoroIconMenuItem =
                new ToolStripMenuItem(StartPomodoroMenuText, null, (a, b) => this.PomodoroMenuClicked());
            this.settingsIconMenuItem = new ToolStripMenuItem("&Settings...", null, (a, b) => this.showSettingsCommand.Do(null));

            var items = this.renderer.ContextMenuStrip.Items;
            items.Add(this.startOrVoidPomodoroIconMenuItem);
            items.Add(new ToolStripSeparator());
            items.Add(this.settingsIconMenuItem);
            items.Add(new ToolStripMenuItem("E&xit", null, (a, b) => Application.Exit()));

            var eventArgs = new IconControllerEventArgs();
            this.collectMenuItemsEvent.Fire(eventArgs);
            this.renderer.ContextMenuStrip.Items.AddRange(eventArgs.MenuItems.ToArray());
        }

        public void StartPomodoroInternal()
        {
            this.renderer.SetIconType(IconType.Pomodoro);

            this.renderer.ContextMenuStrip.UpdateControl(delegate
            {
                if (this.startOrVoidPomodoroIconMenuItem != null)
                {
                    this.startOrVoidPomodoroIconMenuItem.Text = VoidPomodoroMenuText;
                }

                if (this.settingsIconMenuItem != null)
                {
                    this.settingsIconMenuItem.Enabled = false;
                }
            });
        }

        public void UpdateToolTipTextMessage()
        {
            this.renderer.UpdateToolTipTextMessage();
        }

        public string ToolTipText
        {
            get
            {
                return this.renderer.ToolTipText;
            }
        }

        private bool StartFlashing(FlashingState flashingData)
        {
            return this.flasher.StartFlashing(flashingData);
        }

        public void StopFlashing()
        {
            this.flasher.StopFlashing();
        }

        public void StopPomodoroInternal(bool completed)
        {
            if (completed)  // Finished pomodoro
            {
                var flashingData = new FlashingState
                {
                    FlashesCount = 20,
                    ToolTipMessage = "Pomodoro completed",
                    FirstIcon = IconType.Idle,
                    SecondIcon = IconType.Pomodoro
                };
                this.StartFlashing(flashingData);
            }
            else
            {
                this.renderer.SetIconType(IconType.Idle);
            }

            this.renderer.ContextMenuStrip.UpdateControl(delegate
            {
                if (this.startOrVoidPomodoroIconMenuItem != null)
                {
                    this.startOrVoidPomodoroIconMenuItem.Text = StartPomodoroMenuText;
                }

                if (this.settingsIconMenuItem != null)
                {
                    this.settingsIconMenuItem.Enabled = true;
                }
            });
        }

        private ICherryCommand getProductivityCommand;
        private ICherryCommand getCurrentPomodoroCommand;
        private ICherryCommand isInPomodoroCommand;
        private ICherryCommand startPomodoroCommand;
        private ICherryCommand voidPomodoroCommand;
        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;
        private ICherryCommand setActiveWindowForegroundCommand;
        private ICherryCommand showSettingsCommand;

        public void TieEvents(PluginRepository plugins)
        {
            this.getProductivityCommand = plugins.CherryCommands["Get Today Productivity"];
            this.getCurrentPomodoroCommand = plugins.CherryCommands["Get Current Pomodoro Status Data"];
            this.isInPomodoroCommand = plugins.CherryCommands["Is In Pomodoro"];
            this.startPomodoroCommand = plugins.CherryCommands["Start Pomodoro"];
            this.voidPomodoroCommand = plugins.CherryCommands["Void Pomodoro"];
            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];
            this.setActiveWindowForegroundCommand = plugins.CherryCommands["Set Active Window Foreground"];
            this.showSettingsCommand = plugins.CherryCommands["Show Settings"];

            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finished",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData.Successful)));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Active Window Changed",
                wea =>
                {
                    // Disable context menu when a window is shown.
                    this.renderer.ContextMenuEnabled = (wea as WindowEventArgs).Window == null;
                }));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Application Started",
                ea =>
                {
                    this.renderer.CheckSystrayIconCreated();
                }), 
                false);

            // update minutes drawn on the pomodoro icon
            plugins.CherryEvents.Subscribe("Pomodoro Minute Elapsed", this.renderer.UpdateIconTime);
        }

        private CherryCommand flashIconCommand;
        private CherryCommand showBaloonCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.flashIconCommand;
            yield return this.showBaloonCommand;
        }

        public CherryEvent collectMenuItemsEvent = new CherryEvent(
            "Collect Menu Items",
            "Fired on icon initialization. Collects additional context menu items of the System Tray icon. Use the event to add more menus.");
        public CherryEvent leftButtonClickedEvent = new CherryEvent(
            "Icon Left Button Clicked",
            "Fired on left mouse click at the System Tray icon.");

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.collectMenuItemsEvent;
            yield return this.leftButtonClickedEvent;
        }
    }
}
