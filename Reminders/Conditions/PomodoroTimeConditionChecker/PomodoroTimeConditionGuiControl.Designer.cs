namespace CherryTomato.Reminders.PomodoroTimeConditionChecker
{
    partial class PomodoroTimeConditionGuiControl
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
            this.rbtnLess = new System.Windows.Forms.RadioButton();
            this.rbtnMore = new System.Windows.Forms.RadioButton();
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numMinutes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtnStart = new System.Windows.Forms.RadioButton();
            this.rbtnFinish = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.showPopupGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPopupGroupBox
            // 
            this.showPopupGroupBox.Controls.Add(this.panel2);
            this.showPopupGroupBox.Controls.Add(this.panel1);
            this.showPopupGroupBox.Controls.Add(this.numMinutes);
            this.showPopupGroupBox.Controls.Add(this.label3);
            this.showPopupGroupBox.Controls.Add(this.label2);
            this.showPopupGroupBox.Controls.Add(this.label1);
            this.showPopupGroupBox.Controls.Add(this.enableCheckBox);
            this.showPopupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showPopupGroupBox.Location = new System.Drawing.Point(0, 0);
            this.showPopupGroupBox.Name = "showPopupGroupBox";
            this.showPopupGroupBox.Size = new System.Drawing.Size(523, 71);
            this.showPopupGroupBox.TabIndex = 20;
            this.showPopupGroupBox.TabStop = false;
            // 
            // rbtnLess
            // 
            this.rbtnLess.AutoSize = true;
            this.rbtnLess.Location = new System.Drawing.Point(8, 26);
            this.rbtnLess.Name = "rbtnLess";
            this.rbtnLess.Size = new System.Drawing.Size(43, 17);
            this.rbtnLess.TabIndex = 10;
            this.rbtnLess.TabStop = true;
            this.rbtnLess.Text = "less";
            this.rbtnLess.UseVisualStyleBackColor = true;
            // 
            // rbtnMore
            // 
            this.rbtnMore.AutoSize = true;
            this.rbtnMore.Location = new System.Drawing.Point(8, 3);
            this.rbtnMore.Name = "rbtnMore";
            this.rbtnMore.Size = new System.Drawing.Size(48, 17);
            this.rbtnMore.TabIndex = 10;
            this.rbtnMore.TabStop = true;
            this.rbtnMore.Text = "more";
            this.rbtnMore.UseVisualStyleBackColor = true;
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.enableCheckBox.Location = new System.Drawing.Point(6, 0);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(151, 17);
            this.enableCheckBox.TabIndex = 9;
            this.enableCheckBox.Text = "Check last pomodoro time";
            this.enableCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.enableCheckBox.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Notify me if";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "than";
            // 
            // numMinutes
            // 
            this.numMinutes.Location = new System.Drawing.Point(191, 31);
            this.numMinutes.Name = "numMinutes";
            this.numMinutes.Size = new System.Drawing.Size(54, 20);
            this.numMinutes.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "minutes passed since last pomodoro";
            // 
            // rbtnStart
            // 
            this.rbtnStart.AutoSize = true;
            this.rbtnStart.Location = new System.Drawing.Point(3, 3);
            this.rbtnStart.Name = "rbtnStart";
            this.rbtnStart.Size = new System.Drawing.Size(48, 17);
            this.rbtnStart.TabIndex = 10;
            this.rbtnStart.TabStop = true;
            this.rbtnStart.Text = "start.";
            this.rbtnStart.UseVisualStyleBackColor = true;
            // 
            // rbtnFinish
            // 
            this.rbtnFinish.AutoSize = true;
            this.rbtnFinish.Location = new System.Drawing.Point(3, 26);
            this.rbtnFinish.Name = "rbtnFinish";
            this.rbtnFinish.Size = new System.Drawing.Size(52, 17);
            this.rbtnFinish.TabIndex = 10;
            this.rbtnFinish.TabStop = true;
            this.rbtnFinish.Text = "finish.";
            this.rbtnFinish.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtnMore);
            this.panel1.Controls.Add(this.rbtnLess);
            this.panel1.Location = new System.Drawing.Point(72, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(59, 51);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtnStart);
            this.panel2.Controls.Add(this.rbtnFinish);
            this.panel2.Location = new System.Drawing.Point(434, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(59, 51);
            this.panel2.TabIndex = 14;
            // 
            // PomodoroTimeConditionGuiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.showPopupGroupBox);
            this.Name = "PomodoroTimeConditionGuiControl";
            this.Size = new System.Drawing.Size(523, 71);
            this.showPopupGroupBox.ResumeLayout(false);
            this.showPopupGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinutes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox showPopupGroupBox;
        public System.Windows.Forms.CheckBox enableCheckBox;
        private System.Windows.Forms.RadioButton rbtnLess;
        private System.Windows.Forms.RadioButton rbtnMore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMinutes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnFinish;
        private System.Windows.Forms.RadioButton rbtnStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}
