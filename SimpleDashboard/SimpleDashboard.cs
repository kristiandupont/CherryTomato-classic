using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.Database;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.TimeProvider;
using Quartz;

namespace CherryTomato.SimpleDashboard
{
    public class SimpleDashboard : IPlugin, IDisposable
    {
        private DatabaseConnection databaseConnection;

        private SimpleDashForm form = new SimpleDashForm();

        public Dictionary<DateTime, PomodorosProductivity> productivityIndices = new Dictionary<DateTime, PomodorosProductivity>();

        // If you have TestDriven.Net, you can just right-click inside this function and select Run Test in order to test the dashboard.
        public void Test()
        {
            var day = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            var r = new Random();
            while (day <= DateTime.Today)
            {
                var pc = r.Next(8) + 1;

                if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (r.Next(6) == 0)
                        pc = r.Next(2) + 1;
                    else  // Most weekends we didn't do any work..
                        pc = 0;
                        
                }
                var pi = pc * (r.Next(10) + 1);

                productivityIndices[day] = new PomodorosProductivity(pc, pi);
                day = day.AddDays(1);
            }

            this.form.SetProductivityIndices(productivityIndices);
            this.form.statusLabel.Text = "Out of pomodoro for 3 minutes";
            this.form.ShowInTaskbar = true;
            this.form.ControlBox = true; // Add the close button so we can stop the test..
            this.form.ShowDialog();
        }

        public void ConnectToDatabase(DatabaseConnection newDatabaseConnection)
        {
            databaseConnection = newDatabaseConnection;

            var today = (DateTime)this.getCurrentTime.Do(null);
            var twoMonthsAgo = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            var recentPomodoros = databaseConnection.ExecuteReader(
                "select * from PomodoroRegistrations where TimeStamp >= @p1", 
                twoMonthsAgo);
            foreach (DataRow r in recentPomodoros.Rows)
            {
                var date = Convert.ToDateTime(r["TimeStamp"]).Date;
                if (!productivityIndices.ContainsKey(date))
                    productivityIndices[date] = new PomodorosProductivity();

                productivityIndices[date].Pomodoros++;
                var rating = Convert.ToInt32(r["Evaluation"]);
                productivityIndices[date].Rating += rating;
            }

            this.form.SetProductivityIndices(productivityIndices);
        }

        public void SetPomodorosToday(PomodorosProductivity productivity)
        {
            var today = DateTime.Today;

            if (!productivityIndices.ContainsKey(today))
                productivityIndices[today] = new PomodorosProductivity();

            productivityIndices[today].Pomodoros = productivity.Pomodoros;
            productivityIndices[today].Rating = productivity.Rating;
            this.form.UpdateControl(() => { if (this.form.Handle != IntPtr.Zero) this.form.Invalidate(true); });
        }

        private const string InPomodoroTextTemplate = "In pomodoro. {0} left";
        private const string OutOfPomoTextTemplate = "Out of pomodoro for {0}";

        private ICherryCommand getCurrentPomodoroStatus;
        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;
        private ICherryCommand getCurrentTime;

        public RunningPomodoro GetCurrentlyRunningPomodoroData()
        {
            return (RunningPomodoro)this.getCurrentPomodoroStatus.Do(null);
        }

        public bool Enabled 
        {
            get { return this.form.Visible; }
            set
            {
                if (this.form.Visible != value)
                {
                    if (value)
                    {
                        this.UpdateVisual();
                        this.UnsubscribeUpdater(); // just in case...
                        this.SubscribeUpdater();
                    }
                    else
                    {
                        this.UnsubscribeUpdater();
                    }
                }

                this.form.Visible = value;
            }
        }

        private void UpdateVisual()
        {
            var pomodoroData = this.GetCurrentlyRunningPomodoroData();
            string text;
            if (pomodoroData == null)
            {
                text = "Start a pomodoro!";
            }
            else if (pomodoroData.IsInPomodoro)
            {
                text = string.Format(InPomodoroTextTemplate, pomodoroData.Remaining.ToAgeString(true));
            }
            else
            {
                text = string.Format(OutOfPomoTextTemplate, pomodoroData.OutOfPomodoro.ToAgeString(false));
            }

            this.form.SetStatusLabelText(text);
        }

        private void SubscribeUpdater()
        {
            this.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                "Second Elapsed for Simple Dashboard",
                TriggerUtils.MakeSecondlyTrigger(), // fired on every second
                this.UpdateVisual)); // refresh the picture of the dashboard
        }

        private void UnsubscribeUpdater()
        {
            // remove the trigger as of no need
            this.removeExistingTriggerCommand.Do(new TimeTriggerCommandArgs("Second Elapsed for Simple Dashboard"));
        }

        public bool ShowInTaskBar
        {
            get { return this.showInTaskBar; }
            set 
            {
                this.showInTaskBar = value;
                this.form.ShowInTaskbar = value;

                if (value) this.form.BringToFront();    // If show in task bar, activate the window to make it happen
            }
        }
        private bool showInTaskBar;

        public bool AlwaysOnTop
        {
            get { return this.alwaysOnTop; }
            set 
            {
                this.alwaysOnTop = value;
                this.form.TopMost = value;
            }
        }
        private bool alwaysOnTop = true;

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                this.Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
                this.ShowInTaskBar = bool.Parse(fromElement.GetAttribute("showInTaskBar"));
                this.AlwaysOnTop = bool.Parse(fromElement.GetAttribute("alwaysOnTop"));
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);

            pluginElement.SetAttribute("enabled", this.Enabled.ToString());
            pluginElement.SetAttribute("showInTaskBar", this.showInTaskBar.ToString());
            pluginElement.SetAttribute("alwaysOnTop", this.alwaysOnTop.ToString());
        }

        public void Dispose()
        {
            this.form.UpdateControl(this.form.Close);
        }

        public string PluginName { get { return "SimpleDashboard"; } }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Number of Pomodoro Changed", 
                ea => this.SetPomodorosToday((ea as PomodoroEventArgs).ProductivityData)));

            plugins.CherryEvents.Subscribe("Icon Left Button Clicked", () => this.Enabled = !this.Enabled);
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Connect To Database Event",
                ea => this.ConnectToDatabase((ea as ConnectToDbEventArgs).DbConnection)));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect GUI Configurable Plugins",
                ea =>
                {
                    var info = new GuiConfigurablePluginInfo(
                        this.PluginName,
                        new SimpleDashSettingsPanel(this),
                        Helpers.LoadIcon("res://dashboard.ico"));
                    (ea as GuiConfigurablePluginEventArgs).PluginInfos.Add(info);
                }));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Plugin Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Plugin Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigurePluginEventArgs)));

            this.getCurrentPomodoroStatus = plugins.CherryCommands["Get Current Pomodoro Status Data"];
            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];
            this.getCurrentTime = plugins.CherryCommands["Get Current Time"];
        }
    }
}
