using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions;
using System.Xml;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.Reminders.ProductivityConditionChecker
{
    public class ProductivityConditionChecker : ConditionChecker
    {
        private ICherryCommand getProductivity;
        private ICherryCommand now;

        public override bool IsTrue(ICondition condition)
        {
            var c = (ProductivityCondition)condition;
            var since = ((DateTime)this.now.Do(null)) - c.Duration;
            var productivity = this.getProductivity.Do(new ProductivityCommandArgs(since)) as PomodorosProductivity;
            return productivity.Pomodoros < c.Pomodoros;
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.getProductivity = plugins.CherryCommands["Get Productivity"];
            this.now = plugins.CherryCommands["Get Current Time"];
        }

        public override void LoadConditionFromXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (ProductivityCondition)condition;
            c.Pomodoros = int.Parse(conditionElement.GetAttribute("pomodoros"));
            c.Duration = TimeSpan.Parse(conditionElement.GetAttribute("duration"));
        }

        public override void SaveConditionToXml(ICondition condition, XmlElement conditionElement)
        {
            var c = (ProductivityCondition)condition;
            conditionElement.SetAttribute("pomodoros", c.Pomodoros.ToString());
            conditionElement.SetAttribute("duration", c.Duration.ToString());
        }

        public override ICondition CreateCondition()
        {
            return new ProductivityCondition()
            {
                Duration = TimeSpan.FromHours(1),
                Pomodoros = 1,
            };
        }

        protected override IConditionController CreateGuiControllerInternal()
        {
            return new ProductivityConditionGuiController();
        }
    }
}
