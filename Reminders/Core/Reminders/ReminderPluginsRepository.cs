using System.Collections.Generic;
using System.Linq;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Reminders
{
    public class ReminderPluginsRepository : IPlugin, ICherryCommandsProvider
    {
        private Dictionary<string, IReminderPlugin> allReminderPlugins;

        public ReminderPluginsRepository()
        {
            this.getAllReminderPlugins = new CherryCommand(
                "Get All Reminder Plugins",
                ca => this,
                "Returns reminder plugins repository.");
        }

        public IEnumerable<IReminderPlugin> All
        {
            get { return this.allReminderPlugins.Values; }
        }

        public IReminderPlugin GetPlugin(IReminder reminder)
        {
            return this.GetPlugin(reminder.TypeName);
        }

        public IReminderPlugin GetPlugin(string reminderPluginTypeName)
        {
            return this.allReminderPlugins[reminderPluginTypeName];
        }

        public string PluginName
        {
            get { return "Reminder Plugins Repository"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.allReminderPlugins =
                plugins.All.OfType<IReminderPlugin>().ToDictionary(p => p.PluginName);
        }

        private CherryCommand getAllReminderPlugins;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getAllReminderPlugins;
        }
    }
}
