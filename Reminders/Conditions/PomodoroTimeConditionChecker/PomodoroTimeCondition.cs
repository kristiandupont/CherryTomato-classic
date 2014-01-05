using System;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.PomodoroTimeConditionChecker
{
    /// <summary>
    /// Should notify me if more/less than X minutes passed since last pomodoro Start/Finish.
    /// </summary>
    public class PomodoroTimeCondition : Condition
    {
        private bool more = true;
        public bool More 
        { 
            get
            {
                return this.more;
            }
            set
            {
                this.more = value;
            }
        }

        public bool Less
        {
            get
            {
                return !this.more;
            }
            set
            {
                this.more = !value;
            }
        }

        public TimeSpan Timeout { get; set; }

        public bool AfterPomodoroStarted { get; set; }

        public bool AfterPomodoroCompleted { get; set; }
    }
}
