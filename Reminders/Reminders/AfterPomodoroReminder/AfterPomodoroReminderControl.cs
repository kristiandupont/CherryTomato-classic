using System;
using System.Windows.Forms;

namespace CherryTomato.Reminders.AfterPomodoroReminder
{
    public partial class AfterPomodoroReminderControl : UserControl
    {
        public AfterPomodoroReminderControl()
        {
            this.InitializeComponent();
        }

        public void Populate(AfterPomodoroReminder reminder)
        {
            this.numMinutes.Value = (decimal)reminder.Timeout.TotalMinutes;

            this.rbtnStarted.Checked = reminder.Started;
            this.rbtnRung.Checked = reminder.Rung;
        }

        public void Save(AfterPomodoroReminder reminder)
        {
            reminder.Timeout = new TimeSpan(0, 0, (int)this.numMinutes.Value, 0);

            reminder.Started = this.rbtnStarted.Checked;
            reminder.Rung = this.rbtnRung.Checked;
        }
    }
}
