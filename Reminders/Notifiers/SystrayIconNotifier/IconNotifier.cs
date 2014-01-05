using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;
using CherryTomato.SystrayIcon;

namespace CherryTomato.Reminders.SystrayIconNotifier
{
    public class IconNotifier : Notifier
    {
        private ICherryCommand flashIcon;

        public override void Notify(INotification notification)
        {
            var n = (IconNotification)notification;

            this.flashIcon.Do(new FlashIconCommandArgs(new FlashingState
            {
                FlashesCount = n.FlashCount,
                ToolTipMessage = n.NotificationText,
                FirstIcon = IconType.Idle,
                SecondIcon = IconType.Special,
                SecondSpecialIcon = Helpers.LoadIcon(n.FlashIconPath),
            }));
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.flashIcon = plugins.CherryCommands["Flash Icon"];
        }

        protected override INotificationController CreateGuiControllerInternal()
        {
            return new IconNotificationGuiController();
        }

        public override void SaveNotificationToXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as IconNotification;
            notificationElement.SetAttribute("notificationText", n.NotificationText);
            notificationElement.SetAttribute("flashIconPath", n.FlashIconPath);
            notificationElement.SetAttribute("flashCount", n.FlashCount.ToString());
        }

        public override void LoadNotificationFromXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as IconNotification;
            n.NotificationText = notificationElement.GetAttribute("notificationText");
            n.FlashIconPath = notificationElement.GetAttribute("flashIconPath");
            n.FlashCount = int.Parse(notificationElement.GetAttribute("flashCount"));
        }

        public override INotification CreateNotification()
        {
            return new IconNotification();
        }
    }
}
