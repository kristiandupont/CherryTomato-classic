using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.ShakeNotifier
{
    public partial class ShakeNotificationControl : UserControl
    {
        public ShakeNotificationControl()
        {
            this.InitializeComponent();
        }

        public bool NotificationEnabled
        {
            get { return this.shakeCurrentlyActiveWindowCheckBox.Checked; }
            set { this.shakeCurrentlyActiveWindowCheckBox.Checked = value; }
        }

        public int Timeout
        {
            get { return (int)this.numTimeout.Value * 1000; }
            set { this.numTimeout.Value = value / 1000; }
        }
    }
}
