using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.TimeOfDayConditionChecker
{
    public partial class TimeOfDayConditionGuiControl : UserControl
    {
        public TimeOfDayConditionGuiControl()
        {
            this.InitializeComponent();
        }

        public bool ConditionEnabled
        {
            get { return this.checkEnabled.Checked; }
            set { this.checkEnabled.Checked = value; }
        }

        public TimeSpan StartTime
        {
            get { return this.timeStart.Value.TimeOfDay; }
            set { this.timeStart.Value = DateTime.Today.Add(value); }
        }

        public TimeSpan EndTime
        {
            get { return this.timeEnd.Value.TimeOfDay; }
            set { this.timeEnd.Value = DateTime.Today.Add(value); }
        }

        public bool ShouldBeWithin
        {
            get { return this.rbtnWithin.Checked; }
            set
            {
                this.rbtnWithin.Checked = value;
                this.rbtnOut.Checked = !value;
            }
        }
    }
}
