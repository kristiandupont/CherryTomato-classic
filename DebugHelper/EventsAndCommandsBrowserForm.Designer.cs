namespace CherryTomato.DebugHelper
{
    partial class EventsAndCommandsBrowserForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.tabCommands = new System.Windows.Forms.TabPage();
            this.dataCommands = new System.Windows.Forms.DataGridView();
            this.dataEvents = new System.Windows.Forms.DataGridView();
            this.commandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commandAssembly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commandDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventListeners = new System.Windows.Forms.DataGridViewButtonColumn();
            this.eventAssembly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabEvents.SuspendLayout();
            this.tabCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataCommands)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabEvents);
            this.tabControl.Controls.Add(this.tabCommands);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(902, 418);
            this.tabControl.TabIndex = 0;
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.dataEvents);
            this.tabEvents.Location = new System.Drawing.Point(4, 22);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabEvents.Size = new System.Drawing.Size(894, 392);
            this.tabEvents.TabIndex = 0;
            this.tabEvents.Text = "Events";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // tabCommands
            // 
            this.tabCommands.Controls.Add(this.dataCommands);
            this.tabCommands.Location = new System.Drawing.Point(4, 22);
            this.tabCommands.Name = "tabCommands";
            this.tabCommands.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommands.Size = new System.Drawing.Size(894, 392);
            this.tabCommands.TabIndex = 1;
            this.tabCommands.Text = "Commands";
            this.tabCommands.UseVisualStyleBackColor = true;
            // 
            // dataCommands
            // 
            this.dataCommands.AllowUserToAddRows = false;
            this.dataCommands.AllowUserToDeleteRows = false;
            this.dataCommands.AllowUserToOrderColumns = true;
            this.dataCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.commandName,
            this.commandAssembly,
            this.commandDescription});
            this.dataCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataCommands.Location = new System.Drawing.Point(3, 3);
            this.dataCommands.Name = "dataCommands";
            this.dataCommands.ReadOnly = true;
            this.dataCommands.RowHeadersVisible = false;
            this.dataCommands.Size = new System.Drawing.Size(888, 386);
            this.dataCommands.TabIndex = 0;
            // 
            // dataEvents
            // 
            this.dataEvents.AllowUserToAddRows = false;
            this.dataEvents.AllowUserToDeleteRows = false;
            this.dataEvents.AllowUserToOrderColumns = true;
            this.dataEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataEvents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eventName,
            this.eventListeners,
            this.eventAssembly,
            this.eventDescription});
            this.dataEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataEvents.Location = new System.Drawing.Point(3, 3);
            this.dataEvents.Name = "dataEvents";
            this.dataEvents.ReadOnly = true;
            this.dataEvents.RowHeadersVisible = false;
            this.dataEvents.Size = new System.Drawing.Size(888, 386);
            this.dataEvents.TabIndex = 1;
            this.dataEvents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataEvents_CellContentClick);
            // 
            // commandName
            // 
            this.commandName.HeaderText = "Name";
            this.commandName.Name = "commandName";
            this.commandName.ReadOnly = true;
            this.commandName.Width = 200;
            // 
            // commandAssembly
            // 
            this.commandAssembly.HeaderText = "Assembly";
            this.commandAssembly.Name = "commandAssembly";
            this.commandAssembly.ReadOnly = true;
            // 
            // commandDescription
            // 
            this.commandDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.commandDescription.HeaderText = "Description";
            this.commandDescription.Name = "commandDescription";
            this.commandDescription.ReadOnly = true;
            // 
            // eventName
            // 
            this.eventName.HeaderText = "Name";
            this.eventName.Name = "eventName";
            this.eventName.ReadOnly = true;
            this.eventName.Width = 200;
            // 
            // eventListeners
            // 
            this.eventListeners.HeaderText = "Listeners";
            this.eventListeners.Name = "eventListeners";
            this.eventListeners.ReadOnly = true;
            this.eventListeners.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.eventListeners.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.eventListeners.Width = 50;
            // 
            // eventAssembly
            // 
            this.eventAssembly.HeaderText = "Assembly";
            this.eventAssembly.Name = "eventAssembly";
            this.eventAssembly.ReadOnly = true;
            // 
            // eventDescription
            // 
            this.eventDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.eventDescription.HeaderText = "Description";
            this.eventDescription.Name = "eventDescription";
            this.eventDescription.ReadOnly = true;
            // 
            // EventsAndCommandsBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 418);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "EventsAndCommandsBrowserForm";
            this.Text = "All Registered Events and Commands";
            this.tabControl.ResumeLayout(false);
            this.tabEvents.ResumeLayout(false);
            this.tabCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataCommands)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataEvents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEvents;
        private System.Windows.Forms.TabPage tabCommands;
        private System.Windows.Forms.DataGridView dataCommands;
        private System.Windows.Forms.DataGridView dataEvents;
        private System.Windows.Forms.DataGridViewTextBoxColumn commandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn commandAssembly;
        private System.Windows.Forms.DataGridViewTextBoxColumn commandDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventName;
        private System.Windows.Forms.DataGridViewButtonColumn eventListeners;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventAssembly;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventDescription;
    }
}