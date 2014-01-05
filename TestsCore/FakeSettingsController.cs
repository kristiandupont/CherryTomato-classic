using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.TestsCore
{
    public class FakeSettingsController : IPlugin, ICherryEventsProvider, ICherryCommandsProvider
    {
        public FakeSettingsController()
        {
            this.showSettingsCommand = new CherryCommand(
                "Show Settings",
                ca => true);
        }

        #region IPlugin Members

        public string PluginName
        {
            get { return "Fake Settings Controller"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
        }

        #endregion

        #region ICherryEventsProvider Members

        private CherryEvent collectGuiConfigurablePluginsEvent = new CherryEvent("Collect GUI Configurable Plugins");

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.collectGuiConfigurablePluginsEvent;
        }

        #endregion

        #region ICherryCommandsProvider Members

        private CherryCommand showSettingsCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.showSettingsCommand;
        }

        #endregion
    }
}
