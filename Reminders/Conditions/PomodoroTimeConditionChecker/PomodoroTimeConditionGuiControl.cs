using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.PomodoroTimeConditionChecker
{
    public partial class PomodoroTimeConditionGuiControl : UserControl
    {
        public PomodoroTimeConditionGuiControl()
        {
            this.InitializeComponent();
        }

        public bool ConditionEnabled
        {
            get { return this.enableCheckBox.Checked; }
            set { this.enableCheckBox.Checked = value; }
        }

        public bool More
        {
            get { return this.rbtnMore.Checked; }
            set { (value ? this.rbtnMore : this.rbtnLess).Checked = true; }
        }

        public TimeSpan Timeout
        {
            get { return TimeSpan.FromMinutes((double)this.numMinutes.Value); }
            set { this.numMinutes.Value = (decimal)value.TotalMinutes; }
        }

        public bool Start
        {
            get { return this.rbtnStart.Checked; }
            set { (value ? this.rbtnStart : this.rbtnFinish).Checked = true; }
        }
    }
}
