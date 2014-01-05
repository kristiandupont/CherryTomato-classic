using System;
using System.Windows.Forms;
using CherryTomato.Core;

namespace CherryTomato.Reminders.SystrayIconNotifier
{
    public partial class IconNotificationControl : UserControl
    {
        public IconNotificationControl()
        {
            this.InitializeComponent();

            this.iconComboBox.Items.AddRange(new object[] { "Blue", "Cyan", "Green", "Yellow", "Red", "Purple" });
            this.iconComboBox.SelectedIndex = 0;
        }

        public bool NotificationEnabled
        {
            get { return this.flashSystemTrayIconCheckBox.Checked; }
            set { this.flashSystemTrayIconCheckBox.Checked = value; }
        }

        public string IconText
        {
            get { return this.iconComboBox.Text; }
            set { this.iconComboBox.Text = value; }
        }

        public int FlashesCount
        {
            get { return (int)this.flashCountNumericUpDown.Value; }
            set { this.flashCountNumericUpDown.Value = value; }
        }

        public string GetCurrentlySelectedIconPath()
        {
            return "res://" + this.IconText.ToString().ToLower() + ".ico";
        }

        private void iconComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.iconPreviewPictureBox.Image = Helpers.LoadIcon(this.GetCurrentlySelectedIconPath()).ToBitmap();
        }
    }
}
