using System.Collections.Generic;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.WindowsController;

namespace CherryTomato.Settings
{
    public class SettingsControllerPlugin : IPlugin, ICherryEventsProvider, ICherryCommandsProvider
    {
        private PluginRepository plugins;
        private SettingsForm settingsForm;

        public SettingsControllerPlugin()
        {
            this.showSettingsCommand = new CherryCommand(
                "Show Settings",
                ca => this.ShowSettings(),
                "Show the settings windows.");
        }

        public string PluginName
        {
            get { return "Settings Controller Plugin"; }
        }

        private CherryEvent collectGuiConfigurablePluginsEvent = new CherryEvent(
            "Collect GUI Configurable Plugins",
            "Fired when the settings window is about to show. A plugin (which is configured thru GUI) should subscribe and fill the event arguments with necessary data.");

        private ICherryCommand showWindowCommand;

        public void TieEvents(PluginRepository plugins)
        {
            this.plugins = plugins;
            this.showWindowCommand = plugins.CherryCommands["Show Window"];
        }

        private object ShowSettings()
        {
            if (this.settingsForm == null)
            {
                var ea = new GuiConfigurablePluginEventArgs();
                this.collectGuiConfigurablePluginsEvent.Fire(ea);
                this.settingsForm = new SettingsForm(this.plugins, ea.GeneralSettingsInfo, ea.PluginInfos);
            }

            return this.showWindowCommand.Do(new WindowCommandArgs(this.settingsForm));
        }

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.collectGuiConfigurablePluginsEvent;
        }

        private CherryCommand showSettingsCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.showSettingsCommand;
        }
    }
}
