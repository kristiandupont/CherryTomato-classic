using System.Windows.Forms;

namespace CherryTomato.PomodoroEvaluation
{
    public partial class OutOfPomodoroReminderControl : UserControl
    {
        public OutOfPomodoroReminderControl()
        {
            this.InitializeComponent();
        }

        public RadioButton Check5Min { get { return this.fiveMinutesCheckBox; } }

        public RadioButton Check10Min { get { return this.tenMinutesCheckBox; } }

        public RadioButton Check15Min { get { return this.fifteenMinutesCheckBox; } }
    }
}
