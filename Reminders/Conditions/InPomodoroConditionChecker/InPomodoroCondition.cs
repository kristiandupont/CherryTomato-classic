using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.InPomodoroConditionChecker
{
    public class InPomodoroCondition : Condition
    {
        public bool ShouldBeInPomodoro { get; set; }

        public InPomodoroCondition()
        {
            this.Enabled = true;
        }
    }
}
