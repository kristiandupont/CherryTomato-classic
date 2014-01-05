using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.Core.Reminders
{
    public interface IReminderPlugin : IPlugin
    {
        IReminder LoadReminder(XmlElement fromElement);
        void SaveReminder(IReminder reminder, XmlElement parentElement);

        IReminderController GetGuiController();
        IReminder CreateDefaultReminder(PluginRepository pluginRepository);
        IReminder CreateEmptyReminder();
    }
}
