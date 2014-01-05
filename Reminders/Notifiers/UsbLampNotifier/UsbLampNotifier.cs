using System.Drawing;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;
using CherryTomato.UsbLamp;

namespace CherryTomato.Reminders.UsbLampNotifier
{
    public class UsbLampNotifier : Notifier
    {
        private ICherryCommand flashUsbLamp;

        public override void Notify(INotification notification)
        {
            var n = (UsbLampNotification)notification;

            var flashState = new FlashingState
            {
                FlashesCount = n.FlashCount,
                FirstSpecialColor = n.FlashColor,
                FirstColor = ColorType.Special,
            };

            this.flashUsbLamp.Do(new FlashUsbLampCommandArgs(flashState));
        }

        protected override INotificationController CreateGuiControllerInternal()
        {
            return new UsbLampNotificationGuiController();
        }

        public override INotification CreateNotification()
        {
            return new UsbLampNotification()
            {
                Enabled = false,
                FlashColor = Color.Red,
                FlashCount = 20
            };
        }

        public override void SaveNotificationToXml(INotification notification, XmlElement notificationElement)
        {
            var n = (UsbLampNotification)notification;
            notificationElement.SetAttribute("flashColor", n.FlashColor.ToKnownColor().ToString());
            notificationElement.SetAttribute("flashCount", n.FlashCount.ToString());
        }

        public override void LoadNotificationFromXml(INotification notification, XmlElement notificationElement)
        {
            var n = (UsbLampNotification)notification;
            n.FlashColor = Color.FromName(notificationElement.GetAttribute("flashColor"));
            n.FlashCount = int.Parse(notificationElement.GetAttribute("flashCount"));
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.flashUsbLamp = plugins.CherryCommands["Flash Usb Lamp"];
        }
    }
}
