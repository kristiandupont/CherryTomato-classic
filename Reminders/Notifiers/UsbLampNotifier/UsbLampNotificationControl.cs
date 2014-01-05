using System;
using System.Windows.Forms;
using CherryTomato.Core;
using System.Drawing;

namespace CherryTomato.Reminders.UsbLampNotifier
{
    public partial class UsbLampNotificationControl : UserControl
    {
        public UsbLampNotificationControl()
        {
            this.InitializeComponent();

            this.LampColor = Color.Red;
        }

        public bool NotificationEnabled
        {
            get { return this.flashCheckBox.Checked; }
            set { this.flashCheckBox.Checked = value; }
        }

        public Color LampColor
        {
            get { return this.changeColorButton.BackColor; }
            set { this.changeColorButton.BackColor = value; }
        }

        public int FlashesCount
        {
            get { return (int)this.flashCountNumericUpDown.Value; }
            set { this.flashCountNumericUpDown.Value = value; }
        }

        private void changeColorButton_Click(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            cd.Color = this.LampColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.LampColor = cd.Color;
            }
        }
    }
}
