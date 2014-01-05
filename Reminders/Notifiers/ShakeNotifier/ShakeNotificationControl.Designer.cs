namespace CherryTomato.Reminders.ShakeNotifier
{
    partial class ShakeNotificationControl
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
            this.shakeCurrentlyActiveWindowGroupBox = new System.Windows.Forms.GroupBox();
            this.shakeCurrentlyActiveWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.shakeItLabel = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.timesLabel2 = new System.Windows.Forms.Label();
            this.shakeCurrentlyActiveWindowGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // shakeCurrentlyActiveWindowGroupBox
            // 
            this.shakeCurrentlyActiveWindowGroupBox.Controls.Add(this.shakeCurrentlyActiveWindowCheckBox);
            this.shakeCurrentlyActiveWindowGroupBox.Controls.Add(this.shakeItLabel);
            this.shakeCurrentlyActiveWindowGroupBox.Controls.Add(this.numTimeout);
            this.shakeCurrentlyActiveWindowGroupBox.Controls.Add(this.timesLabel2);
            this.shakeCurrentlyActiveWindowGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shakeCurrentlyActiveWindowGroupBox.Location = new System.Drawing.Point(0, 0);
            this.shakeCurrentlyActiveWindowGroupBox.Name = "shakeCurrentlyActiveWindowGroupBox";
            this.shakeCurrentlyActiveWindowGroupBox.Size = new System.Drawing.Size(523, 53);
            this.shakeCurrentlyActiveWindowGroupBox.TabIndex = 21;
            this.shakeCurrentlyActiveWindowGroupBox.TabStop = false;
            // 
            // shakeCurrentlyActiveWindowCheckBox
            // 
            this.shakeCurrentlyActiveWindowCheckBox.AutoSize = true;
            this.shakeCurrentlyActiveWindowCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.shakeCurrentlyActiveWindowCheckBox.Location = new System.Drawing.Point(6, 0);
            this.shakeCurrentlyActiveWindowCheckBox.Name = "shakeCurrentlyActiveWindowCheckBox";
            this.shakeCurrentlyActiveWindowCheckBox.Size = new System.Drawing.Size(176, 17);
            this.shakeCurrentlyActiveWindowCheckBox.TabIndex = 9;
            this.shakeCurrentlyActiveWindowCheckBox.Text = "Shake Currently Avtive &Window";
            this.shakeCurrentlyActiveWindowCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.shakeCurrentlyActiveWindowCheckBox.UseVisualStyleBackColor = false;
            // 
            // shakeItLabel
            // 
            this.shakeItLabel.AutoSize = true;
            this.shakeItLabel.Location = new System.Drawing.Point(6, 28);
            this.shakeItLabel.Name = "shakeItLabel";
            this.shakeItLabel.Size = new System.Drawing.Size(49, 13);
            this.shakeItLabel.TabIndex = 3;
            this.shakeItLabel.Text = "Shake &it:";
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(97, 24);
            this.numTimeout.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(50, 20);
            this.numTimeout.TabIndex = 4;
            this.numTimeout.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // timesLabel2
            // 
            this.timesLabel2.AutoSize = true;
            this.timesLabel2.Location = new System.Drawing.Point(153, 28);
            this.timesLabel2.Name = "timesLabel2";
            this.timesLabel2.Size = new System.Drawing.Size(46, 17);
            this.timesLabel2.TabIndex = 3;
            this.timesLabel2.Text = "seconds";
            this.timesLabel2.UseCompatibleTextRendering = true;
            // 
            // ShakeNotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.shakeCurrentlyActiveWindowGroupBox);
            this.Name = "ShakeNotificationControl";
            this.Size = new System.Drawing.Size(523, 53);
            this.shakeCurrentlyActiveWindowGroupBox.ResumeLayout(false);
            this.shakeCurrentlyActiveWindowGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox shakeCurrentlyActiveWindowGroupBox;
        public System.Windows.Forms.CheckBox shakeCurrentlyActiveWindowCheckBox;
        public System.Windows.Forms.Label shakeItLabel;
        public System.Windows.Forms.NumericUpDown numTimeout;
        public System.Windows.Forms.Label timesLabel2;
    }
}
