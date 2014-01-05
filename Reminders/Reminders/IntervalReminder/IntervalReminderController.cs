using System.Windows.Forms;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.IntervalReminder
{
    public class IntervalReminderController : IReminderController
    {
        private IntervalReminderControl editorControl;

        private IntervalReminderControl EditorControl
        {
            get
            {
                return this.editorControl ?? (this.editorControl = new IntervalReminderControl());
            }
        }

        public void LoadSettingsFromReminder(IReminder reminder)
        {
            this.EditorControl.Populate(reminder as IntervalReminder);
        }

        public void SaveSettingsToReminder(IReminder reminder)
        {
            this.EditorControl.Save(reminder as IntervalReminder);
        }

        public Control GetSettingsPanel()
        {
            return this.EditorControl;
        }
    }
}
