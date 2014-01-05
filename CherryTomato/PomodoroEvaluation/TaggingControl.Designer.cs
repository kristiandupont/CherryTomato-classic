namespace CherryTomato.PomodoroEvaluation
{
    partial class TaggingControl
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
            this.tagsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tagTextBox = new System.Windows.Forms.TextBox();
            this.tagsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tagsPanel
            // 
            this.tagsPanel.Location = new System.Drawing.Point(0, 44);
            this.tagsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tagsPanel.Name = "tagsPanel";
            this.tagsPanel.Size = new System.Drawing.Size(156, 160);
            this.tagsPanel.TabIndex = 30;
            // 
            // tagTextBox
            // 
            this.tagTextBox.AcceptsReturn = true;
            this.tagTextBox.Location = new System.Drawing.Point(0, 17);
            this.tagTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.tagTextBox.Name = "tagTextBox";
            this.tagTextBox.Size = new System.Drawing.Size(156, 20);
            this.tagTextBox.TabIndex = 29;
            // 
            // tagsLabel
            // 
            this.tagsLabel.AutoSize = true;
            this.tagsLabel.Location = new System.Drawing.Point(-3, 0);
            this.tagsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.tagsLabel.Name = "tagsLabel";
            this.tagsLabel.Size = new System.Drawing.Size(34, 13);
            this.tagsLabel.TabIndex = 28;
            this.tagsLabel.Text = "&Tags:";
            // 
            // TaggingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tagsPanel);
            this.Controls.Add(this.tagTextBox);
            this.Controls.Add(this.tagsLabel);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.Name = "TaggingControl";
            this.Size = new System.Drawing.Size(156, 204);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel tagsPanel;
        private System.Windows.Forms.TextBox tagTextBox;
        private System.Windows.Forms.Label tagsLabel;
    }
}
