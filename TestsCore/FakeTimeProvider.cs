using System;
using System.Collections.Generic;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.TestsCore
{
    public class FakeTimeProvider : IPlugin, ICherryCommandsProvider, ICherryEventsProvider
    {
        private CherryCommand nowCommand;
        private CherryCommand addNewTriggerCommand;
        private CherryCommand removeExistingTriggerCommand;
        private CherryCommand scheduleActionCommand;

        public FakeTimeProvider()
        {
            Now = DateTime.Now;
            this.nowCommand = new CherryCommand("Get Current Time", ca => this.Now);
            this.addNewTriggerCommand = new CherryCommand("Add New Time Trigger", ca => true);
            this.removeExistingTriggerCommand = new CherryCommand("Remove Existing Time Trigger", ca => true);
            this.scheduleActionCommand = new CherryCommand("Schedule Single Action", ca => true);
        }

        public DateTime Now { get; set; }

        public void AdvanceMinutes(int minutes)
        {
            this.Now = this.Now.AddMinutes(minutes);
        }

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.nowCommand;
            yield return this.addNewTriggerCommand;
            yield return this.removeExistingTriggerCommand;
            yield return this.scheduleActionCommand;
        }

        public string PluginName
        {
            get { return "Time Provider"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            // Time provider provide events and commands but do not depend on any other stuff.
        }

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return new CherryEvent("Noon Crossed");
        }
    }
}
