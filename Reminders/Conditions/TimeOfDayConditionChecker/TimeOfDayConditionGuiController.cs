using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Reminders.Core.Conditions;
using System.Windows.Forms;

namespace CherryTomato.Reminders.TimeOfDayConditionChecker
{
    public class TimeOfDayConditionGuiController : IConditionController
    {
        private TimeOfDayConditionGuiControl settingsPanel;

        public TimeOfDayConditionGuiController()
        {
            this.settingsPanel = new TimeOfDayConditionGuiControl();
        }

        public void LoadSettingsFromCondition(ICondition condition)
        {
            var c = (TimeOfDayCondition)condition;
            this.settingsPanel.ConditionEnabled = c.Enabled;
            this.settingsPanel.StartTime = c.StartTime;
            this.settingsPanel.EndTime = c.EndTime;
            this.settingsPanel.ShouldBeWithin = c.ShouldBeWithin;
        }

        public void SaveSettingsToCondition(ICondition condition)
        {
            var c = (TimeOfDayCondition)condition;
            c.Enabled = this.settingsPanel.ConditionEnabled;
            c.StartTime = this.settingsPanel.StartTime;
            c.EndTime = this.settingsPanel.EndTime;
            c.ShouldBeWithin = this.settingsPanel.ShouldBeWithin;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
