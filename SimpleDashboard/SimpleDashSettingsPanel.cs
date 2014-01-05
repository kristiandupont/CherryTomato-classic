using System;
using System.Windows.Forms;

namespace CherryTomato.SimpleDashboard
{
    public partial class SimpleDashSettingsPanel : UserControl
    {
        private readonly SimpleDashboard simpleDashController;

        public SimpleDashSettingsPanel(SimpleDashboard simpleDashController)
        {
            InitializeComponent();
            this.simpleDashController = simpleDashController;

            RefreshControls();
        }

        private void enabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            simpleDashController.Enabled = enabledCheckBox.Checked;
        }

        private void showInTaskBarCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            simpleDashController.ShowInTaskBar = showInTaskBarCheckBox.Checked;
        }

        private void alwaysOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            simpleDashController.AlwaysOnTop = alwaysOnTopCheckBox.Checked;
        }

        public override void Refresh()
        {
            RefreshControls();
            base.Refresh();
        }

        private void RefreshControls()
        {
            enabledCheckBox.Checked = simpleDashController.Enabled;
            showInTaskBarCheckBox.Checked = simpleDashController.ShowInTaskBar;
            alwaysOnTopCheckBox.Checked = simpleDashController.AlwaysOnTop;
        }
    }
}