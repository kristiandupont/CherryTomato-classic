using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Settings
{
    public partial class SettingsForm : Form
    {
        private PluginRepository plugins;

        public SettingsForm(
            PluginRepository plugins, 
            GuiConfigurablePluginInfo generalSettingsInfo,
            IEnumerable<GuiConfigurablePluginInfo> guiPluginInfos)
        {
            this.plugins = plugins;
            InitializeComponent();

            var generalSettingsPanel = generalSettingsInfo.PluginSettingsPanel;
            var generalSettingsNode = new TreeNode(generalSettingsInfo.PluginName) { Tag = generalSettingsPanel };
            settingsTreeView.Nodes.Add(generalSettingsNode);

            foreach (var plugin in guiPluginInfos)
            {
                var settingsNode = new TreeNode(plugin.PluginName) { Tag = plugin.PluginSettingsPanel };
                
                if(plugin.PluginIcon != null)
                {
                    treeviewImages.Images.Add(plugin.PluginIcon);
                    settingsNode.ImageIndex = treeviewImages.Images.Count - 1;
                    settingsNode.SelectedImageIndex = treeviewImages.Images.Count - 1;
                }

                generalSettingsNode.Nodes.Add(settingsNode);
            }

            settingsTreeView.SelectedNode = generalSettingsNode;
            generalSettingsNode.ExpandAll();
            panel.Controls.Add(generalSettingsPanel);

            CreateHandle();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Visible = false;
            e.Cancel = true;
        }

        private void settingsTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            panel.Controls.Clear();
            panel.Controls.Add((Control)settingsTreeView.SelectedNode.Tag);
            ((Control)settingsTreeView.SelectedNode.Tag).Refresh();
        }
    }
}
