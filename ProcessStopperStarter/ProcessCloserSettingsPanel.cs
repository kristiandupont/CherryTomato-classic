using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CherryTomato.ProcessStopper
{
    public partial class ProcessCloserSettingsPanel : UserControl
    {
        private readonly ProcessCloserPlugin killerPlugin;

        public ProcessCloserSettingsPanel(ProcessCloserPlugin plugin)
        {
            this.killerPlugin = plugin;
            this.InitializeComponent();

            this.RefreshControls();
        }

        public override void Refresh()
        {
            this.RefreshControls();
            base.Refresh();
        }

        private void RefreshControls()
        {
            this.gridProcesses.CellValueChanged -= this.gridProcesses_CellValueChanged;
            
            this.gridProcesses.Rows.Clear();
            foreach (var process in Process.GetProcesses().Select(p => p.ProcessName).Distinct().OrderBy(p => p))
            {
                var row = this.GetRowFromProcess(process);
                this.gridProcesses.Rows.Add(row);
            }

            this.gridProcesses.CellValueChanged += this.gridProcesses_CellValueChanged;
        }

        private object[] GetRowFromProcess(string killingProcessName)
        {
            return new object[2]
            {
                this.killerPlugin.ContainsVictim(killingProcessName),
                killingProcessName,
            };
        }
        
        private void gridProcesses_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((bool)this.gridProcesses.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
            {
                this.killerPlugin.AddVictim(this.gridProcesses.Rows[e.RowIndex].Cells["processName"].Value as string);
            }
            else
            {
                this.killerPlugin.RemoveVictim(this.gridProcesses.Rows[e.RowIndex].Cells["processName"].Value as string);
            }
        }

        private void gridProcesses_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                this.gridProcesses.EndEdit();
            }
        }
    }
}