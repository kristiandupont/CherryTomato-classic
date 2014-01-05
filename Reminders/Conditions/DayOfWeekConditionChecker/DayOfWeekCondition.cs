using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.DayOfWeekConditionChecker
{
    public class DayOfWeekCondition : Condition
    {
        private HashSet<DayOfWeek> days = new HashSet<DayOfWeek>();

        public IEnumerable<DayOfWeek> Days
        {
            get { return this.days; }
            set { this.days = new HashSet<DayOfWeek>(value); }
        }

        public DayOfWeekCondition()
        {
            var day = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            for (int i = 0; i < 5; i++)
            {
                this.days.Add(day);
                day = (DayOfWeek)(((int)day + 1) % 7);
            }
        }
    }
}
