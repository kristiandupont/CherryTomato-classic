namespace CherryTomato.Reminders.ProductivityConditionChecker
{
    partial class ProductivityConditionGuiControl
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
            this.enableCheckBox = new System.Windows.Forms.CheckBox();
            this.numHours = new System.Windows.Forms.NumericUpDown();
            this.numPomodoros = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.showPopupGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPomodoros)).BeginInit();
            this.SuspendLayout();
            // 
            // showPopupGroupBox
            // 
            this.showPopupGroupBox.Controls.Add(this.enableCheckBox);
            this.showPopupGroupBox.Controls.Add(this.numHours);
            this.showPopupGroupBox.Controls.Add(this.numPomodoros);
            this.showPopupGroupBox.Controls.Add(this.label2);
            this.showPopupGroupBox.Controls.Add(this.label3);
            this.showPopupGroupBox.Controls.Add(this.label1);
            this.showPopupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showPopupGroupBox.Location = new System.Drawing.Point(0, 0);
            this.showPopupGroupBox.Name = "showPopupGroupBox";
            this.showPopupGroupBox.Size = new System.Drawing.Size(523, 71);
            this.showPopupGroupBox.TabIndex = 20;
            this.showPopupGroupBox.TabStop = false;
            // 
            // enableCheckBox
            // 
            this.enableCheckBox.AutoSize = true;
            this.enableCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.enableCheckBox.Location = new System.Drawing.Point(6, 0);
            this.enableCheckBox.Name = "enableCheckBox";
            this.enableCheckBox.Size = new System.Drawing.Size(117, 17);
            this.enableCheckBox.TabIndex = 18;
            this.enableCheckBox.Text = "Check productivity";
            this.enableCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.enableCheckBox.UseVisualStyleBackColor = false;
            // 
            // numHours
            // 
            this.numHours.Location = new System.Drawing.Point(379, 29);
            this.numHours.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numHours.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHours.Name = "numHours";
            this.numHours.Size = new System.Drawing.Size(54, 20);
            this.numHours.TabIndex = 16;
            this.numHours.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numPomodoros
            // 
            this.numPomodoros.Location = new System.Drawing.Point(152, 29);
            this.numPomodoros.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPomodoros.Name = "numPomodoros";
            this.numPomodoros.Size = new System.Drawing.Size(54, 20);
            this.numPomodoros.TabIndex = 17;
            this.numPomodoros.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(439, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "hours.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "pomodoros were done within last";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Notify me only if less than";
            // 
            // ProductivityConditionGuiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.showPopupGroupBox);
            this.Name = "ProductivityConditionGuiControl";
            this.Size = new System.Drawing.Size(523, 71);
            this.showPopupGroupBox.ResumeLayout(false);
            this.showPopupGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPomodoros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox showPopupGroupBox;
        private System.Windows.Forms.NumericUpDown numHours;
        private System.Windows.Forms.NumericUpDown numPomodoros;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox enableCheckBox;
    }
}
