using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;

namespace CherryTomato.TestsCore
{
    public class FakeIconController : IPlugin, ICherryCommandsProvider, ICherryEventsProvider
    {
        private CherryCommand flashIconCommand;
        private CherryCommand showBaloonCommand;

        public FakeIconController()
        {
            this.flashIconCommand = new CherryCommand(
                "Flash Icon",
                fica => true);

            this.showBaloonCommand = new CherryCommand(
                "Show Balloon Tip",
                btca => true);
        }

        #region IPlugin Members

        public string PluginName
        {
            get { return "Fake Icon Controller"; }
        }

        public void TieEvents(PluginRepository plugins)
        {            
        }

        #endregion

        #region ICherryCommandsProvider Members

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.flashIconCommand;
            yield return this.showBaloonCommand;
        }

        #endregion

        #region ICherryEventsProvider Members

        public CherryEvent collectMenuItemsEvent = new CherryEvent("Collect Menu Items");
        public CherryEvent leftButtonClickedEvent = new CherryEvent("Icon Left Button Clicked");

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.collectMenuItemsEvent;
            yield return this.leftButtonClickedEvent;
        }

        #endregion
    }
}
