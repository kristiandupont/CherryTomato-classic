using System;
using System.Linq;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.DayOfWeekConditionChecker
{
    public class DayOfWeekConditionChecker : ConditionChecker
    {
        private ICherryCommand now;

        public override bool IsTrue(ICondition condition)
        {
            var c = condition as DayOfWeekCondition;
            var currentDayOfWeek = ((DateTime)this.now.Do(null)).DayOfWeek;
            return c.Days.Contains(currentDayOfWeek);
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.now = plugins.CherryCommands["Get Current Time"];
        }

        public override void LoadConditionFromXml(ICondition condition, XmlElement conditionElement)
        {
            var c = condition as DayOfWeekCondition;
            var daysOfWeek = 
                Enum.GetValues(typeof(DayOfWeek)).
                OfType<DayOfWeek>().
                Where(day => 
                    !string.IsNullOrEmpty(conditionElement.GetAttribute(day.ToString())) &&
                    bool.Parse(conditionElement.GetAttribute(day.ToString())));
            c.Days = daysOfWeek;
        }

        public override void SaveConditionToXml(ICondition condition, XmlElement conditionElement)
        {
            var c = condition as DayOfWeekCondition;
            foreach (DayOfWeek day in c.Days)
            {
                conditionElement.SetAttribute(day.ToString(), true.ToString());                
            }            
        }

        public override ICondition CreateCondition()
        {
            return new DayOfWeekCondition();
        }

        protected override IConditionController CreateGuiControllerInternal()
        {
            return new DayOfWeekConditionGuiController();
        }
    }
}
