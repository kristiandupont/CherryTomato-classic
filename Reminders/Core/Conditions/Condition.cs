namespace CherryTomato.Reminders.Core.Conditions
{
    public abstract class Condition : ICondition
    {
        public string TypeName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public virtual bool Enabled { get; set; }
    }
}
