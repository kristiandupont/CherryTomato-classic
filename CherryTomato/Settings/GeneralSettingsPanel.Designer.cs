namespace CherryTomato.Settings
{
    partial class GeneralSettingsPanel
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.checkForUpdatesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.pomodoroGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.ringSoundcheckBox = new System.Windows.Forms.CheckBox();
            this.tickingSoundcheckBox = new System.Windows.Forms.CheckBox();
            this.rewindCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pomodoroGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.White;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(164)))), ((int)(((byte)(118)))));
            this.versionLabel.Location = new System.Drawing.Point(242, 81);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(69, 13);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Version x.y";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkForUpdatesLinkLabel
            // 
            this.checkForUpdatesLinkLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkForUpdatesLinkLabel.AutoSize = true;
            this.checkForUpdatesLinkLabel.BackColor = System.Drawing.Color.White;
            this.checkForUpdatesLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkForUpdatesLinkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(209)))), ((int)(((byte)(148)))));
            this.checkForUpdatesLinkLabel.Location = new System.Drawing.Point(132, 94);
            this.checkForUpdatesLinkLabel.Name = "checkForUpdatesLinkLabel";
            this.checkForUpdatesLinkLabel.Size = new System.Drawing.Size(94, 13);
            this.checkForUpdatesLinkLabel.TabIndex = 4;
            this.checkForUpdatesLinkLabel.TabStop = true;
            this.checkForUpdatesLinkLabel.Text = "Check for updates";
            this.checkForUpdatesLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pomodoroGroupBox
            // 
            this.pomodoroGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pomodoroGroupBox.Controls.Add(this.label1);
            this.pomodoroGroupBox.Controls.Add(this.trackBar1);
            this.pomodoroGroupBox.Controls.Add(this.ringSoundcheckBox);
            this.pomodoroGroupBox.Controls.Add(this.tickingSoundcheckBox);
            this.pomodoroGroupBox.Controls.Add(this.rewindCheckBox);
            this.pomodoroGroupBox.Location = new System.Drawing.Point(2, 132);
            this.pomodoroGroupBox.Name = "pomodoroGroupBox";
            this.pomodoroGroupBox.Size = new System.Drawing.Size(351, 83);
            this.pomodoroGroupBox.TabIndex = 5;
            this.pomodoroGroupBox.TabStop = false;
            this.pomodoroGroupBox.Text = "&Pomodoro";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(108, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "&Volume";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(108, 20);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(237, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 10;
            this.trackBar1.Visible = false;
            // 
            // ringSoundcheckBox
            // 
            this.ringSoundcheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ringSoundcheckBox.AutoSize = true;
            this.ringSoundcheckBox.Checked = true;
            this.ringSoundcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ringSoundcheckBox.Location = new System.Drawing.Point(6, 60);
            this.ringSoundcheckBox.Name = "ringSoundcheckBox";
            this.ringSoundcheckBox.Size = new System.Drawing.Size(82, 17);
            this.ringSoundcheckBox.TabIndex = 2;
            this.ringSoundcheckBox.Text = "&Ring Sound";
            this.ringSoundcheckBox.UseVisualStyleBackColor = true;
            this.ringSoundcheckBox.CheckedChanged += new System.EventHandler(this.ringSoundcheckBox_CheckedChanged);
            // 
            // tickingSoundcheckBox
            // 
            this.tickingSoundcheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tickingSoundcheckBox.AutoSize = true;
            this.tickingSoundcheckBox.Checked = true;
            this.tickingSoundcheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tickingSoundcheckBox.Location = new System.Drawing.Point(6, 40);
            this.tickingSoundcheckBox.Name = "tickingSoundcheckBox";
            this.tickingSoundcheckBox.Size = new System.Drawing.Size(95, 17);
            this.tickingSoundcheckBox.TabIndex = 1;
            this.tickingSoundcheckBox.Text = "&Ticking Sound";
            this.tickingSoundcheckBox.UseVisualStyleBackColor = true;
            this.tickingSoundcheckBox.CheckedChanged += new System.EventHandler(this.tickingSoundcheckBox_CheckedChanged);
            // 
            // rewindCheckBox
            // 
            this.rewindCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rewindCheckBox.AutoSize = true;
            this.rewindCheckBox.Checked = true;
            this.rewindCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rewindCheckBox.Location = new System.Drawing.Point(6, 20);
            this.rewindCheckBox.Name = "rewindCheckBox";
            this.rewindCheckBox.Size = new System.Drawing.Size(96, 17);
            this.rewindCheckBox.TabIndex = 0;
            this.rewindCheckBox.Text = "R&ewind Sound";
            this.rewindCheckBox.UseVisualStyleBackColor = true;
            this.rewindCheckBox.CheckedChanged += new System.EventHandler(this.rewindCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 121);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::CherryTomato.Properties.Resources.smalllogo;
            this.pictureBox1.Location = new System.Drawing.Point(36, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(286, 64);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // GeneralSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkForUpdatesLinkLabel);
            this.Controls.Add(this.pomodoroGroupBox);
            this.Controls.Add(this.panel1);
            this.Name = "GeneralSettingsPanel";
            this.Size = new System.Drawing.Size(356, 216);
            this.pomodoroGroupBox.ResumeLayout(false);
            this.pomodoroGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel checkForUpdatesLinkLabel;
        private System.Windows.Forms.GroupBox pomodoroGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox ringSoundcheckBox;
        private System.Windows.Forms.CheckBox tickingSoundcheckBox;
        private System.Windows.Forms.CheckBox rewindCheckBox;
        private System.Windows.Forms.Panel panel1;
    }
}
