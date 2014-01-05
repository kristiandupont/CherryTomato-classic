namespace CherryTomato.SimpleDashboard
{
    partial class SimpleDashSettingsPanel
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
            this.simpleDashGroupBox = new System.Windows.Forms.GroupBox();
            this.showInTaskBarCheckBox = new System.Windows.Forms.CheckBox();
            this.alwaysOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.simpleDashGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleDashGroupBox
            // 
            this.simpleDashGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleDashGroupBox.Controls.Add(this.showInTaskBarCheckBox);
            this.simpleDashGroupBox.Controls.Add(this.alwaysOnTopCheckBox);
            this.simpleDashGroupBox.Controls.Add(this.enabledCheckBox);
            this.simpleDashGroupBox.Location = new System.Drawing.Point(4, 4);
            this.simpleDashGroupBox.Name = "simpleDashGroupBox";
            this.simpleDashGroupBox.Size = new System.Drawing.Size(347, 207);
            this.simpleDashGroupBox.TabIndex = 0;
            this.simpleDashGroupBox.TabStop = false;
            this.simpleDashGroupBox.Text = "Simple Dashboard";
            // 
            // showInTaskBarCheckBox
            // 
            this.showInTaskBarCheckBox.AutoSize = true;
            this.showInTaskBarCheckBox.Location = new System.Drawing.Point(6, 65);
            this.showInTaskBarCheckBox.Name = "showInTaskBarCheckBox";
            this.showInTaskBarCheckBox.Size = new System.Drawing.Size(102, 17);
            this.showInTaskBarCheckBox.TabIndex = 0;
            this.showInTaskBarCheckBox.Text = "&Show in taskbar";
            this.showInTaskBarCheckBox.UseVisualStyleBackColor = true;
            this.showInTaskBarCheckBox.CheckedChanged += new System.EventHandler(this.showInTaskBarCheckBox_CheckedChanged);
            // 
            // alwaysOnTopCheckBox
            // 
            this.alwaysOnTopCheckBox.AutoSize = true;
            this.alwaysOnTopCheckBox.Location = new System.Drawing.Point(6, 42);
            this.alwaysOnTopCheckBox.Name = "alwaysOnTopCheckBox";
            this.alwaysOnTopCheckBox.Size = new System.Drawing.Size(92, 17);
            this.alwaysOnTopCheckBox.TabIndex = 0;
            this.alwaysOnTopCheckBox.Text = "A&lways on top";
            this.alwaysOnTopCheckBox.UseVisualStyleBackColor = true;
            this.alwaysOnTopCheckBox.CheckedChanged += new System.EventHandler(this.alwaysOnTopCheckBox_CheckedChanged);
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.AutoSize = true;
            this.enabledCheckBox.Location = new System.Drawing.Point(6, 19);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(56, 17);
            this.enabledCheckBox.TabIndex = 0;
            this.enabledCheckBox.Text = "&Active";
            this.enabledCheckBox.UseVisualStyleBackColor = true;
            this.enabledCheckBox.CheckedChanged += new System.EventHandler(this.enabledCheckBox_CheckedChanged);
            // 
            // SimpleDashSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.simpleDashGroupBox);
            this.Name = "SimpleDashSettingsPanel";
            this.Size = new System.Drawing.Size(354, 214);
            this.simpleDashGroupBox.ResumeLayout(false);
            this.simpleDashGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox simpleDashGroupBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.CheckBox showInTaskBarCheckBox;
        private System.Windows.Forms.CheckBox alwaysOnTopCheckBox;

    }
}