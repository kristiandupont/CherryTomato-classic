using System.Drawing;
using System.Windows.Forms;
using CherryTomato.Core.EventsModel;
using System.Collections.Generic;

namespace CherryTomato.Core.PluginArchitecture
{
    public class GuiConfigurablePluginInfo
    {
        public string PluginName { get; protected set; }

        public Control PluginSettingsPanel { get; protected set; }

        public Icon PluginIcon { get; protected set; }

        public GuiConfigurablePluginInfo(string pluginName, Control settingsControl, Icon icon)
        {
            this.PluginName = pluginName;
            this.PluginSettingsPanel = settingsControl;
            this.PluginIcon = icon;
        }
    }
}
