using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.ProcessStopper
{
    public class ProcessCloserPlugin : IPlugin
    {
        private List<string> configuredVictims;

        public ProcessCloserPlugin()
        {
            this.configuredVictims = new List<string>();
        }
    
        public string PluginName
        {
            get { return "Close Applications"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect GUI Configurable Plugins",
                ea =>
                {
                    var info = new GuiConfigurablePluginInfo(
                        this.PluginName,
                        new ProcessCloserSettingsPanel(this),
                        Helpers.LoadIcon("res://process.ico"));
                    (ea as GuiConfigurablePluginEventArgs).PluginInfos.Add(info);
                }));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Plugin Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Plugin Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigurePluginEventArgs)));

            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

        }

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            this.configuredVictims.Clear();
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                foreach (XmlElement node in fromElement.SelectNodes("process"))
                {
                    string name = node.GetAttribute("name");
                    this.configuredVictims.Add(name);
                }
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);
            foreach (var proc in this.configuredVictims)
            {
                var processNode = cpea.Document.CreateElement("process");
                processNode.SetAttribute("name", proc);
                pluginElement.AppendChild(processNode);
            }
        }

        public bool ContainsVictim(string victimProcessName)
        {
            return this.configuredVictims.Any(vpn => vpn == victimProcessName);
        }

        public void AddVictim(string victim)
        {
            if (!this.configuredVictims.Contains(victim))
            {
                this.configuredVictims.Add(victim);
            }
        }

        public void RemoveVictim(string victim)
        {
            this.configuredVictims.Remove(victim);
        }

        private void StartPomodoroInternal()
        {
            new Thread(
                () =>
                {
                    foreach (var proc in this.configuredVictims.SelectMany(vn => Process.GetProcessesByName(vn)))
                    {
                        this.CloseProcess(proc);
                    }
                }).Start();
        }

        private void CloseProcess(Process proc)
        {
            try
            {
                if (!proc.CloseMainWindow())
                {
                    proc.Close();
                }
            }
            catch
            {
            }
        }
    }
}
