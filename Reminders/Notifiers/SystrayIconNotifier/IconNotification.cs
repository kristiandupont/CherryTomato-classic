using CherryTomato.Reminders.Core.Notifications;

namespace CherryTomato.Reminders.SystrayIconNotifier
{
    public class IconNotification : Notification
    {
        public override string TypeName { get { return "IconNotification"; } }
        public string NotificationText { get; set; }
        public string FlashIconPath { get; set; }
        public int FlashCount { get; set; }

        public IconNotification()
        {
            this.Enabled = true;
            this.FlashCount = 10;
            this.FlashIconPath = "res://blue.ico";
        }
    }
}
