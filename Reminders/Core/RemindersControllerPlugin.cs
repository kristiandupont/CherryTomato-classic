using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders;

namespace CherryTomato.Reminders.Core
{
    public class RemindersCorePlugin : IPlugin, ICherryCommandsProvider
    {
        public RemindersCorePlugin()
        {
            this.getAllRemindersCommand = new CherryCommand(
                "Get All Reminders",
                ca => this.allReminders,
                "Returns all scheduled reminder objects as IEnumerable.");
            this.addNewRemiderCommand = new CherryCommand(
                "Add New Reminder",
                ca => this.AddReminder((ca as ReminderCommandArgs).Reminder),
                "Add one more reminder object to reminders list.");
            this.removeExistingReminderCommand = new CherryCommand(
                "Remove Existing Reminder",
                ca => this.RemoveReminder((ca as ReminderCommandArgs).Reminder),
                "Remove one of the reminders from the reminders list.");
        }

        private bool RemoveReminder(IReminder reminder)
        {
            if (this.allReminders.Remove(reminder))
            {
                reminder.Unschedule();
            }

            return false;
        }

        public bool AddReminder(IReminder reminder)
        {
            if (!this.allReminders.Add(reminder))
            {
                return false;
            }

            if (reminder.Enabled)
            {
                reminder.Schedule();
            }

            return true;
        }

        public string PluginName
        {
            get { return "Reminders"; }
        }

        private CherryCommand getAllRemindersCommand;
        private CherryCommand addNewRemiderCommand;
        private CherryCommand removeExistingReminderCommand;

        private ReminderPluginsRepository allReminderPlugins;
        private ReminderPluginsRepository AllReminderPlugins
        {
            get
            {
                if (this.allReminderPlugins == null)
                {
                    this.allReminderPlugins = this.getAllReminderPluginsCommand.Do(null) as ReminderPluginsRepository;
                }

                return this.allReminderPlugins;
            }
        }

        private HashSet<IReminder> allReminders = new HashSet<IReminder>();

        private ICherryCommand getAllReminderPluginsCommand;

        public void TieEvents(PluginRepository plugins)
        {
            this.getAllReminderPluginsCommand = plugins.CherryCommands["Get All Reminder Plugins"];

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                cea => this.LoadConfig(cea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                cea => this.SaveConfig(cea as ConfigureEventArgs)));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect GUI Configurable Plugins",
                ea =>
                {
                    var info = new GuiConfigurablePluginInfo(
                        this.PluginName,
                        plugins.CherryCommands["Get Reminders List Panel"].Do(null) as Control,
                        Helpers.LoadIcon("res://reminders.ico"));
                    (ea as GuiConfigurablePluginEventArgs).PluginInfos.Add(info);
                }),
                false);
        }

        private void SaveConfig(ConfigureEventArgs cea)
        {
            if (this.allReminders.Count != 0)
            {
                var configNode = cea.ConfigNode;
                var remindersNode = cea.Document.CreateElement("reminders");
                configNode.AppendChild(remindersNode);

                foreach (var reminder in allReminders)
                {
                    this.AllReminderPlugins.GetPlugin(reminder).SaveReminder(reminder, remindersNode);
                }
            }
        }

        private void LoadConfig(ConfigureEventArgs cea)
        {
            var reminderNodes = cea.ConfigNode.SelectNodes("reminders/reminder");
            foreach (XmlElement reminderNode in reminderNodes)
            {
                var typeName = reminderNode.GetAttribute("typeName");
                var plugin = this.AllReminderPlugins.GetPlugin(typeName);
                if (plugin == null)
                {
                    Trace.WriteLine("Plugin for reminders of type '" + typeName + "' is not registered.");
                    continue;
                }

                var reminder = plugin.LoadReminder(reminderNode);
                this.AddReminder(reminder);
            }
        }

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getAllRemindersCommand;
            yield return this.addNewRemiderCommand;
            yield return this.removeExistingReminderCommand;
        }
    }
}
