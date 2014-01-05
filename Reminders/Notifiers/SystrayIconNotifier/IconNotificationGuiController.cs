using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.SystrayIconNotifier
{
    public class IconNotificationGuiController : INotificationController
    {
        private IconNotificationControl settingsPanel;

        public IconNotificationGuiController()
        {
            this.settingsPanel = new IconNotificationControl();
        }

        public void LoadSettingsFromNotification(INotification notification)
        {
            var n = notification as IconNotification;
            this.settingsPanel.NotificationEnabled = n.Enabled;
            this.settingsPanel.FlashesCount = n.FlashCount;

            this.settingsPanel.IconText = this.GetIconTitle(n.FlashIconPath);
        }

        public void SaveSettingsToNotification(INotification notification)
        {
            var n = notification as IconNotification;
            n.Enabled = this.settingsPanel.NotificationEnabled;
            n.FlashCount = this.settingsPanel.FlashesCount;
            n.FlashIconPath = this.settingsPanel.GetCurrentlySelectedIconPath();
        }

        private string GetIconTitle(string path)
        {
            if (path.StartsWith("res://"))
            {
                var resourceName = path.Substring("res://".Length);
                var iconName = resourceName.Substring(0, resourceName.LastIndexOf('.'));
                iconName = char.ToUpper(iconName[0]) + iconName.Substring(1);
                return iconName;
            }

            return path;
        }

        public Control GetSettingsPanel()
        {
            return this.settingsPanel;
        }
    }
}
