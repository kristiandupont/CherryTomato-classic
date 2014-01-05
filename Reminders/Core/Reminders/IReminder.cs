using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.Core.Reminders
{
    public interface IReminder
    {
        string Name { get; set; }

        string Description { get; set; }

        string TypeName { get; }

        bool Enabled { get; set; }

        CompositeNotification CompositeNotification { get; set; }

        CompositeCondition CompositeCondition { get; set; }

        void Schedule();

        bool Unschedule();

        void Notify();
    }
}
