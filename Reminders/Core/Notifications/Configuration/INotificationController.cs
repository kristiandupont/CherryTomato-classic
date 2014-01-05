using System.Windows.Forms;

namespace CherryTomato.Reminders.Core.Notifications.Configuration
{
    public interface INotificationController
    {
        void LoadSettingsFromNotification(INotification notification);
        void SaveSettingsToNotification(INotification notification);
        Control GetSettingsPanel();
    }
}
