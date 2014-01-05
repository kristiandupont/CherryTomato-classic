using System.Windows.Forms;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.AfterPomodoroReminder
{
    public class AfterPomodoroReminderController : IReminderController
    {
        private AfterPomodoroReminderControl editorControl;

        private AfterPomodoroReminderControl EditorControl
        {
            get
            {
                return this.editorControl ?? (this.editorControl = new AfterPomodoroReminderControl());
            }
        }

        public void LoadSettingsFromReminder(IReminder reminder)
        {
            this.EditorControl.Populate(reminder as AfterPomodoroReminder);
        }

        public void SaveSettingsToReminder(IReminder reminder)
        {
            this.EditorControl.Save(reminder as AfterPomodoroReminder);
        }

        public Control GetSettingsPanel()
        {
            return this.EditorControl;
        }
    }
}
