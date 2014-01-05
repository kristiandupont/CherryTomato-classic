using System.Linq;
using System.Collections.Generic;

namespace CherryTomato.Core.Pomodoro
{
    /// <summary>
    /// Represents several pomodoros productivity.
    /// </summary>
    public class PomodorosProductivity
    {
        public PomodorosProductivity()
        {
        }

        public PomodorosProductivity(IEnumerable<PomodoroRegistration> pomodoros)
	    {
            this.Rating = pomodoros.Select(p => p.Rating).Sum();
            this.Pomodoros = pomodoros.Where(p => p.Rating != 0).Count();
        }

        public PomodorosProductivity(int pomodoros, int rating)
        {
            this.Pomodoros = pomodoros;
            this.Rating = rating;
        }

        /// <summary>
        /// Number of pomodoros.
        /// </summary>
        public int Pomodoros { get; set; }

        /// <summary>
        /// The pomodoros total productivity index.
        /// </summary>
        public int Rating { get; set; }
    }
}
