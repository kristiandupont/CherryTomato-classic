using System;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.SoundController;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Settings
{
    public partial class GeneralSettingsPanel : UserControl
    {
        private PluginRepository plugins;

        public GeneralSettingsPanel(PluginRepository plugins)
        {
            InitializeComponent();

            this.plugins = plugins;

            var v = new Version(Application.ProductVersion);
            versionLabel.Text = "Version " + v.Major + "." + v.Minor;

            // Currently, we can't check for updates..
            checkForUpdatesLinkLabel.Visible = false;

            var soundSettings =
                plugins.CherryCommands["Get Sound Settings"].Do(null) as
                SoundSettings;
            rewindCheckBox.Checked = soundSettings.PlayRewindSound;
            tickingSoundcheckBox.Checked = soundSettings.PlayTickingSound;
            ringSoundcheckBox.Checked = soundSettings.PlayRingSound;
        }

        private void rewindCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PopulateSoundSettings();
        }

        private void tickingSoundcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PopulateSoundSettings();
        }

        private void ringSoundcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.PopulateSoundSettings();
        }

        private void PopulateSoundSettings()
        {
            var soundSettings = new SoundSettings()
            {
                PlayRewindSound = rewindCheckBox.Checked,
                PlayTickingSound = tickingSoundcheckBox.Checked,
                PlayRingSound = ringSoundcheckBox.Checked,
            };

            this.plugins.CherryCommands["Set Sound Settings"].Do(
                new SoundSettingsCommandArgs(soundSettings));
        }
    }
}
