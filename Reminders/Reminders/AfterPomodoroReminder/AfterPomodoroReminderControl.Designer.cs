namespace CherryTomato.Reminders.AfterPomodoroReminder
{
    partial class AfterPomodoroReminderControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.remindAfterLabel = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.rbtnStarted = new System.Windows.Forms.RadioButton();
            this.rbtnRung = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // remindAfterLabel
            // 
            this.remindAfterLabel.AutoSize = true;
            this.remindAfterLabel.Location = new System.Drawing.Point(3, 9);
            this.remindAfterLabel.Name = "remindAfterLabel";
            this.remindAfterLabel.Size = new System.Drawing.Size(67, 13);
            this.remindAfterLabel.TabIndex = 11;
            this.remindAfterLabel.Text = "Remind after";
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.Location = new System.Drawing.Point(3, 38);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(44, 13);
            this.lblWait.TabIndex = 11;
            this.lblWait.Text = "Wait for";
            // 
            // numMinutes
            // 
            this.numMinutes.Location = new System.Drawing.Point(94, 34);
            this.numMinutes.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(47, 20);
            this.numMinutes.TabIndex = 13;
            this.numMinutes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(147, 38);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(46, 13);
            this.lblMinutes.TabIndex = 11;
            this.lblMinutes.Text = "minutes.";
            // 
            // rbtnStarted
            // 
            this.rbtnStarted.AutoSize = true;
            this.rbtnStarted.Location = new System.Drawing.Point(94, 1);
            this.rbtnStarted.Name = "rbtnStarted";
            this.rbtnStarted.Size = new System.Drawing.Size(130, 17);
            this.rbtnStarted.TabIndex = 14;
            this.rbtnStarted.TabStop = true;
            this.rbtnStarted.Text = "Pomodoro was started";
            this.rbtnStarted.UseVisualStyleBackColor = true;
            // 
            // rbtnRung
            // 
            this.rbtnRung.AutoSize = true;
            this.rbtnRung.Location = new System.Drawing.Point(230, 1);
            this.rbtnRung.Name = "rbtnRung";
            this.rbtnRung.Size = new System.Drawing.Size(134, 17);
            this.rbtnRung.TabIndex = 14;
            this.rbtnRung.TabStop = true;
            this.rbtnRung.Text = "Pomodoro was finished";
            this.rbtnRung.UseVisualStyleBackColor = true;
            // 
            // AfterPomodoroReminderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.rbtnRung);
            this.Controls.Add(this.rbtnStarted);
            this.Controls.Add(this.numMinutes);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.remindAfterLabel);
            this.Name = "AfterPomodoroReminderControl";
            this.Size = new System.Drawing.Size(529, 64);
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label remindAfterLabel;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.NumericUpDown numMinutes;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.RadioButton rbtnStarted;
        private System.Windows.Forms.RadioButton rbtnRung;
    }
}
