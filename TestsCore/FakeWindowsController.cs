using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.TestsCore
{
    public class FakeWindowsController : IPlugin, ICherryCommandsProvider, ICherryEventsProvider
    {
        public string PluginName
        {
            get { return "FakeWindowsController"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
        }

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return new CherryCommand(
                "Set Active Window Foreground",
                ca => { return true; });
        }

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return new CherryEvent("Active Window Changed");
        }
    }
}
