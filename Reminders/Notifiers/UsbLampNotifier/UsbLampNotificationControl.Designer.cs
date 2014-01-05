namespace CherryTomato.Reminders.UsbLampNotifier
{
    partial class UsbLampNotificationControl
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
            this.flashSystemTrayIconGroupBox = new System.Windows.Forms.GroupBox();
            this.changeColorButton = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.flashCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flashCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.flashCheckBox = new System.Windows.Forms.CheckBox();
            this.flashSystemTrayIconGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flashCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flashSystemTrayIconGroupBox
            // 
            this.flashSystemTrayIconGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flashSystemTrayIconGroupBox.Controls.Add(this.changeColorButton);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.colorLabel);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.flashCountLabel);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.label3);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.flashCountNumericUpDown);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.flashCheckBox);
            this.flashSystemTrayIconGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flashSystemTrayIconGroupBox.Location = new System.Drawing.Point(0, 0);
            this.flashSystemTrayIconGroupBox.Name = "flashSystemTrayIconGroupBox";
            this.flashSystemTrayIconGroupBox.Size = new System.Drawing.Size(523, 83);
            this.flashSystemTrayIconGroupBox.TabIndex = 18;
            this.flashSystemTrayIconGroupBox.TabStop = false;
            // 
            // changeColorButton
            // 
            this.changeColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeColorButton.Location = new System.Drawing.Point(97, 23);
            this.changeColorButton.Name = "changeColorButton";
            this.changeColorButton.Size = new System.Drawing.Size(75, 23);
            this.changeColorButton.TabIndex = 5;
            this.changeColorButton.Text = "Change...";
            this.changeColorButton.UseVisualStyleBackColor = true;
            this.changeColorButton.Click += new System.EventHandler(this.changeColorButton_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(6, 28);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(34, 13);
            this.colorLabel.TabIndex = 1;
            this.colorLabel.Text = "&Color:";
            // 
            // flashCountLabel
            // 
            this.flashCountLabel.AutoSize = true;
            this.flashCountLabel.Location = new System.Drawing.Point(6, 54);
            this.flashCountLabel.Name = "flashCountLabel";
            this.flashCountLabel.Size = new System.Drawing.Size(35, 13);
            this.flashCountLabel.TabIndex = 3;
            this.flashCountLabel.Text = "Fl&ash:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "times";
            // 
            // flashCountNumericUpDown
            // 
            this.flashCountNumericUpDown.Location = new System.Drawing.Point(97, 52);
            this.flashCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.flashCountNumericUpDown.Name = "flashCountNumericUpDown";
            this.flashCountNumericUpDown.Size = new System.Drawing.Size(50, 20);
            this.flashCountNumericUpDown.TabIndex = 4;
            this.flashCountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // flashCheckBox
            // 
            this.flashCheckBox.AutoSize = true;
            this.flashCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flashCheckBox.Location = new System.Drawing.Point(6, 0);
            this.flashCheckBox.Name = "flashCheckBox";
            this.flashCheckBox.Size = new System.Drawing.Size(105, 17);
            this.flashCheckBox.TabIndex = 0;
            this.flashCheckBox.Text = "&Flash USB Lamp";
            this.flashCheckBox.UseVisualStyleBackColor = false;
            // 
            // UsbLampNotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flashSystemTrayIconGroupBox);
            this.Name = "UsbLampNotificationControl";
            this.Size = new System.Drawing.Size(523, 83);
            this.flashSystemTrayIconGroupBox.ResumeLayout(false);
            this.flashSystemTrayIconGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flashCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox flashSystemTrayIconGroupBox;
        public System.Windows.Forms.Label colorLabel;
        public System.Windows.Forms.Label flashCountLabel;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown flashCountNumericUpDown;
        public System.Windows.Forms.CheckBox flashCheckBox;
        private System.Windows.Forms.Button changeColorButton;
    }
}
