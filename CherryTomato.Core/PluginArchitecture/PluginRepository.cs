using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.CommandsModel;
using System.Windows.Forms;
using System.Linq;
using System.Xml;

namespace CherryTomato.Core.PluginArchitecture
{
    public class PluginRepository : IDisposable
    {
        private readonly List<IPlugin> plugins = new List<IPlugin>();
        
        public readonly CherryEventsCollection CherryEvents = new CherryEventsCollection();
        public readonly CherryCommandsCollection CherryCommands = new CherryCommandsCollection();

        public IEnumerable<IPlugin> All { get { return this.plugins; } }

        public void RegisterPlugins()
        {
            // Register the plugins in here
            this.RegisterPluginsFromAssembly(Assembly.GetEntryAssembly());

            // Register plugins in the folder
            var pluginFiles = Directory.GetFiles(Path.GetDirectoryName(Application.ExecutablePath), "*.dll");
            foreach (var pluginFile in pluginFiles)
            {
                try
                {
                    this.RegisterPluginsFromAssembly(
                        Assembly.LoadFrom(Path.GetFullPath(pluginFile)));
                }
                catch (BadImageFormatException) { }         // This DLL wasn't a .NET assembly. Just skip it.
                catch (ReflectionTypeLoadException) { }
            }
        }

        public void TieEvents()
        {
            this.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                cea => this.LoadConfig(cea as ConfigureEventArgs)));
            this.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                cea => this.SaveConfig(cea as ConfigureEventArgs)));

            foreach (var plugin in this.plugins)
            {
                plugin.TieEvents(this);
            }
        }

        private CherryEvent loadPluginConfigEvent = new CherryEvent(
            "Load Plugin Configuration Event",
            "Fired on plugins initialization. Brings configuration source data with event arguments. All plugins which are configured thru this event.");
        private CherryEvent savePluginConfigEvent = new CherryEvent(
            "Save Plugin Configuration Event",
            "Fired on plugins disposing. Brings configuration writer object with event arguments. Plugins should store its configuration thru this event.");

        public void InitializePluginRepositoryEventsAndCommands()
        {
            this.CherryEvents.AddEvent(this.loadPluginConfigEvent);
            this.CherryEvents.AddEvent(this.savePluginConfigEvent);
        }

        private void SaveConfig(ConfigureEventArgs cea)
        {
            var pluginsNode = cea.Document.CreateElement("plugins");
            cea.ConfigNode.AppendChild(pluginsNode);
            this.savePluginConfigEvent.Fire(new ConfigurePluginEventArgs(cea.Document));
        }

        private void LoadConfig(ConfigureEventArgs cea)
        {
            var cpea = new ConfigurePluginEventArgs(cea.Document);
            this.loadPluginConfigEvent.Fire(new ConfigurePluginEventArgs(cea.Document));
        }

        public void RegisterPlugin(IPlugin plugin)
        {
            this.plugins.Add(plugin);

            this.RegisterInEventsCollection(plugin);
            this.RegisterInCommandsCollection(plugin);
        }

        private void RegisterInEventsCollection(IPlugin plugin)
        {
            var p = plugin as ICherryEventsProvider;
            if (p != null) this.CherryEvents.AddProvider(p);
        }

        private void RegisterInCommandsCollection(IPlugin plugin)
        {
            var p = plugin as ICherryCommandsProvider;
            if (p != null) this.CherryCommands.AddProvider(p);
        }

        public void RegisterPluginsFromAssembly(Assembly assembly)
        {
            Trace.WriteLine("Looking for plugins in " + Path.GetFileName(assembly.Location));
            var pluginType = typeof(IPlugin);
            foreach (var type in assembly.GetTypes())
            {
                if (pluginType.IsAssignableFrom(type) && !type.IsAbstract)
                {
                    Trace.WriteLine("Registering plugin: " + type.Name);
                    var plugin = Activator.CreateInstance(type) as IPlugin;
                    this.RegisterPlugin(plugin);
                }
            }
        }

        public void Dispose()
        {
            foreach (var d in this.plugins.OfType<IDisposable>())
            {
                d.Dispose();
            }
        }
    }
}
