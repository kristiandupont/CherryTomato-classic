using System.Windows.Forms;

namespace CherryTomato.Reminders.Core.Reminders.Configuration
{
    public interface IReminderController
    {
        void LoadSettingsFromReminder(IReminder reminder);
        void SaveSettingsToReminder(IReminder reminder);
        Control GetSettingsPanel();
    }
}
