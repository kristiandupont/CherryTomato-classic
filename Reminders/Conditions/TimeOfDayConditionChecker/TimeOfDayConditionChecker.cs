using System;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.TimeOfDayConditionChecker
{
    public class TimeOfDayConditionChecker : ConditionChecker
    {
        private ICherryCommand now;

        public override bool IsTrue(ICondition condition)
        {
            var c = condition as TimeOfDayCondition;
            var currentTime = ((DateTime)this.now.Do(null)).TimeOfDay;
            return c.ShouldBeWithin ?
                c.StartTime <= currentTime && currentTime <= c.EndTime :
                currentTime <= c.StartTime || currentTime >= c.EndTime;
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.now = plugins.CherryCommands["Get Current Time"];
        }

        public override void LoadConditionFromXml(ICondition condition, XmlElement conditionElement)
        {
            var c = condition as TimeOfDayCondition;
            c.StartTime = TimeSpan.Parse(conditionElement.GetAttribute("start"));
            c.EndTime = TimeSpan.Parse(conditionElement.GetAttribute("end"));
            c.ShouldBeWithin = bool.Parse(conditionElement.GetAttribute("shouldBeWithin"));
        }

        public override void SaveConditionToXml(ICondition condition, XmlElement conditionElement)
        {
            var c = condition as TimeOfDayCondition;
            conditionElement.SetAttribute("start", c.StartTime.ToString());
            conditionElement.SetAttribute("end", c.EndTime.ToString());
            conditionElement.SetAttribute("shouldBeWithin", c.ShouldBeWithin.ToString());
        }

        public override ICondition CreateCondition()
        {
            return new TimeOfDayCondition();
        }

        protected override IConditionController CreateGuiControllerInternal()
        {
            return new TimeOfDayConditionGuiController();
        }
    }
}
