using System;
using System.Windows.Forms;

namespace CherryTomato.Reminders.IntervalReminder
{
    public partial class IntervalReminderControl : UserControl
    {
        public IntervalReminderControl()
        {
            this.InitializeComponent();
        }

        public void Populate(IntervalReminder reminder)
        {
            this.fromIntervalNumericUpDown.Value = (int)reminder.FromInterval.TotalMinutes;
            this.toIntervalNumericUpDown.Value = (int)reminder.ToInterval.TotalMinutes;
        }

        public void Save(IntervalReminder reminder)
        {
            reminder.FromInterval = new TimeSpan(0, 0, (int)this.fromIntervalNumericUpDown.Value, 0);
            reminder.ToInterval = new TimeSpan(0, 0, (int)this.toIntervalNumericUpDown.Value, 0);
        }

        private void fromIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.fromIntervalNumericUpDown.Value > this.toIntervalNumericUpDown.Value)
            {
                this.toIntervalNumericUpDown.Value = this.fromIntervalNumericUpDown.Value;
            }
        }

        private void toIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.toIntervalNumericUpDown.Value < this.fromIntervalNumericUpDown.Value)
            {
                this.fromIntervalNumericUpDown.Value = this.toIntervalNumericUpDown.Value;
            }
        }
    }
}
