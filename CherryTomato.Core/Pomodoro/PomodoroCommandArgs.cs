using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.Pomodoro
{
    public class PomodoroCommandArgs : CherryCommandArgs
    {
        public CompletedPomodoro PomodoroData { get; protected set; }

        public PomodoroCommandArgs(CompletedPomodoro pomodoroData)
        {
            this.PomodoroData = pomodoroData;
        }
    }
}
