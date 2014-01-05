
namespace CherryTomato.Reminders.Core.Notifications
{
    public abstract class Notification : INotification
    {
        public abstract string TypeName { get; }

        public virtual bool Enabled { get; set; }
    }
}
