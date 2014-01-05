using System.Collections.Generic;
using System.Linq;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Notifications
{
    public class NotifyPluginsRepository : IPlugin, ICherryCommandsProvider
    {
        private Dictionary<string, INotifier> allNotifyPlugins;

        public NotifyPluginsRepository()
        {
            this.getAllNotifyPluginsCommand = new CherryCommand(
                "Get All Notify Plugins",
                ca => this,
                "Returns NotifyPluginsRepository object, which is the list of INotifier plugins.");
        }

        public IEnumerable<INotifier> All
        {
            get { return this.allNotifyPlugins.Values; }
        }

        public INotifier GetPlugin(INotification notification)
        {
            return this.GetPlugin(notification.TypeName);
        }

        public INotifier GetPlugin(string notifyPluginTypeName)
        {
            return this.allNotifyPlugins[notifyPluginTypeName];
        }

        public string PluginName
        {
            get { return "Notify Plugins Repository"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.allNotifyPlugins = plugins.All.OfType<INotifier>().ToDictionary(p => p.PluginName);
        }

        private CherryCommand getAllNotifyPluginsCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getAllNotifyPluginsCommand;
        }
    }
}
