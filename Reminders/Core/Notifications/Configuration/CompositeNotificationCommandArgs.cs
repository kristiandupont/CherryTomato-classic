using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Reminders.Core.Notifications.Configuration
{
    public class CompositeNotificationCommandArgs : CherryCommandArgs
    {
        public CompositeNotification CompositeNotification { get; protected set; }

        public CompositeNotificationCommandArgs(CompositeNotification compositNotification)
        {
            this.CompositeNotification = compositNotification;
        }
    }
}
