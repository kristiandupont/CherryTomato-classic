using System;
using System.Windows.Forms;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;
using CherryTomato.SystrayIcon;

namespace CherryTomato.Reminders.PopupNotifier
{
    public class PopupNotifier : Notifier
    {
        private ICherryCommand showBaloonTip;

        public override void Notify(INotification notification)
        {
            var n = (PopupNotification)notification;

            this.showBaloonTip.Do(new BaloonTipCommandArgs(new BaloonState
            {
                Caption = n.Caption,
                Message = n.NotificationText,
                Icon = n.Icon,
            }));
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.showBaloonTip = plugins.CherryCommands["Show Balloon Tip"];
        }

        protected override INotificationController CreateGuiControllerInternal()
        {
            return new PopupNotificationGuiController();
        }

        public override void SaveNotificationToXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as PopupNotification;
            notificationElement.SetAttribute("notificationText", n.NotificationText);
            notificationElement.SetAttribute("caption", n.Caption);
            notificationElement.SetAttribute("icon", n.Icon.ToString());
        }

        public override void LoadNotificationFromXml(INotification notification, XmlElement notificationElement)
        {
            var n = notification as PopupNotification;
            n.NotificationText = notificationElement.GetAttribute("notificationText");
            n.Caption = notificationElement.GetAttribute("caption");
            n.Icon = (ToolTipIcon)Enum.Parse(typeof(ToolTipIcon), notificationElement.GetAttribute("icon"));
        }

        public override INotification CreateNotification()
        {
            return new PopupNotification();
        }
    }
}
