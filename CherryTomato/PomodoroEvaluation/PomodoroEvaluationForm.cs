using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.PomodoroEvaluation
{
    public partial class PomodoroEvaluationForm : Form
    {
        private PluginRepository pluginRepository;

        private CompletedPomodoro PomodoroData { get; set; }

        private ActivityGraphController graphController;
        private TasksListController tasksController;
        private RatingController ratingController;
        private TaggingController taggingController;
        private OutOfPomodoroReminderController reminderController;

        public PomodoroEvaluationForm(PluginRepository pluginRepository)
        {
            this.pluginRepository = pluginRepository;

            this.InitializeComponent();

            this.tasksController = new TasksListController(this.tasksListControl);
            this.graphController = new ActivityGraphController(this.activityGraphControl, this.tasksController);
            this.ratingController = new RatingController(this.ratingControl, this.tasksController);
            this.taggingController = new TaggingController(this.taggingControl, this);
            this.reminderController = new OutOfPomodoroReminderController(
                this.outOfPomodoroReminderControl, 
                this.pluginRepository);

            if (!(bool)this.pluginRepository.CherryCommands["Is User Activity Sensor Enabled"].Do(null))
            {
                this.OverlapGraphWithTasksList();
            }

            // Create a handle to allow invoke(show).
            this.CreateHandle();
        }

        public void DisableDefaultAcceptButton()
        {
            this.AcceptButton = null;
        }

        public void EnableDefaultAcceptButton()
        {
            this.AcceptButton = this.approveButton;
        }

        public void SetData(CompletedPomodoro data)
        {
            this.UpdateControl(
                () =>
                {
                    this.PomodoroData = data;
                    this.graphController.SetData(this.PomodoroData);
                    this.tasksController.SetData(this.PomodoroData);
                    this.ratingController.SetData(this.PomodoroData);
                    this.taggingController.SetData(this.PomodoroData);

                    this.PopulateData();
                });
        }

        private void OverlapGraphWithTasksList()
        {
            this.SuspendLayout();

            this.graphController.Disable();
            var sizeY =
                this.tasksListControl.Location.Y -
                this.activityGraphControl.Location.Y +
                this.tasksListControl.Size.Height;
            this.tasksListControl.Location = this.activityGraphControl.Location;
            this.tasksListControl.Size = new Size(this.tasksListControl.Size.Width, sizeY);

            this.ResumeLayout();
        }

        private void PopulateData()
        {
            this.graphController.PopulateData();
            this.tasksController.PopulateData();
            this.taggingController.PopulateData();
            this.PopulateLabels();
        }

        private void PopulateLabels()
        {
            this.headerLabel.Text = string.Format(
                "Pomodoro completed. Start time: {0}, end time: {1}",
                this.PomodoroData.Start.ToShortTimeString(), 
                this.PomodoroData.End.ToShortTimeString());
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            // This is dirty hack.
            // The problem is the DataGridView selectes first row no matter what!
            // The only way to not highlight rows - drop the selection.
            if (this.Visible)
            {
                // When the window appears (became visible) let's clear rows selection.
                this.tasksController.ClearSelection();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void approveButton_Click(object sender, EventArgs e)
        {
            var pomodoroSensor = this.pluginRepository.CherryCommands["Approve Pomodoro"].Do(
                new PomodoroCommandArgs(this.PomodoroData));

            this.reminderController.PopulateData();
            this.Close();
        }

        private void voidButton_Click(object sender, EventArgs e)
        {
            this.reminderController.PopulateData();
            this.Close();
        }
    }
}