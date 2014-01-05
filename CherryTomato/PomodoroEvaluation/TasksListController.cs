using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation
{
    public class TasksListController
    {
        private enum ViewState
        {
            [Description("Task Name Only")]
            TaskName,

            [Description("Process Name Only")]
            ProcessName,

            [Description("Both Task and Process Names")]
            TaskAndProcessNames,
        }

        private const string processNameColumnName = "Process";
        private const string taskNameColumnName = "AppSite";

        private TasksListControl control;
        private CompletedPomodoro pomodoroData;
        private ViewState currentViewState = ViewState.TaskName; // the default value is Task name only

        public TasksListController(TasksListControl control)
        {
            this.control = control;
            if (this.control != null)
            {
                this.control.GridControl.SelectionChanged += this.GridControl_SelectionChanged;
                this.control.ViewButton.Click += this.ViewButton_Click;
                this.control.ViewButtonMenu.Items.AddRange(
                    Helpers.GetEnumValuesDescriptionPairs<ViewState>().
                    Select(pair =>
                    {
                        var menuItem = new ToolStripMenuItem(pair.Value);
                        menuItem.Tag = pair.Key;
                        menuItem.Click += this.menuItem_Click;
                        return menuItem;
                    }).ToArray());
                this.SetupTableView();
            }
        }

        public event Action<IEnumerable<TaskRegistration>> SelectionChanged;

        public void PopulateData()
        {
            this.control.GridControl.Rows.Clear();

            foreach (var app in this.GetSortedTaskRegistrations())
            {
                this.control.GridControl.Rows.Add(this.GetRowObjects(app));
            }
        }

        public void ClearSelection()
        {
            this.control.GridControl.ClearSelection();
        }

        public IEnumerable<TaskRegistration> GetSortedTaskRegistrations()
        {
            if (this.pomodoroData == null)
            {
                return Enumerable.Empty<TaskRegistration>();
            }

            var appActivity = new Dictionary<string, TaskRegistration>();

            foreach (var appRegistration in this.pomodoroData.TaskRegistrations)
            {
                var key = this.currentViewState == ViewState.ProcessName ?
                    appRegistration.ProcessName : appRegistration.TaskName;

                if (!appActivity.ContainsKey(key))
                {
                    appActivity.Add(key, new TaskRegistration()
                    {
                        TaskName = appRegistration.TaskName,
                        ProcessName = appRegistration.ProcessName,
                    });
                }

                appActivity[key].Duration += appRegistration.Duration;
            }

            return appActivity.Values.OrderByDescending(app => app.Duration);
        }

        public void SetData(CompletedPomodoro data)
        {
            this.pomodoroData = data;
        }

        public string GetFormattedDuration(TimeSpan duration)
        {
            return string.Format(
                "{0:0.##}% ({1:00}:{2:00})",
                (int)(duration.TotalSeconds * 100.0 / this.pomodoroData.Duration.TotalSeconds),
                        duration.Minutes,
                        duration.Seconds);
        }

        public object[] GetRowObjects(TaskRegistration taskReg)
        {
            return new object[]
            { 
                taskReg.ProcessName, 
                taskReg.TaskName, 
                this.GetFormattedDuration(taskReg.Duration) 
            };
        }

        private void SetupTableView()
        {
            this.control.SuspendLayout();

            this.PopulateData();

            this.control.GridControl.Columns[taskNameColumnName].Visible =
                this.currentViewState == ViewState.TaskName || this.currentViewState == ViewState.TaskAndProcessNames;
            this.control.GridControl.Columns[processNameColumnName].Visible =
                this.currentViewState == ViewState.ProcessName || this.currentViewState == ViewState.TaskAndProcessNames;

            this.control.ResumeLayout();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem menuItem in this.control.ViewButtonMenu.Items)
            {
                menuItem.Checked = (ViewState)menuItem.Tag == this.currentViewState;
            }

            this.control.ViewButtonMenu.Show(this.control.ViewButton, new Point(this.control.ViewButton.Size));
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            var newState = (ViewState)(sender as ToolStripMenuItem).Tag;
            this.currentViewState = newState;
            this.SetupTableView();
        }

        private void GridControl_SelectionChanged(object sender, EventArgs e)
        {
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this.GetSelectedTaskRegistrations());
            }
        }

        private IEnumerable<TaskRegistration> GetSelectedTaskRegistrations()
        {
            HashSet<TaskRegistration> tasksList = new HashSet<TaskRegistration>();
            foreach (DataGridViewRow row in this.control.GridControl.SelectedRows)
            {
                string processName = null;
                if (this.currentViewState == ViewState.ProcessName ||
                    this.currentViewState == ViewState.TaskAndProcessNames)
                {
                    processName = row.Cells[processNameColumnName].Value as string;
                }

                string taskName = null;
                if (this.currentViewState == ViewState.TaskName ||
                    this.currentViewState == ViewState.TaskAndProcessNames)
                {
                    taskName = row.Cells[taskNameColumnName].Value as string;
                }

                foreach (var task in this.pomodoroData.TaskRegistrations)
                {
                    if (!string.IsNullOrEmpty(processName))
                    {
                        if (task.ProcessName != processName)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(taskName))
                    {
                        if (task.TaskName != taskName)
                        {
                            continue;
                        }
                    }

                    tasksList.Add(task);
                }
            }

            return tasksList;
        }
    }
}
