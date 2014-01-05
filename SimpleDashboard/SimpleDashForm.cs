using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.SimpleDashboard
{
    public partial class SimpleDashForm : Form
    {
        public SimpleDashForm()
        {
            InitializeComponent();
            viewModeComboBox.SelectedIndex = 0;
        }

        public void SetProductivityIndices(Dictionary<DateTime, PomodorosProductivity> productivityIndices)
        {
            monthChart1.productivityIndices = productivityIndices;
        }

        public void SetStatusLabelText(string text)
        {
            this.UpdateControl(() => { statusLabel.Text = text; });
        }

        private void SimpleDashForm_Resize(object sender, EventArgs e)
        {
            statusLabel.Font = new Font("Microsoft Sans Serif", Size.Width / 24.0f);
            Invalidate(true);
        }

        private void SimpleDashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Visible = false;
                e.Cancel = true;
            }
        }

        private void viewModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            monthChart1.ViewMode = (MonthChart.ViewModeEnum)viewModeComboBox.SelectedIndex;
        }
    }

}