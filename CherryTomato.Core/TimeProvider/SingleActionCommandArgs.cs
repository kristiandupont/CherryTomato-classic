using System;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.TimeProvider
{
    public class SingleActionCommandArgs : CherryCommandArgs
    {
        public string ActionName { get; protected set; }

        public DateTime LocalTime { get; protected set; }

        public Action Action { get; protected set; }

        public SingleActionCommandArgs(string actionName, DateTime localTime, Action action)
        {
            this.LocalTime = localTime;
            this.ActionName = actionName;
            this.Action = action;
        }
    }
}
