using System;
using System.Windows.Forms;

namespace CherryTomato.Reminders.ProductivityConditionChecker
{
    public partial class ProductivityConditionGuiControl : UserControl
    {
        public ProductivityConditionGuiControl()
        {
            this.InitializeComponent();
        }

        public bool ConditionEnabled
        {
            get { return this.enableCheckBox.Checked; }
            set { this.enableCheckBox.Checked = value; }
        }

        public int Pomodoros
        {
            get { return (int)this.numPomodoros.Value; }
            set { this.numPomodoros.Value = value; }
        }

        public TimeSpan Duration
        {
            get { return TimeSpan.FromHours((double)this.numHours.Value); }
            set { this.numHours.Value = (decimal)value.TotalHours; }
        }
    }
}
