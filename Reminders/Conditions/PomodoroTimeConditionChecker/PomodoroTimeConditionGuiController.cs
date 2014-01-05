using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Reminders.Core.Conditions;
using System.Windows.Forms;

namespace CherryTomato.Reminders.PomodoroTimeConditionChecker
{
    public class PomodoroTimeConditionGuiController : IConditionController
    {
        private PomodoroTimeConditionGuiControl settingsPanel;

        public PomodoroTimeConditionGuiController()
        {
            this.settingsPanel = new PomodoroTimeConditionGuiControl();
        }

        public void LoadSettingsFromCondition(ICondition condition)
        {
            var c = (PomodoroTimeCondition)condition;
            this.settingsPanel.ConditionEnabled = c.Enabled;
            this.settingsPanel.More = c.More;
            this.settingsPanel.Timeout = c.Timeout;
            this.settingsPanel.Start = c.AfterPomodoroStarted;
        }

        public void SaveSettingsToCondition(ICondition condition)
        {
            var c = (PomodoroTimeCondition)condition;
            c.Enabled = this.settingsPanel.ConditionEnabled;
            c.More = this.settingsPanel.More;
            c.Timeout = this.settingsPanel.Timeout;
            c.AfterPomodoroStarted = this.settingsPanel.Start;
            c.AfterPomodoroCompleted = !this.settingsPanel.Start;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
