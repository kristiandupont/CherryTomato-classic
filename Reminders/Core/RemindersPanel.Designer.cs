namespace CherryTomato.Reminders.Core
{
    partial class RemindersPanel
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
            this.components = new System.ComponentModel.Container();
            this.remindersListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.remindersLabel = new System.Windows.Forms.Label();
            this.newButton = new System.Windows.Forms.Button();
            this.newReminderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.remindersListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remindersListViewContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // remindersListView
            // 
            this.remindersListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.remindersListView.CheckBoxes = true;
            this.remindersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.typeColumnHeader});
            this.remindersListView.Location = new System.Drawing.Point(4, 20);
            this.remindersListView.MultiSelect = false;
            this.remindersListView.Name = "remindersListView";
            this.remindersListView.Size = new System.Drawing.Size(347, 162);
            this.remindersListView.TabIndex = 0;
            this.remindersListView.UseCompatibleStateImageBehavior = false;
            this.remindersListView.View = System.Windows.Forms.View.Details;
            this.remindersListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.remindersListView_ItemChecked);
            this.remindersListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.remindersListView_MouseClick);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 231;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "Type";
            this.typeColumnHeader.Width = 112;
            // 
            // remindersLabel
            // 
            this.remindersLabel.AutoSize = true;
            this.remindersLabel.Location = new System.Drawing.Point(4, 4);
            this.remindersLabel.Name = "remindersLabel";
            this.remindersLabel.Size = new System.Drawing.Size(60, 13);
            this.remindersLabel.TabIndex = 1;
            this.remindersLabel.Text = "&Reminders:";
            // 
            // newButton
            // 
            this.newButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newButton.Location = new System.Drawing.Point(275, 188);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(75, 23);
            this.newButton.TabIndex = 2;
            this.newButton.Text = "&New...";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // newReminderContextMenuStrip
            // 
            this.newReminderContextMenuStrip.Name = "newReminderContextMenuStrip";
            this.newReminderContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // remindersListViewContextMenuStrip
            // 
            this.remindersListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.remindersListViewContextMenuStrip.Name = "remindersListViewContextMenuStrip";
            this.remindersListViewContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // RemindersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.newButton);
            this.Controls.Add(this.remindersLabel);
            this.Controls.Add(this.remindersListView);
            this.Name = "RemindersPanel";
            this.Size = new System.Drawing.Size(354, 214);
            this.remindersListViewContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView remindersListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.Label remindersLabel;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.ContextMenuStrip newReminderContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip remindersListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
    }
}
