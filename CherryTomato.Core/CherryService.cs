using System;
using System.IO;
using System.Threading;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core
{
    public class CherryService : IDisposable
    {
        public CherryService()
        {
            this.PluginRepository = new PluginRepository();
        }

        public void Start()
        {
            this.applicationStartedEvent.Fire(null);
        }

        public PluginRepository PluginRepository { get; private set; }

        public void Dispose()
        {
            this.PluginRepository.Dispose();
        }

        #region Config

        private string configFilePath;

        public void LoadConfiguration(string path)
        {
            this.configFilePath = path;

            // If the configuration doesn't exist, just stop here. We will save the configuration when necessary.
            if (File.Exists(path))
            {
                using (var streamReader = new StreamReader(path))
                {
                    this.LoadConfiguration(streamReader);
                }
            }
        }

        public void LoadConfiguration(TextReader textReader)
        {
            var xd = new XmlDocument();
            xd.Load(textReader);

            this.loadConfigEvent.Fire(new ConfigureEventArgs(xd));
        }

        public void SaveConfiguration()
        {
            var dir = Path.GetDirectoryName(configFilePath);
            if (!Directory.Exists(dir) && !string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var streamWriter = new StreamWriter(configFilePath))
            {
                this.SaveConfiguration(streamWriter);
            }
        }

        public void SaveConfiguration(TextWriter textWriter)
        {
            var xd = new XmlDocument();

            var rootNode = xd.CreateElement("cherryTomato");
            xd.AppendChild(rootNode);

            var configNode = xd.CreateElement("config");
            rootNode.AppendChild(configNode);

            this.saveConfigEvent.Fire(new ConfigureEventArgs(xd));

            xd.Save(textWriter);
        }

        #endregion

        private CherryEvent loadConfigEvent = new CherryEvent(
            "Load Configuration Event",
            "Fired on application initialization. Brings configuration reader with event arguments. All core parts of application uses this plugin for configurating.");
        private CherryEvent saveConfigEvent = new CherryEvent(
            "Save Configuration Event",
            "Firs on application exit. Brings configuration writer object with event arguments. All core parts of application stores settings thru this event.");
        private CherryEvent applicationStartedEvent = new CherryEvent(
            "Application Started",
            "Fired after the core parts of application finished all kind intializations. The system is up and ready for actions.");

        public void InitializeCherryServiceEventsAndCommands()
        {
            this.PluginRepository.CherryEvents.AddEvent(this.loadConfigEvent);
            this.PluginRepository.CherryEvents.AddEvent(this.saveConfigEvent);
            this.PluginRepository.CherryEvents.AddEvent(this.applicationStartedEvent);

            this.PluginRepository.InitializePluginRepositoryEventsAndCommands();
        }
    }
}
