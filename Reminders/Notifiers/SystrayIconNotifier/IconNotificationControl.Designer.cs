namespace CherryTomato.Reminders.SystrayIconNotifier
{
    partial class IconNotificationControl
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
            this.iconPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.iconComboBox = new System.Windows.Forms.ComboBox();
            this.iconLabel = new System.Windows.Forms.Label();
            this.FlashCountLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.flashCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.flashSystemTrayIconCheckBox = new System.Windows.Forms.CheckBox();
            this.flashSystemTrayIconGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPreviewPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flashCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // flashSystemTrayIconGroupBox
            // 
            this.flashSystemTrayIconGroupBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flashSystemTrayIconGroupBox.Controls.Add(this.iconPreviewPictureBox);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.iconComboBox);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.iconLabel);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.FlashCountLabel);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.label3);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.flashCountNumericUpDown);
            this.flashSystemTrayIconGroupBox.Controls.Add(this.flashSystemTrayIconCheckBox);
            this.flashSystemTrayIconGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flashSystemTrayIconGroupBox.Location = new System.Drawing.Point(0, 0);
            this.flashSystemTrayIconGroupBox.Name = "flashSystemTrayIconGroupBox";
            this.flashSystemTrayIconGroupBox.Size = new System.Drawing.Size(523, 82);
            this.flashSystemTrayIconGroupBox.TabIndex = 18;
            this.flashSystemTrayIconGroupBox.TabStop = false;
            // 
            // iconPreviewPictureBox
            // 
            this.iconPreviewPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconPreviewPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.iconPreviewPictureBox.Location = new System.Drawing.Point(487, 25);
            this.iconPreviewPictureBox.Name = "iconPreviewPictureBox";
            this.iconPreviewPictureBox.Size = new System.Drawing.Size(30, 21);
            this.iconPreviewPictureBox.TabIndex = 5;
            this.iconPreviewPictureBox.TabStop = false;
            // 
            // iconComboBox
            // 
            this.iconComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.iconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iconComboBox.FormattingEnabled = true;
            this.iconComboBox.Location = new System.Drawing.Point(97, 25);
            this.iconComboBox.Name = "iconComboBox";
            this.iconComboBox.Size = new System.Drawing.Size(384, 21);
            this.iconComboBox.TabIndex = 2;
            this.iconComboBox.SelectedIndexChanged += new System.EventHandler(this.iconComboBox_SelectedIndexChanged);
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Location = new System.Drawing.Point(6, 28);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(31, 13);
            this.iconLabel.TabIndex = 1;
            this.iconLabel.Text = "&Icon:";
            // 
            // FlashCountLabel
            // 
            this.FlashCountLabel.AutoSize = true;
            this.FlashCountLabel.Location = new System.Drawing.Point(6, 54);
            this.FlashCountLabel.Name = "FlashCountLabel";
            this.FlashCountLabel.Size = new System.Drawing.Size(35, 13);
            this.FlashCountLabel.TabIndex = 3;
            this.FlashCountLabel.Text = "Fl&ash:";
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
            this.flashCountNumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
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
            // flashSystemTrayIconCheckBox
            // 
            this.flashSystemTrayIconCheckBox.AutoSize = true;
            this.flashSystemTrayIconCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flashSystemTrayIconCheckBox.Location = new System.Drawing.Point(6, 0);
            this.flashSystemTrayIconCheckBox.Name = "flashSystemTrayIconCheckBox";
            this.flashSystemTrayIconCheckBox.Size = new System.Drawing.Size(136, 17);
            this.flashSystemTrayIconCheckBox.TabIndex = 0;
            this.flashSystemTrayIconCheckBox.Text = "&Flash System Tray Icon";
            this.flashSystemTrayIconCheckBox.UseVisualStyleBackColor = false;
            // 
            // IconNotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flashSystemTrayIconGroupBox);
            this.Name = "IconNotificationControl";
            this.Size = new System.Drawing.Size(523, 82);
            this.flashSystemTrayIconGroupBox.ResumeLayout(false);
            this.flashSystemTrayIconGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPreviewPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flashCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox flashSystemTrayIconGroupBox;
        private System.Windows.Forms.PictureBox iconPreviewPictureBox;
        public System.Windows.Forms.ComboBox iconComboBox;
        public System.Windows.Forms.Label iconLabel;
        public System.Windows.Forms.Label FlashCountLabel;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown flashCountNumericUpDown;
        public System.Windows.Forms.CheckBox flashSystemTrayIconCheckBox;
    }
}
