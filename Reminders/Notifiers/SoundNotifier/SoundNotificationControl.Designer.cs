namespace CherryTomato.Reminders.SoundNotifier
{
    partial class SoundNotificationControl
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
            this.playSoundGroupBox = new System.Windows.Forms.GroupBox();
            this.SoundFileLabel = new System.Windows.Forms.Label();
            this.soundFileTextBox = new System.Windows.Forms.TextBox();
            this.soundFileBrowseButton = new System.Windows.Forms.Button();
            this.playSoundCheckBox = new System.Windows.Forms.CheckBox();
            this.playSoundGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // playSoundGroupBox
            // 
            this.playSoundGroupBox.Controls.Add(this.SoundFileLabel);
            this.playSoundGroupBox.Controls.Add(this.soundFileTextBox);
            this.playSoundGroupBox.Controls.Add(this.soundFileBrowseButton);
            this.playSoundGroupBox.Controls.Add(this.playSoundCheckBox);
            this.playSoundGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playSoundGroupBox.Location = new System.Drawing.Point(0, 0);
            this.playSoundGroupBox.Name = "playSoundGroupBox";
            this.playSoundGroupBox.Size = new System.Drawing.Size(523, 55);
            this.playSoundGroupBox.TabIndex = 19;
            this.playSoundGroupBox.TabStop = false;
            // 
            // SoundFileLabel
            // 
            this.SoundFileLabel.AutoSize = true;
            this.SoundFileLabel.Location = new System.Drawing.Point(6, 28);
            this.SoundFileLabel.Name = "SoundFileLabel";
            this.SoundFileLabel.Size = new System.Drawing.Size(60, 13);
            this.SoundFileLabel.TabIndex = 6;
            this.SoundFileLabel.Text = "&Sound File:";
            // 
            // soundFileTextBox
            // 
            this.soundFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.soundFileTextBox.Location = new System.Drawing.Point(97, 25);
            this.soundFileTextBox.Name = "soundFileTextBox";
            this.soundFileTextBox.Size = new System.Drawing.Size(384, 20);
            this.soundFileTextBox.TabIndex = 7;
            // 
            // soundFileBrowseButton
            // 
            this.soundFileBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.soundFileBrowseButton.Location = new System.Drawing.Point(487, 25);
            this.soundFileBrowseButton.Name = "soundFileBrowseButton";
            this.soundFileBrowseButton.Size = new System.Drawing.Size(30, 20);
            this.soundFileBrowseButton.TabIndex = 8;
            this.soundFileBrowseButton.Text = "...";
            this.soundFileBrowseButton.UseVisualStyleBackColor = true;
            this.soundFileBrowseButton.Click += new System.EventHandler(this.soundFileBrowseButton_Click);
            // 
            // playSoundCheckBox
            // 
            this.playSoundCheckBox.AutoSize = true;
            this.playSoundCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.playSoundCheckBox.Location = new System.Drawing.Point(6, 0);
            this.playSoundCheckBox.Name = "playSoundCheckBox";
            this.playSoundCheckBox.Size = new System.Drawing.Size(80, 17);
            this.playSoundCheckBox.TabIndex = 5;
            this.playSoundCheckBox.Text = "&Play Sound";
            this.playSoundCheckBox.UseVisualStyleBackColor = false;
            // 
            // SoundNotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.playSoundGroupBox);
            this.Name = "SoundNotificationControl";
            this.Size = new System.Drawing.Size(523, 55);
            this.playSoundGroupBox.ResumeLayout(false);
            this.playSoundGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox playSoundGroupBox;
        private System.Windows.Forms.Label SoundFileLabel;
        private System.Windows.Forms.TextBox soundFileTextBox;
        private System.Windows.Forms.Button soundFileBrowseButton;
        private System.Windows.Forms.CheckBox playSoundCheckBox;
    }
}
