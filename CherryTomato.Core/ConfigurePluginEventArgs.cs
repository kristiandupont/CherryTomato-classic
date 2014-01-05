using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CherryTomato.Core
{
    public class ConfigurePluginEventArgs : ConfigureEventArgs
    {
        public XmlElement PluginsNode { get; protected set; }

        private Dictionary<string, XmlElement> PluginNodes { get; set; }

        public ConfigurePluginEventArgs(XmlDocument configDocument)
            : base(configDocument)
        {
            if (this.ConfigNode != null)
            {
                this.PluginsNode = this.ConfigNode.SelectSingleNode("plugins") as XmlElement;
            }
        }

        public XmlElement GetMyNode(string pluginName)
        {
            if (this.PluginNodes == null)
            {
                this.PluginNodes = this.PluginsNode.
                    SelectNodes("plugin").
                    Cast<XmlElement>().
                    ToDictionary(node => node.GetAttribute("name"));
            }

            if (!this.PluginNodes.ContainsKey(pluginName))
            {
                return null;
            }

            return this.PluginNodes[pluginName];
        }

        public XmlElement CreateNewPluginNode(string pluginName)
        {
            var node = this.Document.CreateElement("plugin");
            node.SetAttribute("name", pluginName);
            this.PluginsNode.AppendChild(node);
            return node;
        }
    }
}
