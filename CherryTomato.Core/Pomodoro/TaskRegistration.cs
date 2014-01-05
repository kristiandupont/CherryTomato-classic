using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CherryTomato.Core.Pomodoro
{
    [DebuggerDisplay("{ProcessName} {TaskName} {TimeStamp} {Duration}")]
    public class TaskRegistration
    {
        public string TaskName { get; set; }

        public string ProcessName { get; set; }

        public DateTime TimeStamp { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
