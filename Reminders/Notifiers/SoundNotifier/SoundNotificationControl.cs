using System;
using System.IO;
using System.Windows.Forms;

namespace CherryTomato.Reminders.SoundNotifier
{
    public partial class SoundNotificationControl : UserControl
    {
        public SoundNotificationControl()
        {
            this.InitializeComponent();
        }

        public bool NotificationEnabled
        {
            get { return this.playSoundCheckBox.Checked; }
            set { this.playSoundCheckBox.Checked = value; }
        }

        public string SoundPath
        {
            get { return this.soundFileTextBox.Text; }
            set { this.soundFileTextBox.Text = value; }
        }

        private void soundFileBrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                FileName = this.soundFileTextBox.Text,
                InitialDirectory = System.IO.Directory.GetCurrentDirectory(),
                RestoreDirectory = true,
                Filter = "Wave Files (*.WAV)|*.WAV|All files (*.*)|*.*"
            };

            if (ofd.ShowDialog(this) != DialogResult.OK) return;

            if (Path.GetDirectoryName(ofd.FileName) == ofd.InitialDirectory)
            {
                this.soundFileTextBox.Text = Path.GetFileName(ofd.FileName);
            }
            else
            {
                this.soundFileTextBox.Text = ofd.FileName;
            }
        }
    }
}
