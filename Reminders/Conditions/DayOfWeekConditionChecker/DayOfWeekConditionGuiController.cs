using System.Windows.Forms;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.DayOfWeekConditionChecker
{
    public class DayOfWeekConditionGuiController : IConditionController
    {
        private DayOfWeekConditionGuiControl settingsPanel;

        public DayOfWeekConditionGuiController()
        {
            this.settingsPanel = new DayOfWeekConditionGuiControl();
        }

        public void LoadSettingsFromCondition(ICondition condition)
        {
            var c = (DayOfWeekCondition)condition;
            this.settingsPanel.ConditionEnabled = c.Enabled;
            this.settingsPanel.CheckedDays = c.Days;
        }

        public void SaveSettingsToCondition(ICondition condition)
        {
            var c = (DayOfWeekCondition)condition;
            c.Enabled = this.settingsPanel.ConditionEnabled;
            c.Days = this.settingsPanel.CheckedDays;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
