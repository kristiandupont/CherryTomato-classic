using System;
using System.Windows.Forms;

namespace CherryTomato.VersionCheck
{
    public partial class NewVersionAvailableForm : Form
    {
        public NewVersionAvailableForm(Version newestVersion)
        {
            InitializeComponent();
            label1.Text = "Version " + newestVersion.Major + "." + newestVersion.Minor + " is now available";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var target = "http://www.beatpoints.com/cherrytomato";
            System.Diagnostics.Process.Start(target);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
