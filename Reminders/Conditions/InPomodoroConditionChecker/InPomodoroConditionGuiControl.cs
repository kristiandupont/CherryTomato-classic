using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.InPomodoroConditionChecker
{
    public partial class InPomodoroConditionGuiControl : UserControl
    {
        public InPomodoroConditionGuiControl()
        {
            this.InitializeComponent();
        }

        public bool ConditionEnabled
        {
            get { return this.showPopupCheckBox.Checked; }
            set { this.showPopupCheckBox.Checked = value; }
        }

        public bool ShouldBeInOrOut
        {
            get { return this.rbtnShouldBeIn.Checked; }
            set { (value ? this.rbtnShouldBeIn : this.rbtnShouldBeOut).Checked = true; }
        }
    }
}
