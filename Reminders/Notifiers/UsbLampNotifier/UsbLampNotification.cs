using System.Drawing;
using CherryTomato.Reminders.Core.Notifications;

namespace CherryTomato.Reminders.UsbLampNotifier
{
    public class UsbLampNotification : Notification
    {
        public override string TypeName { get { return "UsbLampNotification"; } }
        public Color FlashColor { get; set; }
        public int FlashCount { get; set; }
    }
}
