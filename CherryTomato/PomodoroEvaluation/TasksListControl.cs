using System.Windows.Forms;

namespace CherryTomato.PomodoroEvaluation
{
    public partial class TasksListControl : UserControl
    {
        public TasksListControl()
        {
            this.InitializeComponent();
        }

        public DataGridView GridControl { get { return this.dataGridView; } }

        public Button ViewButton { get { return this.viewButton; } }

        public ContextMenuStrip ViewButtonMenu { get { return this.viewButtonMenu; } }
    }
}
