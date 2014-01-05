using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using System.Xml;

namespace CherryTomato.TestsCore
{
    public class XmlConfigurationHelper
    {
        private XmlDocument document;
        private ConfigurePluginEventArgs eventArgs;
        private XmlElement pluginsElement;

        public XmlConfigurationHelper()
        {
            this.document = new XmlDocument();

            var root = this.document.CreateElement("cherryTomato");
            this.document.AppendChild(root);

            var config = this.document.CreateElement("config");
            root.AppendChild(config);

            this.pluginsElement = this.document.CreateElement("plugins");
            config.AppendChild(this.pluginsElement);
        }

        public ConfigurePluginEventArgs GetEventArgs()
        {
            if (this.eventArgs == null)
            {
                this.eventArgs = new ConfigurePluginEventArgs(this.document);
            }

            return this.eventArgs;
        }

        public XmlElement GetPluginsElement()
        {
            return this.pluginsElement;
        }
    }
}
