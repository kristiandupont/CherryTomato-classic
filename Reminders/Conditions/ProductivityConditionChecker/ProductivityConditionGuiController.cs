using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Reminders.Core.Conditions;
using System.Windows.Forms;

namespace CherryTomato.Reminders.ProductivityConditionChecker
{
    public class ProductivityConditionGuiController : IConditionController
    {
        private ProductivityConditionGuiControl settingsPanel;

        public ProductivityConditionGuiController()
        {
            this.settingsPanel = new ProductivityConditionGuiControl();
        }

        public void LoadSettingsFromCondition(ICondition condition)
        {
            var c = (ProductivityCondition)condition;
            this.settingsPanel.ConditionEnabled = c.Enabled;
            this.settingsPanel.Pomodoros = c.Pomodoros;
            this.settingsPanel.Duration = c.Duration;
        }

        public void SaveSettingsToCondition(ICondition condition)
        {
            var c = (ProductivityCondition)condition;
            c.Enabled = this.settingsPanel.ConditionEnabled;
            c.Pomodoros = this.settingsPanel.Pomodoros;
            c.Duration = this.settingsPanel.Duration;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
