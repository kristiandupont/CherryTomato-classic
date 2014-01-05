using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Reminders.Core.Conditions
{
    public class ConditionCheckerPluginsRepository : IPlugin, ICherryCommandsProvider
    {
        private Dictionary<string, IConditionChecker> allConditionCheckerPlugins;

        public ConditionCheckerPluginsRepository()
        {
            this.getAllConditionCheckerPluginsCommand = new CherryCommand(
                "Get All Condition Checker Plugins",
                ca => this,
                "Returns ConditionCheckerPluginsRepository object, which is the list of IConditionChecker plugins.");
        }

        public string PluginName
        {
            get { return "Condition Checker Plugins Repository"; }
        }

        public IEnumerable<IConditionChecker> All
        {
            get { return this.allConditionCheckerPlugins.Values; }
        }

        public IConditionChecker GetPlugin(ICondition condition)
        {
            return this.GetPlugin(condition.TypeName);
        }

        public IConditionChecker GetPlugin(string conditionPluginTypeName)
        {
            return this.allConditionCheckerPlugins[conditionPluginTypeName];
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.allConditionCheckerPlugins = plugins.All.OfType<IConditionChecker>().ToDictionary(p => p.PluginName);
        }

        private CherryCommand getAllConditionCheckerPluginsCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getAllConditionCheckerPluginsCommand;
        }
    }
}
