using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Reminders.Core.Conditions;
using System.Windows.Forms;

namespace CherryTomato.Reminders.InPomodoroConditionChecker
{
    public class InPomodoroConditionGuiController : IConditionController
    {
        private InPomodoroConditionGuiControl settingsPanel;

        public InPomodoroConditionGuiController()
        {
            this.settingsPanel = new InPomodoroConditionGuiControl();
        }

        public void LoadSettingsFromCondition(ICondition condition)
        {
            var c = (InPomodoroCondition)condition;
            this.settingsPanel.ConditionEnabled = c.Enabled;
            this.settingsPanel.ShouldBeInOrOut = c.ShouldBeInPomodoro;
        }

        public void SaveSettingsToCondition(ICondition condition)
        {
            var c = (InPomodoroCondition)condition;
            c.Enabled = this.settingsPanel.ConditionEnabled;
            c.ShouldBeInPomodoro = this.settingsPanel.ShouldBeInOrOut;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
