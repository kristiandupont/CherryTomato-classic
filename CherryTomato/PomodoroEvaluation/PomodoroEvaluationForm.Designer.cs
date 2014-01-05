namespace CherryTomato.PomodoroEvaluation
{
    partial class PomodoroEvaluationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PomodoroEvaluationForm));
            this.approveButton = new System.Windows.Forms.Button();
            this.voidButton = new System.Windows.Forms.Button();
            this.headerLabel = new System.Windows.Forms.Label();
            this.taggingControl = new CherryTomato.PomodoroEvaluation.TaggingControl();
            this.ratingControl = new CherryTomato.PomodoroEvaluation.RatingControl();
            this.tasksListControl = new CherryTomato.PomodoroEvaluation.TasksListControl();
            this.activityGraphControl = new CherryTomato.PomodoroEvaluation.ActivityGraphControl();
            this.outOfPomodoroReminderControl = new CherryTomato.PomodoroEvaluation.OutOfPomodoroReminderControl();
            this.SuspendLayout();
            // 
            // approveButton
            // 
            this.approveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.approveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.approveButton.Location = new System.Drawing.Point(725, 443);
            this.approveButton.Name = "approveButton";
            this.approveButton.Size = new System.Drawing.Size(75, 23);
            this.approveButton.TabIndex = 14;
            this.approveButton.Text = "&Approve";
            this.approveButton.UseVisualStyleBackColor = true;
            this.approveButton.Click += new System.EventHandler(this.approveButton_Click);
            // 
            // voidButton
            // 
            this.voidButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.voidButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.voidButton.Location = new System.Drawing.Point(806, 443);
            this.voidButton.Name = "voidButton";
            this.voidButton.Size = new System.Drawing.Size(75, 23);
            this.voidButton.TabIndex = 15;
            this.voidButton.Text = "&Void";
            this.voidButton.UseVisualStyleBackColor = true;
            this.voidButton.Click += new System.EventHandler(this.voidButton_Click);
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Location = new System.Drawing.Point(12, 9);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(269, 13);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Pomodoro completed. Start time: 00:00, end time: 00:00";
            // 
            // taggingControl
            // 
            this.taggingControl.Location = new System.Drawing.Point(9, 232);
            this.taggingControl.Margin = new System.Windows.Forms.Padding(0);
            this.taggingControl.Name = "taggingControl";
            this.taggingControl.Size = new System.Drawing.Size(156, 204);
            this.taggingControl.TabIndex = 33;
            this.taggingControl.Visible = false;
            // 
            // ratingControl
            // 
            this.ratingControl.Location = new System.Drawing.Point(9, 44);
            this.ratingControl.Margin = new System.Windows.Forms.Padding(0);
            this.ratingControl.Name = "ratingControl";
            this.ratingControl.Size = new System.Drawing.Size(168, 161);
            this.ratingControl.TabIndex = 32;
            // 
            // tasksListControl
            // 
            this.tasksListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tasksListControl.Location = new System.Drawing.Point(178, 232);
            this.tasksListControl.Name = "tasksListControl";
            this.tasksListControl.Size = new System.Drawing.Size(706, 205);
            this.tasksListControl.TabIndex = 31;
            // 
            // activityGraphControl
            // 
            this.activityGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.activityGraphControl.Location = new System.Drawing.Point(178, 42);
            this.activityGraphControl.Margin = new System.Windows.Forms.Padding(0);
            this.activityGraphControl.Name = "activityGraphControl";
            this.activityGraphControl.Size = new System.Drawing.Size(704, 179);
            this.activityGraphControl.TabIndex = 30;
            // 
            // outOfPomodoroReminderControl
            // 
            this.outOfPomodoroReminderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.outOfPomodoroReminderControl.Location = new System.Drawing.Point(375, 442);
            this.outOfPomodoroReminderControl.Name = "outOfPomodoroReminderControl";
            this.outOfPomodoroReminderControl.Size = new System.Drawing.Size(333, 35);
            this.outOfPomodoroReminderControl.TabIndex = 34;
            // 
            // PomodoroEvaluationForm
            // 
            this.AcceptButton = this.approveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.voidButton;
            this.ClientSize = new System.Drawing.Size(893, 478);
            this.Controls.Add(this.outOfPomodoroReminderControl);
            this.Controls.Add(this.taggingControl);
            this.Controls.Add(this.ratingControl);
            this.Controls.Add(this.tasksListControl);
            this.Controls.Add(this.activityGraphControl);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.voidButton);
            this.Controls.Add(this.approveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PomodoroEvaluationForm";
            this.Text = "Evaluate Pomodoro - cherrytomato";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button approveButton;
        private System.Windows.Forms.Button voidButton;
        private System.Windows.Forms.Label headerLabel;
        private ActivityGraphControl activityGraphControl;
        private TasksListControl tasksListControl;
        private RatingControl ratingControl;
        private TaggingControl taggingControl;
        private OutOfPomodoroReminderControl outOfPomodoroReminderControl;
    }
}