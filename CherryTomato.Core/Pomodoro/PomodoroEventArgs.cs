using CherryTomato.Core.EventsModel;

namespace CherryTomato.Core.Pomodoro
{
    public class PomodoroEventArgs : CherryEventArgs
    {
        public CompletedPomodoro PomodoroData { get; protected set; }

        public PomodorosProductivity ProductivityData { get; protected set; }

        public RunningPomodoro RunnigPomodoroData { get; protected set; }

        public PomodoroEventArgs(CompletedPomodoro pomodoroData)
        {
            this.PomodoroData = pomodoroData;
        }

        public PomodoroEventArgs(PomodorosProductivity productivityData)
        {
            this.ProductivityData = productivityData;
        }

        public PomodoroEventArgs(RunningPomodoro runningPomodoroData)
        {
            this.RunnigPomodoroData = runningPomodoroData;
        }
    }
}
