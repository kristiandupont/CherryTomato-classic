using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.TimeOfDayConditionChecker
{
    public class TimeOfDayCondition : Condition
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool ShouldBeWithin { get; set; }

        public TimeOfDayCondition()
        {
            this.ShouldBeWithin = true;
            this.StartTime = TimeSpan.FromHours(8);
            this.EndTime = TimeSpan.FromHours(18);
        }
    }
}
