using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.PopupNotifier
{
    public class PopupNotificationGuiController : INotificationController
    {
        private PopupNotificationControl settingsPanel;

        public PopupNotificationGuiController()
        {
            this.settingsPanel = new PopupNotificationControl();
        }

        public void LoadSettingsFromNotification(INotification notification)
        {
            var n = notification as PopupNotification;
            this.settingsPanel.NotificationEnabled = n.Enabled;
            this.settingsPanel.Caption = n.Caption;
            this.settingsPanel.Message = n.NotificationText;
            this.settingsPanel.IconType = n.Icon;
        }

        public void SaveSettingsToNotification(INotification notification)
        {
            var n = notification as PopupNotification;
            n.Enabled = this.settingsPanel.NotificationEnabled;
            n.Caption = this.settingsPanel.Caption;
            n.NotificationText = this.settingsPanel.Message;
            n.Icon = this.settingsPanel.IconType;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
