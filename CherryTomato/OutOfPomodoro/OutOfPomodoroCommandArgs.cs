using CherryTomato.Core.CommandsModel;

namespace CherryTomato.OutOfPomodoro
{
    public class OutOfPomodoroCommandArgs : CherryCommandArgs
    {
        public int RemindAtMinutes { get; protected set; }

        /// <summary>
        /// Create command object with all necessary command data.
        /// </summary>
        /// <param name="remindAtMinutes">The minutes to remind after.</param>
        public OutOfPomodoroCommandArgs(int remindAtMinutes)
        {
            this.RemindAtMinutes = remindAtMinutes;
        }
    }
}
