using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation
{
    public class RatingController
    {
        private RatingControl control;
        private CompletedPomodoro pomodoroData;
        private TasksListController tasksListController;

        public RatingController(RatingControl ratingControl, TasksListController tasksListController)
        {
            this.control = ratingControl;
            this.control.TrackBarControl.Scroll += this.trackBar_Scroll;
            this.tasksListController = tasksListController;
            this.tasksListController.SelectionChanged += this.tasksListController_SelectionChanged;
        }

        public int Rating
        {
            get
            {
                return this.pomodoroData.Rating;
            }

            protected set
            {
                this.pomodoroData.Rating = value;
                this.control.RatingLabel.Text = value.ToString();
            }
        }

        public void SetData(CompletedPomodoro data)
        {
            this.pomodoroData = data;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            this.Rating = this.control.TrackBarControl.Value / 10;
        }

        private void tasksListController_SelectionChanged(IEnumerable<TaskRegistration> tasks)
        {
            double sum = tasks.Select(t => t.Duration.TotalMilliseconds).Sum();
            int newScrollValue = (int)(Math.Round(sum / this.pomodoroData.Duration.TotalMilliseconds * 100));
            newScrollValue = newScrollValue < this.control.TrackBarControl.Minimum ? 
                this.control.TrackBarControl.Minimum : newScrollValue;
            int newRating = (int)(Math.Round(newScrollValue / 10.0));
            this.control.TrackBarControl.Value = newScrollValue;
            this.Rating = newRating;
        }
    }
}
