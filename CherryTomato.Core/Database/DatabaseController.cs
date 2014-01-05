using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core;
using System.Xml;

namespace CherryTomato.Core.Database
{
    public class DatabaseController : IPlugin, ICherryEventsProvider, IDisposable
    {
        private DatabaseConnection connection;

        private string databaseFilename = 
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cherrytomato\\database.sqlite";

        private CherryEvent connectToDbEvent = new CherryEvent(
            "Connect To Database Event",
            "Fired when a DB connection became available. Brings connection in event arguments.");

        public string PluginName
        {
            get { return "Database Controller"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigureEventArgs)));
        }

        private void LoadConfiguration(ConfigureEventArgs cpea)
        {
            var databaseFileNode = (XmlElement)cpea.Document.SelectSingleNode("//cherryTomato/databaseFile");
            if (databaseFileNode != null)
            {
                this.databaseFilename = databaseFileNode.GetAttribute("path");
            }

            this.connection = new DatabaseConnection(this.databaseFilename);
            var ea = new ConnectToDbEventArgs(this.connection);
            this.connectToDbEvent.Fire(ea);
        }

        private void SaveConfiguration(ConfigureEventArgs cpea)
        {
            var databaseFileNode = cpea.Document.CreateElement("databaseFile");
            databaseFileNode.SetAttribute("path", this.databaseFilename);
            cpea.RootNode.AppendChild(databaseFileNode);
        }

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.connectToDbEvent;
        }

        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.Dispose();
            }
        }
    }
}
