using System;
using System.Windows.Forms;

namespace CherryTomato.SkypeController
{
    public partial class SkypeSettingsPanel : UserControl
    {
        private readonly SkypeController skypeController;
        private readonly Timer connectionCheckTimer = new Timer { Interval = 100 };

        public SkypeSettingsPanel(SkypeController skypeController)
        {
            InitializeComponent();
            this.skypeController = skypeController;
            connectionCheckTimer.Tick += delegate { UpdateConnectionStatus(); };
            connectionCheckTimer.Start();

            UpdateConnectionStatus();
            enabledCheckBox.Checked = skypeController.Enabled;
            statusTextEditBox.Text = skypeController.InPomodoroTextTemplate;
        }

        private void UpdateConnectionStatus()
        {
            connectionStatusLabel.Text = "Connection status: " + (skypeController.IsConnected ? "Connected" : "Not connected");
            connectButton.Enabled = !skypeController.IsConnected;
            connectionCheckTimer.Start();
        }

        private void enabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            skypeController.Enabled = enabledCheckBox.Checked;
        }

        private void connectButton_Click(object sender, System.EventArgs e)
        {
            skypeController.TryConnect();
        }

        private void statusTextEditBox_TextChanged(object sender, System.EventArgs e)
        {
            skypeController.InPomodoroTextTemplate = statusTextEditBox.Text;
        }
    }
}
