using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.Reminders.DayOfWeekConditionChecker
{
    public partial class DayOfWeekConditionGuiControl : UserControl
    {
        public DayOfWeekConditionGuiControl()
        {
            this.InitializeComponent();
            this.chkSunday.Tag = DayOfWeek.Sunday;
            this.chkMonday.Tag = DayOfWeek.Monday;
            this.chkTuesday.Tag = DayOfWeek.Tuesday;
            this.chkWednesday.Tag = DayOfWeek.Wednesday;
            this.chkThursday.Tag = DayOfWeek.Thursday;
            this.chkFriday.Tag = DayOfWeek.Friday;
            this.chkSaturday.Tag = DayOfWeek.Saturday;
        }

        public bool ConditionEnabled
        {
            get { return this.checkDayCheckBox.Checked; }
            set { this.checkDayCheckBox.Checked = value; }
        }

        public IEnumerable<DayOfWeek> CheckedDays
        {
            get
            {
                return this.AllCheckboxes.Where(chk => chk.Checked).Select(c => (DayOfWeek)c.Tag);
            }

            set
            {
                foreach (var chk in this.AllCheckboxes)
                {
                    chk.Checked = value.Contains((DayOfWeek)chk.Tag);
                }
            }
        }

        private IEnumerable<CheckBox> AllCheckboxes
        {
            get
            {
                return this.showPopupGroupBox.Controls.
                    OfType<CheckBox>().
                    Except(Enumerable.Repeat(this.checkDayCheckBox, 1));
            }
        }
    }
}
