using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Core.Pomodoro
{
    public class CompletedPomodoro : PomodoroRegistration
    {
        public List<TaskRegistration> TaskRegistrations { get; set; }

        public List<int> KeyboardActivity { get; set; }

        public List<int> MouseActivity { get; set; }

        /// <summary>
        /// True only when all the pomodoro duration passed by (disregarding if it was voided or approved). 
        /// False when the pomodoro was interupted (voided).
        /// </summary>
        public bool Successful { get; set; }

        public CompletedPomodoro()
        {
            this.Rating = 5;
        }
    }
}
