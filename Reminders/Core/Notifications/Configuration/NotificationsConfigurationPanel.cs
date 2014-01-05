using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CherryTomato.Reminders.Core.Notifications.Configuration
{
    public partial class NotificationsConfigurationPanel : UserControl
    {
        public event Action TestButtonClicked;

        public NotificationsConfigurationPanel()
        {
            this.InitializeComponent();
        }

        public NotificationsConfigurationPanel(IEnumerable<Control> notifierEditors) : this()
        {
            this.flowLayoutPanel.Controls.AddRange(notifierEditors.ToArray());
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            this.TestButtonClicked();
        }

        private void flowLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.flowLayoutPanel.Controls)
            {
                ctrl.Width = this.flowLayoutPanel.Width - this.flowLayoutPanel.Margin.Horizontal - ctrl.Padding.Horizontal;
            }

            this.ResumeLayout();
        }
    }
}
