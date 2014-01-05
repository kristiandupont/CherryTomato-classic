using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.ShakeNotifier
{
    public class ShakeNotifier : Notifier
    {
        public override void Notify(INotification notification)
        {
            var n = (ShakeNotification)notification;

            new WindowShaker(new ShakeState()
            {
                Timeout = n.Timeout,
            }).Shake();
        }

        public override void TieEvents(PluginRepository plugins)
        {
        }

        protected override INotificationController CreateGuiControllerInternal()
        {
            return new ShakeNotificationGuiController();
        }

        public override void SaveNotificationToXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as ShakeNotification;
            notificationElement.SetAttribute("timeout", n.Timeout.ToString());
        }

        public override void LoadNotificationFromXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as ShakeNotification;
            n.Timeout = int.Parse(notificationElement.GetAttribute("timeout"));
        }

        public override INotification CreateNotification()
        {
            return new ShakeNotification() { Timeout = 2000 };
        }
    }
}
