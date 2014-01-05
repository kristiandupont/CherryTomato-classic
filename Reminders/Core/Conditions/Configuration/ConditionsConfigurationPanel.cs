using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CherryTomato.Reminders.Core.Conditions.Configuration
{
    public partial class ConditionsConfigurationPanel : UserControl
    {
        public ConditionsConfigurationPanel()
        {
            this.InitializeComponent();
        }

        public ConditionsConfigurationPanel(IEnumerable<Control> notifierEditors) : this()
        {
            this.flowLayoutPanel.Controls.AddRange(notifierEditors.ToArray());
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
