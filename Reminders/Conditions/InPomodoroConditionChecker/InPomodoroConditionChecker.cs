using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Core.PluginArchitecture;
using System.Xml;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Reminders.InPomodoroConditionChecker
{
    public class InPomodoroConditionChecker : ConditionChecker
    {
        private ICherryCommand isInPomodoroCommand;

        public override bool IsTrue(ICondition condition)
        {
            var c = (InPomodoroCondition)condition;
            return (bool)this.isInPomodoroCommand.Do(null) == c.ShouldBeInPomodoro;
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.isInPomodoroCommand = plugins.CherryCommands["Is In Pomodoro"];
        }

        public override void LoadConditionFromXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (InPomodoroCondition)condition;
            c.ShouldBeInPomodoro = bool.Parse(conditionElement.GetAttribute("shouldBeInPomodoro"));
        }

        public override void SaveConditionToXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (InPomodoroCondition)condition;
            conditionElement.SetAttribute("shouldBeInPomodoro", c.ShouldBeInPomodoro.ToString());
        }

        public override ICondition CreateCondition()
        {
            return new InPomodoroCondition();
        }

        protected override IConditionController CreateGuiControllerInternal()
        {
            return new InPomodoroConditionGuiController();
        }
    }
}
