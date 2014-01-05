using System;
using System.IO;
using System.Windows.Forms;
using CherryTomato.Core;

namespace CherryTomato
{
    public class CherryTomatoApplicationContext : ApplicationContext
    {
        private CherryService cherryService;

        public CherryTomatoApplicationContext(string configFilePath)
        {
            this.Launch(configFilePath);
        }

        private void Launch(string configFilePath)
        {
            Application.ApplicationExit += Application_ApplicationExit;

            this.cherryService = new CherryService();

            this.cherryService.PluginRepository.RegisterPlugins();

            if (configFilePath == null)
            {
                configFilePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"cherrytomato\settings.xml");
            }

            this.cherryService.InitializeCherryServiceEventsAndCommands();

            this.cherryService.PluginRepository.TieEvents();
            this.cherryService.LoadConfiguration(configFilePath);

            this.cherryService.Start();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.cherryService.SaveConfiguration();
            this.cherryService.Dispose();
        }
    }
}
