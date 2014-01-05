using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Reminders.Core.Reminders
{
    public class ReminderCommandArgs : CherryCommandArgs
    {
        public IReminder Reminder { get; protected set; }

        public ReminderCommandArgs(IReminder reminder)
        {
            this.Reminder = reminder;
        }
    }
}
