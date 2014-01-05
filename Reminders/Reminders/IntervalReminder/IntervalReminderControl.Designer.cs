namespace CherryTomato.Reminders.IntervalReminder
{
    partial class IntervalReminderControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.toIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.toLabel = new System.Windows.Forms.Label();
            this.fromIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.intervalLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.toIntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "minutes.";
            // 
            // toIntervalNumericUpDown
            // 
            this.toIntervalNumericUpDown.Location = new System.Drawing.Point(325, 4);
            this.toIntervalNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.toIntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.toIntervalNumericUpDown.Name = "toIntervalNumericUpDown";
            this.toIntervalNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.toIntervalNumericUpDown.TabIndex = 12;
            this.toIntervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.toIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.toIntervalNumericUpDown_ValueChanged);
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(161, 6);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(129, 13);
            this.toLabel.TabIndex = 14;
            this.toLabel.Text = "minutes and no more than";
            // 
            // fromIntervalNumericUpDown
            // 
            this.fromIntervalNumericUpDown.Location = new System.Drawing.Point(91, 4);
            this.fromIntervalNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fromIntervalNumericUpDown.Name = "fromIntervalNumericUpDown";
            this.fromIntervalNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.fromIntervalNumericUpDown.TabIndex = 13;
            this.fromIntervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fromIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.fromIntervalNumericUpDown_ValueChanged);
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(3, 6);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(66, 13);
            this.intervalLabel.TabIndex = 11;
            this.intervalLabel.Text = "Wai&t at least";
            // 
            // IntervalReminderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toIntervalNumericUpDown);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromIntervalNumericUpDown);
            this.Controls.Add(this.intervalLabel);
            this.Name = "IntervalReminderControl";
            this.Size = new System.Drawing.Size(529, 27);
            ((System.ComponentModel.ISupportInitialize)(this.toIntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown toIntervalNumericUpDown;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.NumericUpDown fromIntervalNumericUpDown;
        private System.Windows.Forms.Label intervalLabel;
    }
}
