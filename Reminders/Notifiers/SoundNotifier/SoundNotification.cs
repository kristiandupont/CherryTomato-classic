using CherryTomato.Reminders.Core.Notifications;

namespace CherryTomato.Reminders.SoundNotifier
{
    public class SoundNotification : Notification
    {
        public override string TypeName { get { return "SoundNotification"; } }
        public string SoundPath { get; set; }
    }
}
