using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation
{
    public class ActivityGraphController
    {
        private CompletedPomodoro pomodoroData;
        private bool enabled = true;

        private ActivityGraphControl control;
        private TasksListController tasksController;
        private ChartRenderer chartRenderer;

        public ActivityGraphController(ActivityGraphControl control, TasksListController tasksController)
        {
            this.control = control;
            this.tasksController = tasksController;
            this.chartRenderer = new ChartRenderer();

            this.control.Resize += this.ControlResized;
            this.control.Panel.Paint += this.PaintPanel;
            this.tasksController.SelectionChanged += this.tasksController_SelectionChanged;
        }

        public void PopulateData()
        {
            if (!this.enabled) return;

            this.control.KeyboardLabel.Text = "Keyboard activity: " + pomodoroData.KeyboardActivity.Sum();
            this.control.MouseLabel.Text = "Mouse activity: " + pomodoroData.MouseActivity.Sum();
        }

        public void SetData(CompletedPomodoro data)
        {
            this.pomodoroData = data;
            this.chartRenderer.SetData(data);
        }

        public void Disable()
        {
            this.control.Visible = false;
            this.control.Resize -= this.ControlResized;
            this.control.Panel.Paint -= this.PaintPanel;
        }

        private void PaintPanel(object sender, PaintEventArgs e)
        {
            if (this.pomodoroData == null)
            {
                return;
            }

            chartRenderer.Render(e.Graphics, this.control.Panel.Size);
        }

        private void ControlResized(object sender, EventArgs e)
        {
            this.control.Panel.Invalidate();
        }

        private void tasksController_SelectionChanged(IEnumerable<TaskRegistration> tasks)
        {
            if (!this.enabled) return;

            this.chartRenderer.SetHighlightedTasks(tasks);

            this.control.Panel.Invalidate();
        }
    }
}
