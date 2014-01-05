namespace CherryTomato.PomodoroEvaluation
{
    partial class OutOfPomodoroReminderControl
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
            this.offCheckBox = new System.Windows.Forms.RadioButton();
            this.fifteenMinutesCheckBox = new System.Windows.Forms.RadioButton();
            this.tenMinutesCheckBox = new System.Windows.Forms.RadioButton();
            this.fiveMinutesCheckBox = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // offCheckBox
            // 
            this.offCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.offCheckBox.AutoSize = true;
            this.offCheckBox.Checked = true;
            this.offCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.offCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.offCheckBox.Location = new System.Drawing.Point(295, 5);
            this.offCheckBox.Name = "offCheckBox";
            this.offCheckBox.Size = new System.Drawing.Size(33, 25);
            this.offCheckBox.TabIndex = 32;
            this.offCheckBox.TabStop = true;
            this.offCheckBox.Text = "Off";
            this.offCheckBox.UseVisualStyleBackColor = true;
            // 
            // fifteenMinutesCheckBox
            // 
            this.fifteenMinutesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fifteenMinutesCheckBox.AutoSize = true;
            this.fifteenMinutesCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.fifteenMinutesCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fifteenMinutesCheckBox.Location = new System.Drawing.Point(260, 5);
            this.fifteenMinutesCheckBox.Name = "fifteenMinutesCheckBox";
            this.fifteenMinutesCheckBox.Size = new System.Drawing.Size(31, 25);
            this.fifteenMinutesCheckBox.TabIndex = 33;
            this.fifteenMinutesCheckBox.Text = "15";
            this.fifteenMinutesCheckBox.UseVisualStyleBackColor = true;
            // 
            // tenMinutesCheckBox
            // 
            this.tenMinutesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.tenMinutesCheckBox.AutoSize = true;
            this.tenMinutesCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tenMinutesCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tenMinutesCheckBox.Location = new System.Drawing.Point(225, 5);
            this.tenMinutesCheckBox.Name = "tenMinutesCheckBox";
            this.tenMinutesCheckBox.Size = new System.Drawing.Size(31, 25);
            this.tenMinutesCheckBox.TabIndex = 34;
            this.tenMinutesCheckBox.Text = "10";
            this.tenMinutesCheckBox.UseVisualStyleBackColor = true;
            // 
            // fiveMinutesCheckBox
            // 
            this.fiveMinutesCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.fiveMinutesCheckBox.AutoSize = true;
            this.fiveMinutesCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.fiveMinutesCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fiveMinutesCheckBox.Location = new System.Drawing.Point(194, 5);
            this.fiveMinutesCheckBox.Name = "fiveMinutesCheckBox";
            this.fiveMinutesCheckBox.Size = new System.Drawing.Size(25, 25);
            this.fiveMinutesCheckBox.TabIndex = 31;
            this.fiveMinutesCheckBox.Text = "5";
            this.fiveMinutesCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Remind me to start another pomodoro:";
            // 
            // OutOfPomodoroReminderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.offCheckBox);
            this.Controls.Add(this.fifteenMinutesCheckBox);
            this.Controls.Add(this.tenMinutesCheckBox);
            this.Controls.Add(this.fiveMinutesCheckBox);
            this.Controls.Add(this.label1);
            this.Name = "OutOfPomodoroReminderControl";
            this.Size = new System.Drawing.Size(333, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton offCheckBox;
        private System.Windows.Forms.RadioButton fifteenMinutesCheckBox;
        private System.Windows.Forms.RadioButton tenMinutesCheckBox;
        private System.Windows.Forms.RadioButton fiveMinutesCheckBox;
        private System.Windows.Forms.Label label1;
    }
}
