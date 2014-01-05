using System;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.PomodoroTimeConditionChecker
{
    public class PomodoroTimeConditionChecker : ConditionChecker
    {
        private ICherryCommand getPomodoroStatus;

        public override bool IsTrue(ICondition condition)
        {
            var c = (PomodoroTimeCondition)condition;
            var pomodoroStatus = (RunningPomodoro)this.getPomodoroStatus.Do(null);

            var measureTimeSpan =
                c.AfterPomodoroStarted ? pomodoroStatus.Elapsed :
                c.AfterPomodoroCompleted ? pomodoroStatus.OutOfPomodoro :
                default(TimeSpan);
            if (c.More)
            {
                return measureTimeSpan > c.Timeout;
            }
            else
            {
                return measureTimeSpan < c.Timeout;
            }

            throw new PluginException("Internal error. PomodoroTimeCondition contains broken data.");
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.getPomodoroStatus = plugins.CherryCommands["Get Current Pomodoro Status Data"];
        }

        public override void LoadConditionFromXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (PomodoroTimeCondition)condition;
            c.More = bool.Parse(conditionElement.GetAttribute("moreOrLess"));
            c.Timeout = TimeSpan.Parse(conditionElement.GetAttribute("timeout"));
            c.AfterPomodoroStarted = bool.Parse(conditionElement.GetAttribute("afterPomodoroStarted"));
            c.AfterPomodoroCompleted = bool.Parse(conditionElement.GetAttribute("afterPomodoroCompleted"));
        }

        public override void SaveConditionToXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (PomodoroTimeCondition)condition;
            conditionElement.SetAttribute("moreOrLess", c.More.ToString());
            conditionElement.SetAttribute("timeout", c.Timeout.ToString());
            conditionElement.SetAttribute("afterPomodoroStarted", c.AfterPomodoroStarted.ToString());
            conditionElement.SetAttribute("afterPomodoroCompleted", c.AfterPomodoroCompleted.ToString());
        }

        public override ICondition CreateCondition()
        {
            return new PomodoroTimeCondition();
        }

        protected override IConditionController CreateGuiControllerInternal()
        {
            return new PomodoroTimeConditionGuiController();
        }
    }
}
