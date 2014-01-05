using System;
using System.Windows.Forms;

namespace CherryTomato.LiveMessengerController
{
    public partial class LiveMessengerSettingsPanel : UserControl
    {
        private readonly LiveMessengerController msnController;
        private readonly Timer connectionCheckTimer = new Timer { Interval = 100 };

        public LiveMessengerSettingsPanel(LiveMessengerController msnController)
        {
            InitializeComponent();
            this.msnController = msnController;
            connectionCheckTimer.Tick += delegate { UpdateConnectionStatus(); };
            connectionCheckTimer.Start();

            UpdateConnectionStatus();
            enabledCheckBox.Checked = msnController.Enabled;
            statusTextEditBox.Text = msnController.InPomodoroTextTemplate;
        }

        private void UpdateConnectionStatus()
        {
            connectionStatusLabel.Text = "Connection status: " + (msnController.IsConnected ? "Connected" : "Not connected");
            connectButton.Enabled = !msnController.IsConnected;
        }

        private void enabledCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            msnController.Enabled = enabledCheckBox.Checked;
        }

        private void connectButton_Click(object sender, System.EventArgs e)
        {
            msnController.TryConnect();
        }

        private void statusTextEditBox_TextChanged(object sender, System.EventArgs e)
        {
            msnController.InPomodoroTextTemplate = statusTextEditBox.Text;
        }

        private Label label1;
    }
}
