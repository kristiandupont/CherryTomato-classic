using System.Windows.Forms;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.Core.Conditions.Configuration
{
    public interface IConditionController
    {
        void LoadSettingsFromCondition(ICondition condition);
        void SaveSettingsToCondition(ICondition condition);
        Control GetSettingsPanel();
    }
}
