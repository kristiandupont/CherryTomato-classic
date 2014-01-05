using System.Collections.Generic;
using System.Linq;
using System.Xml;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Conditions
{
    public class CompositeCondition
    {
        private Dictionary<string, ICondition> allConditions = new Dictionary<string, ICondition>();
        private ConditionCheckerPluginsRepository conditionPlugins;

        public bool IsTrue
        {
            get
            {
                return this.allConditions.Values.
                    Where(c => c.Enabled).
                    All(c => this.conditionPlugins.GetPlugin(c.TypeName).IsTrue(c));
            }
        }


        public ICondition GetCondition(string typeName)
        {
            return this.allConditions[typeName];
        }

        public CompositeCondition(PluginRepository plugins, params ICondition[] conditions)
        {
            this.conditionPlugins = plugins.CherryCommands["Get All Condition Checker Plugins"].Do(null) as ConditionCheckerPluginsRepository;

            foreach (var n in this.conditionPlugins.All)
            {
                this.allConditions[n.ConditionTypeName] = n.CreateCondition();
            }

            foreach (var condition in conditions)
            {
                this.allConditions[condition.TypeName] = condition;
            }
        }

        public void Load(XmlElement compositeConditionNode)
        {
            if (compositeConditionNode == null)
            {
                return;
            }

            var conditionNodes = compositeConditionNode.SelectNodes("condition");

            foreach (XmlElement conditionElement in conditionNodes)
            {
                var typeName = conditionElement.GetAttribute("typeName");
                var conditionPlugin = this.conditionPlugins.GetPlugin(typeName);
                var condition = conditionPlugin.CreateCondition();

                condition.Enabled = bool.Parse(conditionElement.GetAttribute("enabled"));
                conditionPlugin.LoadConditionFromXml(condition, conditionElement);

                this.allConditions[typeName] = condition;
            }
        }

        public void Save(XmlElement parentElement)
        {
            var compositeConditionElement = parentElement.OwnerDocument.CreateElement("compositeCondition");
            parentElement.AppendChild(compositeConditionElement);

            foreach (var conditionType in this.allConditions.Keys)
            {
                var conditionPlugin = this.conditionPlugins.GetPlugin(conditionType);
                var conditionElement = compositeConditionElement.OwnerDocument.CreateElement("condition");
                conditionElement.SetAttribute("typeName", conditionPlugin.ConditionTypeName);
                var condition = this.allConditions[conditionType];

                conditionElement.SetAttribute("enabled", condition.Enabled.ToString());
                conditionPlugin.SaveConditionToXml(condition, conditionElement);

                compositeConditionElement.AppendChild(conditionElement);
            }
        }

        public void AddCondition(ICondition condition)
        {
            // Replace existing condition with the new one
            this.allConditions[condition.TypeName] = condition;
        }
    }
}
