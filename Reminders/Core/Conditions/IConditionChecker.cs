using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions.Configuration;

namespace CherryTomato.Reminders.Core.Conditions
{
    public interface IConditionChecker : IPlugin
    {
        string ConditionTypeName { get; }
        bool IsTrue(ICondition condition);

        void LoadConditionFromXml(ICondition condition, XmlElement fromElement);
        void SaveConditionToXml(ICondition condition, XmlElement parentElement);

        ICondition CreateCondition();

        IConditionController GetGuiController();
    }
}
