namespace CherryTomato.Reminders.Core.Conditions
{
    public interface ICondition
    {
        string TypeName { get; }

        bool Enabled { get; set; }
    }
}
