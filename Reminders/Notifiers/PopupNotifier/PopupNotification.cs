using System.Windows.Forms;
using CherryTomato.Reminders.Core.Notifications;

namespace CherryTomato.Reminders.PopupNotifier
{
    public class PopupNotification : Notification
    {
        public override string TypeName { get { return "PopupNotification"; } }
        public string Caption { get; set; }
        public string NotificationText { get; set; }
        public ToolTipIcon Icon { get; set; }
    }
}
