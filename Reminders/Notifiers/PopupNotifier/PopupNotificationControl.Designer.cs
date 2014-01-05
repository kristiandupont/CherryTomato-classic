namespace CherryTomato.Reminders.PopupNotifier
{
    partial class PopupNotificationControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.popupTextLabel = new System.Windows.Forms.Label();
            this.txtCaption = new System.Windows.Forms.TextBox();
            this.cmbPopupType = new System.Windows.Forms.ComboBox();
            this.popupMessageTextBox = new System.Windows.Forms.TextBox();
            this.showPopupCheckBox = new System.Windows.Forms.CheckBox();
            this.showPopupGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // showPopupGroupBox
            // 
            this.showPopupGroupBox.Controls.Add(this.label1);
            this.showPopupGroupBox.Controls.Add(this.label3);
            this.showPopupGroupBox.Controls.Add(this.popupTextLabel);
            this.showPopupGroupBox.Controls.Add(this.txtCaption);
            this.showPopupGroupBox.Controls.Add(this.cmbPopupType);
            this.showPopupGroupBox.Controls.Add(this.popupMessageTextBox);
            this.showPopupGroupBox.Controls.Add(this.showPopupCheckBox);
            this.showPopupGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showPopupGroupBox.Location = new System.Drawing.Point(0, 0);
            this.showPopupGroupBox.Name = "showPopupGroupBox";
            this.showPopupGroupBox.Size = new System.Drawing.Size(523, 131);
            this.showPopupGroupBox.TabIndex = 20;
            this.showPopupGroupBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Popup T&ype:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Subject:";
            // 
            // popupTextLabel
            // 
            this.popupTextLabel.AutoSize = true;
            this.popupTextLabel.Location = new System.Drawing.Point(7, 83);
            this.popupTextLabel.Name = "popupTextLabel";
            this.popupTextLabel.Size = new System.Drawing.Size(65, 13);
            this.popupTextLabel.TabIndex = 10;
            this.popupTextLabel.Text = "Pop&up Text:";
            // 
            // txtCaption
            // 
            this.txtCaption.AcceptsReturn = true;
            this.txtCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaption.Location = new System.Drawing.Point(98, 54);
            this.txtCaption.Multiline = true;
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(419, 23);
            this.txtCaption.TabIndex = 11;
            // 
            // cmbPopupType
            // 
            this.cmbPopupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPopupType.FormattingEnabled = true;
            this.cmbPopupType.Location = new System.Drawing.Point(98, 25);
            this.cmbPopupType.Name = "cmbPopupType";
            this.cmbPopupType.Size = new System.Drawing.Size(184, 21);
            this.cmbPopupType.TabIndex = 2;
            // 
            // popupMessageTextBox
            // 
            this.popupMessageTextBox.AcceptsReturn = true;
            this.popupMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.popupMessageTextBox.Location = new System.Drawing.Point(98, 83);
            this.popupMessageTextBox.Multiline = true;
            this.popupMessageTextBox.Name = "popupMessageTextBox";
            this.popupMessageTextBox.Size = new System.Drawing.Size(419, 41);
            this.popupMessageTextBox.TabIndex = 11;
            // 
            // showPopupCheckBox
            // 
            this.showPopupCheckBox.AutoSize = true;
            this.showPopupCheckBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.showPopupCheckBox.Location = new System.Drawing.Point(6, 0);
            this.showPopupCheckBox.Name = "showPopupCheckBox";
            this.showPopupCheckBox.Size = new System.Drawing.Size(87, 17);
            this.showPopupCheckBox.TabIndex = 9;
            this.showPopupCheckBox.Text = "S&how Popup";
            this.showPopupCheckBox.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.showPopupCheckBox.UseVisualStyleBackColor = false;
            // 
            // PopupNotificationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.showPopupGroupBox);
            this.Name = "PopupNotificationControl";
            this.Size = new System.Drawing.Size(523, 131);
            this.showPopupGroupBox.ResumeLayout(false);
            this.showPopupGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox showPopupGroupBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label popupTextLabel;
        public System.Windows.Forms.ComboBox cmbPopupType;
        public System.Windows.Forms.TextBox popupMessageTextBox;
        public System.Windows.Forms.CheckBox showPopupCheckBox;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtCaption;
    }
}
