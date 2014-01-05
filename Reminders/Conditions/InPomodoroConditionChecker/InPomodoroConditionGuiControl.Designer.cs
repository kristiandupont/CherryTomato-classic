namespace CherryTomato.Reminders.InPomodoroConditionChecker
{
    partial class InPomodoroConditionGuiControl
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
            this.showPopupCheckBox = new System.Windows.Forms.CheckBox();
            this.rbtnShouldBeIn = new System.Windows.Forms.RadioButton();
            this.rbtnShouldBeOut = new System.Windows.Forms.RadioButton();
            this.showPopupGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPopupGroupBox
            // 
            this.showPopupGroupBox.Controls.Add(this.rbtnShouldBeOut);
            this.showPopupGroupBox.Controls.Add(this.rbtnShouldBeIn);
            this.showPopupGroupBox.Controls.Add(this.showPopupCheckBox);
            this.showPopupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showPopupGroupBox.Location = new System.Drawing.Point(0, 0);
            this.showPopupGroupBox.Name = "showPopupGroupBox";
            this.showPopupGroupBox.Size = new System.Drawing.Size(523, 47);
            this.showPopupGroupBox.TabIndex = 20;
            this.showPopupGroupBox.TabStop = false;
            // 
            // showPopupCheckBox
            // 
            this.showPopupCheckBox.AutoSize = true;
            this.showPopupCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.showPopupCheckBox.Location = new System.Drawing.Point(6, 0);
            this.showPopupCheckBox.Name = "showPopupCheckBox";
            this.showPopupCheckBox.Size = new System.Drawing.Size(133, 17);
            this.showPopupCheckBox.TabIndex = 9;
            this.showPopupCheckBox.Text = "Check pomodoro state";
            this.showPopupCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.showPopupCheckBox.UseVisualStyleBackColor = false;
            // 
            // rbtnShouldBeIn
            // 
            this.rbtnShouldBeIn.AutoSize = true;
            this.rbtnShouldBeIn.Location = new System.Drawing.Point(4, 24);
            this.rbtnShouldBeIn.Name = "rbtnShouldBeIn";
            this.rbtnShouldBeIn.Size = new System.Drawing.Size(134, 17);
            this.rbtnShouldBeIn.TabIndex = 10;
            this.rbtnShouldBeIn.TabStop = true;
            this.rbtnShouldBeIn.Text = "Should be in pomodoro";
            this.rbtnShouldBeIn.UseVisualStyleBackColor = true;
            // 
            // rbtnShouldBeOut
            // 
            this.rbtnShouldBeOut.AutoSize = true;
            this.rbtnShouldBeOut.Location = new System.Drawing.Point(270, 24);
            this.rbtnShouldBeOut.Name = "rbtnShouldBeOut";
            this.rbtnShouldBeOut.Size = new System.Drawing.Size(153, 17);
            this.rbtnShouldBeOut.TabIndex = 10;
            this.rbtnShouldBeOut.TabStop = true;
            this.rbtnShouldBeOut.Text = "Should be out of pomodoro";
            this.rbtnShouldBeOut.UseVisualStyleBackColor = true;
            // 
            // InPomodoroConditionGuiController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.showPopupGroupBox);
            this.Name = "InPomodoroConditionGuiController";
            this.Size = new System.Drawing.Size(523, 47);
            this.showPopupGroupBox.ResumeLayout(false);
            this.showPopupGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox showPopupGroupBox;
        public System.Windows.Forms.CheckBox showPopupCheckBox;
        private System.Windows.Forms.RadioButton rbtnShouldBeOut;
        private System.Windows.Forms.RadioButton rbtnShouldBeIn;
    }
}
