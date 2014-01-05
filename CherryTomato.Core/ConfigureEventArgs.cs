using System.Xml;
using CherryTomato.Core.EventsModel;
using System.Collections.Generic;
using System.Linq;

namespace CherryTomato.Core
{
    public class ConfigureEventArgs : CherryEventArgs
    {
        public XmlDocument Document { get; protected set; }

        public XmlElement RootNode { get; protected set; }

        public XmlElement ConfigNode { get; protected set; }

        public ConfigureEventArgs(XmlDocument configDocument)
        {
            this.Document = configDocument;
            this.RootNode = this.Document.SelectSingleNode("//cherryTomato") as XmlElement;
            this.ConfigNode = this.RootNode.SelectSingleNode("config") as XmlElement;
        }
    }
}
