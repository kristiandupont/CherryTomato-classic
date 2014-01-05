using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.UsbLampNotifier
{
    public class UsbLampNotificationGuiController : INotificationController
    {
        private UsbLampNotificationControl settingsPanel;

        public UsbLampNotificationGuiController()
        {
            this.settingsPanel = new UsbLampNotificationControl();
        }

        public void LoadSettingsFromNotification(INotification notification)
        {
            var n = notification as UsbLampNotification;
            this.settingsPanel.NotificationEnabled = n.Enabled;
            this.settingsPanel.FlashesCount = n.FlashCount;

            this.settingsPanel.LampColor = n.FlashColor;
        }

        public void SaveSettingsToNotification(INotification notification)
        {
            var n = notification as UsbLampNotification;
            n.Enabled = this.settingsPanel.NotificationEnabled;
            n.FlashCount = this.settingsPanel.FlashesCount;
            n.FlashColor = this.settingsPanel.LampColor;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
