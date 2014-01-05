using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Notifications
{
    public class CompositeNotification
    {
        private Dictionary<string, INotification> allNotifications = new Dictionary<string, INotification>();
        private NotifyPluginsRepository notifiers;

        public INotification GetNotification(string typeName)
        {
            return this.allNotifications[typeName];
        }

        public CompositeNotification(PluginRepository plugins, params INotification[] notifications)
        {
            this.notifiers = plugins.CherryCommands["Get All Notify Plugins"].Do(null) as NotifyPluginsRepository;

            foreach (var n in this.notifiers.All)
            {
                this.allNotifications[n.NotificationTypeName] = n.CreateNotification();
            }

            foreach (var notification in notifications)
            {
                this.allNotifications[notification.TypeName] = notification;
            }
        }

        public void Load(XmlElement compositeNotificationNode)
        {
            if (compositeNotificationNode == null)
            {
                return;
            }

            var notificationNodes = compositeNotificationNode.SelectNodes("notification");

            foreach (XmlElement notificationElement in notificationNodes)
            {
                var typeName = notificationElement.GetAttribute("typeName");
                var notifierPlugin = this.notifiers.GetPlugin(typeName);
                var notification = notifierPlugin.CreateNotification();

                notification.Enabled = bool.Parse(notificationElement.GetAttribute("enabled"));
                notifierPlugin.LoadNotificationFromXml(notification, notificationElement);
                
                this.allNotifications[typeName] = notification;
            }
        }

        public void Save(XmlElement parentElement)
        {
            var compositeNotificationElement = parentElement.OwnerDocument.CreateElement("compositeNotification");
            parentElement.AppendChild(compositeNotificationElement);

            foreach (var notificationType in this.allNotifications.Keys)
            {
                var notifierPlugin = this.notifiers.GetPlugin(notificationType);
                var notificationElement = compositeNotificationElement.OwnerDocument.CreateElement("notification");
                notificationElement.SetAttribute("typeName", notifierPlugin.NotificationTypeName);
                var notification = this.allNotifications[notificationType];

                notificationElement.SetAttribute("enabled", notification.Enabled.ToString());
                notifierPlugin.SaveNotificationToXml(notification, notificationElement);

                compositeNotificationElement.AppendChild(notificationElement);
            }
        }

        public void Notify()
        {
            foreach (var notification in this.allNotifications.Values.Where(n => n.Enabled))
            {
                var notificationTypeName = notification.TypeName;
                this.notifiers.GetPlugin(notificationTypeName).Notify(notification);
            }
        }

        public void AddNotification(INotification notification)
        {
            // Replace existing notification with the new one
            this.allNotifications[notification.TypeName] = notification;
        }
    }
}
