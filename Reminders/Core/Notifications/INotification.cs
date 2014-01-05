namespace CherryTomato.Reminders.Core.Notifications
{
    public interface INotification
    {
        string TypeName { get; }
        bool Enabled { get; set; }
    }
}
