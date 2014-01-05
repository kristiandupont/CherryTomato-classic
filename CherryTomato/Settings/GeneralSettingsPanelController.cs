using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Settings
{
    public class GeneralSettingsPanelController : IPlugin
    {
        public string PluginName
        {
            get { return "General"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect GUI Configurable Plugins",
                ea =>
                {
                    var info = new GuiConfigurablePluginInfo(
                        this.PluginName,
                        new GeneralSettingsPanel(plugins),
                        null);
                    (ea as GuiConfigurablePluginEventArgs).GeneralSettingsInfo = info;
                }));
        }
    }
}
