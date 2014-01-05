using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.Core.Notifications
{
    public interface INotifier : IPlugin
    {
        string NotificationTypeName { get; }
        void Notify(INotification notification);

        void LoadNotificationFromXml(INotification notification, XmlElement fromElement);
        void SaveNotificationToXml(INotification notification, XmlElement parentElement);

        INotification CreateNotification();

        INotificationController GetGuiController();
    }
}
