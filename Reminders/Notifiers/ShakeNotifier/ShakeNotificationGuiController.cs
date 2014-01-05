using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.ShakeNotifier
{
    public class ShakeNotificationGuiController : INotificationController
    {
        private ShakeNotificationControl settingsPanel;

        public ShakeNotificationGuiController()
        {
            this.settingsPanel = new ShakeNotificationControl();
        }

        public void LoadSettingsFromNotification(INotification notification)
        {
            var n = notification as ShakeNotification;
            this.settingsPanel.NotificationEnabled = n.Enabled;
            this.settingsPanel.Timeout = n.Timeout;
        }

        public void SaveSettingsToNotification(INotification notification)
        {
            var n = notification as ShakeNotification;
            n.Enabled = this.settingsPanel.NotificationEnabled;
            n.Timeout = this.settingsPanel.Timeout;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
