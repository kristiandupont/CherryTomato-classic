using System;
using CherryTomato.Core.CommandsModel;
using Quartz;

namespace CherryTomato.Core.TimeProvider
{
    public class TimeTriggerCommandArgs : CherryCommandArgs
    {
        public string ActionName { get; protected set; }

        public Trigger TimeTrigger { get; protected set; }

        public Action Action { get; protected set; }

        public TimeTriggerCommandArgs(string jobName)
        {
            this.ActionName = jobName;
        }

        public TimeTriggerCommandArgs(string jobName, Trigger timeTrigger, Action action)
        {
            this.ActionName = jobName;
            this.TimeTrigger = timeTrigger;
            this.Action = action;
        }
    }
}
