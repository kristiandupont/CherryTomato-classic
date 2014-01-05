using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.Pomodoro
{
    public class ProductivityCommandArgs : CherryCommandArgs
    {
        public DateTime SinceTime { get; protected set; }

        public bool CountPartialPomodoros { get; protected set; }

        public ProductivityCommandArgs(DateTime since, bool countPartialPomodoros = false)
        {
            this.SinceTime = since;
            this.CountPartialPomodoros = countPartialPomodoros;
        }
    }
}
