using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core;
using CherryTomato.SystrayIcon;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.SystrayIcon.FirstRun
{
    public class FirstRunSensor : IPlugin
    {
        private ICherryCommand showBaloonCommand;

        /// <summary>
        /// Has the application run before?
        /// (If not, we should show the welcoming bubble)
        /// </summary>
        public bool HasRun { get; private set; }

        public string PluginName
        {
            get { return "First Run Sensor"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigureEventArgs)));

            this.showBaloonCommand = plugins.CherryCommands["Show Balloon Tip", false];

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Application Started",
                ea =>
                {
                    if (!this.HasRun && this.showBaloonCommand != null)
                    {
                        this.showBaloonCommand.Do(new BaloonTipCommandArgs(new BaloonState
                        {
                            Caption = "I'm right here!",
                            Message = "This icon represents cherrytomato. Right-click to start a pomodoro.",
                        }));
                    }
                }),
                false);
        }

        private void SaveConfiguration(ConfigureEventArgs cpea)
        {
            var hasRunNode = cpea.Document.CreateElement("hasRun");
            cpea.RootNode.AppendChild(hasRunNode);
        }

        private void LoadConfiguration(ConfigureEventArgs cpea)
        {
            var hasRunNode = cpea.Document.SelectSingleNode("//cherryTomato/hasRun");
            this.HasRun = hasRunNode != null;
        }
    }
}
