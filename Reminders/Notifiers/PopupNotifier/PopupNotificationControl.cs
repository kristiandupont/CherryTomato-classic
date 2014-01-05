using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.PopupNotifier
{
    public partial class PopupNotificationControl : UserControl
    {
        public PopupNotificationControl()
        {
            this.InitializeComponent();

            this.cmbPopupType.Items.AddRange(
                Enum.GetValues(typeof(ToolTipIcon)).
                OfType<object>().
                ToArray());
        }

        public bool NotificationEnabled
        {
            get { return this.showPopupCheckBox.Checked; }
            set { this.showPopupCheckBox.Checked = value; }
        }

        public string Caption
        {
            get { return this.txtCaption.Text; }
            set { this.txtCaption.Text = value; }
        }

        public string Message
        {
            get { return this.popupMessageTextBox.Text; }
            set { this.popupMessageTextBox.Text = value; }
        }

        public ToolTipIcon IconType
        {
            get { return (ToolTipIcon)this.cmbPopupType.SelectedItem; }
            set { this.cmbPopupType.SelectedItem = value; }
        }
    }
}
