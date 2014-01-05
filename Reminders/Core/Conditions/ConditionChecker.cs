using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.Core.Conditions
{
    public abstract class ConditionChecker : IConditionChecker
    {
        private IConditionController guiController;

        public string ConditionTypeName
        {
            get
            {
                return this.CreateCondition().TypeName;
            }
        }

        public string PluginName
        {
            get
            {
                return this.ConditionTypeName;
            }
        }

        public abstract bool IsTrue(ICondition condition);

        public abstract void TieEvents(PluginRepository plugins);

        public abstract void LoadConditionFromXml(ICondition condition, XmlElement conditionElement);

        public abstract void SaveConditionToXml(ICondition condition, XmlElement conditionElement);

        public abstract ICondition CreateCondition();

        protected abstract IConditionController CreateGuiControllerInternal();

        public IConditionController GetGuiController()
        {
            return this.guiController ?? (this.guiController = this.CreateGuiControllerInternal());
        }
    }
}
