using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Reminders.Core.Conditions.Configuration
{
    public class CompositeConditionCommandArgs : CherryCommandArgs
    {
        public CompositeCondition CompositeCondition { get; protected set; }

        public CompositeConditionCommandArgs(CompositeCondition compositeCondition)
        {
            this.CompositeCondition = compositeCondition;
        }
    }
}
