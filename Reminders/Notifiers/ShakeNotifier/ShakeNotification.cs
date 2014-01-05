using CherryTomato.Reminders.Core.Notifications;

namespace CherryTomato.Reminders.ShakeNotifier
{
    public class ShakeNotification : Notification
    {
        public override string TypeName { get { return "ShakeNotification"; } }
        public int Timeout { get; set; }
    }
}
