using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.SoundNotifier
{
    public class SoundNotificationGuiController : INotificationController
    {
        private SoundNotificationControl settingsPanel;

        public SoundNotificationGuiController()
        {
            this.settingsPanel = new SoundNotificationControl();
        }

        public void LoadSettingsFromNotification(INotification notification)
        {
            var n = notification as SoundNotification;
            this.settingsPanel.NotificationEnabled = n.Enabled;
            this.settingsPanel.SoundPath = n.SoundPath;
        }

        public void SaveSettingsToNotification(INotification notification)
        {
            var n = notification as SoundNotification;
            n.Enabled = this.settingsPanel.NotificationEnabled;
            n.SoundPath = this.settingsPanel.SoundPath;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
