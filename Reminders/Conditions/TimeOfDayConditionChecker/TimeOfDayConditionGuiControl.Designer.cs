namespace CherryTomato.Reminders.TimeOfDayConditionChecker
{
    partial class TimeOfDayConditionGuiControl
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
            this.showPopupGroupBox = new System.Windows.Forms.GroupBox();
            this.checkEnabled = new System.Windows.Forms.CheckBox();
            this.rbtnWithin = new System.Windows.Forms.RadioButton();
            this.rbtnOut = new System.Windows.Forms.RadioButton();
            this.timeStart = new System.Windows.Forms.DateTimePicker();
            this.timeEnd = new System.Windows.Forms.DateTimePicker();
            this.showPopupGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPopupGroupBox
            // 
            this.showPopupGroupBox.Controls.Add(this.timeEnd);
            this.showPopupGroupBox.Controls.Add(this.timeStart);
            this.showPopupGroupBox.Controls.Add(this.rbtnOut);
            this.showPopupGroupBox.Controls.Add(this.rbtnWithin);
            this.showPopupGroupBox.Controls.Add(this.checkEnabled);
            this.showPopupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showPopupGroupBox.Location = new System.Drawing.Point(0, 0);
            this.showPopupGroupBox.Name = "showPopupGroupBox";
            this.showPopupGroupBox.Size = new System.Drawing.Size(523, 70);
            this.showPopupGroupBox.TabIndex = 20;
            this.showPopupGroupBox.TabStop = false;
            // 
            // checkEnabled
            // 
            this.checkEnabled.AutoSize = true;
            this.checkEnabled.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkEnabled.Location = new System.Drawing.Point(6, 0);
            this.checkEnabled.Name = "checkEnabled";
            this.checkEnabled.Size = new System.Drawing.Size(114, 17);
            this.checkEnabled.TabIndex = 9;
            this.checkEnabled.Text = "Check time of day.";
            this.checkEnabled.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.checkEnabled.UseVisualStyleBackColor = false;
            // 
            // rbtnWithin
            // 
            this.rbtnWithin.AutoSize = true;
            this.rbtnWithin.Location = new System.Drawing.Point(4, 24);
            this.rbtnWithin.Name = "rbtnWithin";
            this.rbtnWithin.Size = new System.Drawing.Size(178, 17);
            this.rbtnWithin.TabIndex = 10;
            this.rbtnWithin.TabStop = true;
            this.rbtnWithin.Text = "Notify only within this time range:";
            this.rbtnWithin.UseVisualStyleBackColor = true;
            // 
            // rbtnOut
            // 
            this.rbtnOut.AutoSize = true;
            this.rbtnOut.Location = new System.Drawing.Point(4, 47);
            this.rbtnOut.Name = "rbtnOut";
            this.rbtnOut.Size = new System.Drawing.Size(178, 17);
            this.rbtnOut.TabIndex = 10;
            this.rbtnOut.TabStop = true;
            this.rbtnOut.Text = "Notify only out of this time range:";
            this.rbtnOut.UseVisualStyleBackColor = true;
            // 
            // timeStart
            // 
            this.timeStart.CustomFormat = "HH:mm";
            this.timeStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeStart.Location = new System.Drawing.Point(212, 36);
            this.timeStart.Name = "timeStart";
            this.timeStart.ShowUpDown = true;
            this.timeStart.Size = new System.Drawing.Size(61, 20);
            this.timeStart.TabIndex = 11;
            // 
            // timeEnd
            // 
            this.timeEnd.CustomFormat = "HH:mm";
            this.timeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeEnd.Location = new System.Drawing.Point(279, 36);
            this.timeEnd.Name = "timeEnd";
            this.timeEnd.ShowUpDown = true;
            this.timeEnd.Size = new System.Drawing.Size(61, 20);
            this.timeEnd.TabIndex = 11;
            // 
            // TimeOfDayConditionGuiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.showPopupGroupBox);
            this.Name = "TimeOfDayConditionGuiControl";
            this.Size = new System.Drawing.Size(523, 70);
            this.showPopupGroupBox.ResumeLayout(false);
            this.showPopupGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox showPopupGroupBox;
        public System.Windows.Forms.CheckBox checkEnabled;
        private System.Windows.Forms.RadioButton rbtnOut;
        private System.Windows.Forms.RadioButton rbtnWithin;
        private System.Windows.Forms.DateTimePicker timeStart;
        private System.Windows.Forms.DateTimePicker timeEnd;
    }
}
