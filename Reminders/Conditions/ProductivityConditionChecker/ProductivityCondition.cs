using System;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.ProductivityConditionChecker
{
    /// <summary>
    /// How many pomodoros done within last period of time?
    /// </summary>
    public class ProductivityCondition : Condition
    {
        public int Pomodoros { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
