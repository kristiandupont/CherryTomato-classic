using System.Collections.Generic;
using CherryTomato.Core.EventsModel;

namespace CherryTomato.Core.PluginArchitecture
{
    public class GuiConfigurablePluginEventArgs : CherryEventArgs
    {
        public GuiConfigurablePluginInfo GeneralSettingsInfo { get; set; }

        public List<GuiConfigurablePluginInfo> PluginInfos { get; protected set; }

        public GuiConfigurablePluginEventArgs()
        {
            this.PluginInfos = new List<GuiConfigurablePluginInfo>();
        }
    }
}
