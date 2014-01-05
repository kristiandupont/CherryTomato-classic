using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.Tests
{
    public class FakeReminderPlugin : IReminderPlugin
    {
        public string PluginName { get { return "FakeReminder"; } }

        public IReminder LoadReminder(PluginRepository pluginRepository, XmlElement fromElement)
        {
            var result = new FakeReminder(pluginRepository);
            return result;
        }

        public void SaveReminder(IReminder reminder, XmlElement parentElement)
        {
        }

        public IReminder CreateDefaultReminder(PluginRepository pluginRepository)
        {
            return new FakeReminder(pluginRepository);
        }

        private PluginRepository plugins;

        public void TieEvents(PluginRepository plugins)
        {
            this.plugins = plugins;
        }

        public IReminder LoadReminder(XmlElement fromElement)
        {
            return new FakeReminder(this.plugins);
        }

        public IReminderController GetGuiController()
        {
            return null;
        }


        public IReminder CreateEmptyReminder()
        {
            throw new System.NotImplementedException();
        }
    }
}
