using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.Core.Notifications
{
    public abstract class Notifier : INotifier
    {
        private INotificationController guiController;

        public string NotificationTypeName
        {
            get
            {
                return this.CreateNotification().TypeName;
            }
        }

        public string PluginName
        {
            get
            {
                return this.NotificationTypeName;
            }
        }

        public abstract void Notify(INotification notification);

        public abstract void TieEvents(PluginRepository plugins);

        public abstract INotification CreateNotification();

        public abstract void SaveNotificationToXml(INotification notification, XmlElement notificationElement);

        public abstract void LoadNotificationFromXml(INotification notification, XmlElement notificationElement);

        protected abstract INotificationController CreateGuiControllerInternal();

        public INotificationController GetGuiController()
        {
            return this.guiController ?? (this.guiController = this.CreateGuiControllerInternal());
        }
    }
}
