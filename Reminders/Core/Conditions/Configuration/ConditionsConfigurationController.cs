using System.Collections.Generic;
using System.Linq;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Conditions.Configuration
{
    public class ConditionsConfigurationController : IPlugin, ICherryCommandsProvider
    {
        private ConditionsConfigurationPanel panel;

        public ConditionsConfigurationController()
        {
            this.getConditionsConfigutationControl = new CherryCommand(
                "Get Conditions Configuration Control",
                ca => this.Panel,
                "Returns Control which edits all kind of notify conditions.");
            this.populateConditionsConfigutation = new CherryCommand(
                "Populate Conditions Configuration",
                ca => this.Populate((ca as CompositeConditionCommandArgs).CompositeCondition),
                "Update data on the UI panel with given conditions.");
            this.saveConditionsConfigutation = new CherryCommand(
                "Save Conditions Configuration",
                ca => this.SaveToCompositeCondition((ca as CompositeConditionCommandArgs).CompositeCondition),
                "Store all user input data to the conditions object.");
        }

        public ConditionsConfigurationPanel Panel
        {
            get
            {
                if (this.panel == null)
                {
                    this.panel = new ConditionsConfigurationPanel(
                        this.GetAllConditionCheckers().All.
                        Select(n => n.GetGuiController().GetSettingsPanel()));
                }

                return this.panel;
            }
        }

        private ICherryCommand getAllConditionCheckerPluginsCommand;

        private ConditionCheckerPluginsRepository GetAllConditionCheckers()
        {
            return this.getAllConditionCheckerPluginsCommand.Do(null) as ConditionCheckerPluginsRepository;
        }

        public bool SaveToCompositeCondition(CompositeCondition compositeCondition)
        {
            foreach (var conditionChecker in this.GetAllConditionCheckers().All)
            {
                conditionChecker.GetGuiController().SaveSettingsToCondition(
                    compositeCondition.GetCondition(conditionChecker.ConditionTypeName));
            }

            return true;
        }

        public bool Populate(CompositeCondition compositeCondition)
        {
            foreach (var conditionChecker in this.GetAllConditionCheckers().All)
            {
                conditionChecker.GetGuiController().LoadSettingsFromCondition(
                    compositeCondition.GetCondition(conditionChecker.ConditionTypeName));
            }

            return true;
        }

        public string PluginName
        {
            get { return "Conditions Configuration Controller"; }
        }

        private PluginRepository plugins;

        public void TieEvents(PluginRepository plugins)
        {
            this.plugins = plugins;
            this.getAllConditionCheckerPluginsCommand = plugins.CherryCommands["Get All Condition Checker Plugins"];
        }

        private CherryCommand getConditionsConfigutationControl;
        private CherryCommand populateConditionsConfigutation;
        private CherryCommand saveConditionsConfigutation;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getConditionsConfigutationControl;
            yield return this.populateConditionsConfigutation;
            yield return this.saveConditionsConfigutation;
        }
    }
}
