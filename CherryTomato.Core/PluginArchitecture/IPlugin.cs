using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.PluginArchitecture
{
    public interface IPlugin
    {
        string PluginName { get; }
        void TieEvents(PluginRepository plugins);
    }
}
