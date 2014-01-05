using System;

namespace CherryTomato.Core.Pomodoro
{
    public class RunningPomodoro
    {
        public RunningPomodoro()
        {
        }

        public bool IsInPomodoro { get; set; }

        /// <summary>
        /// The total length of running pomodoro.
        /// </summary>
        public TimeSpan ScheduledPomodoroDuration { get; set; }

        /// <summary>
        /// The time spent after the last pomodoro ended.
        /// </summary>
        public TimeSpan OutOfPomodoro { get; set; }

        /// <summary>
        /// The pomodoro started time.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// The time elapsed since pomodoro start.
        /// </summary>
        public TimeSpan Elapsed { get; set; }

        /// <summary>
        /// The time remaining for running pomodor.
        /// </summary>
        public TimeSpan Remaining
        {
            get
            {
                return this.ScheduledPomodoroDuration - this.Elapsed;
            }
        }

        /// <summary>
        /// Rounded up number of minutes left in current pomodoro. In case out of pomodoro returns 0.
        /// </summary>
        public int MinutesLeft
        {
            get
            {
                if (!this.IsInPomodoro)
                {
                    return 0;
                }

                return (int)this.Remaining.Add(TimeSpan.FromMilliseconds(-1)).TotalMinutes + 1;
            }
        }
    }
}
