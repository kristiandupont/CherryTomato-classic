using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Core.Pomodoro
{
    public class PomodoroRegistration
    {
        public DateTime Start { get; set; }

        public DateTime End { get { return this.Start + this.Duration; } }

        public TimeSpan Duration { get; set; }

        public int Rating { get; set; }
    }
}
