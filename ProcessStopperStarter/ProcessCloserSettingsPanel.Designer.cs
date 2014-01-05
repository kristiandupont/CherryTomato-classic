namespace CherryTomato.ProcessStopper
{
    partial class ProcessCloserSettingsPanel
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
            this.gridProcesses = new System.Windows.Forms.DataGridView();
            this.checks = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.processName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.simpleDashGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesses)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleDashGroupBox
            // 
            this.simpleDashGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleDashGroupBox.Controls.Add(this.gridProcesses);
            this.simpleDashGroupBox.Location = new System.Drawing.Point(4, 4);
            this.simpleDashGroupBox.Name = "simpleDashGroupBox";
            this.simpleDashGroupBox.Size = new System.Drawing.Size(347, 207);
            this.simpleDashGroupBox.TabIndex = 0;
            this.simpleDashGroupBox.TabStop = false;
            this.simpleDashGroupBox.Text = "Safely close these applications just after pomodoro start";
            // 
            // gridProcesses
            // 
            this.gridProcesses.AllowUserToAddRows = false;
            this.gridProcesses.AllowUserToDeleteRows = false;
            this.gridProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProcesses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checks,
            this.processName});
            this.gridProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProcesses.Location = new System.Drawing.Point(3, 16);
            this.gridProcesses.MultiSelect = false;
            this.gridProcesses.Name = "gridProcesses";
            this.gridProcesses.RowHeadersVisible = false;
            this.gridProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridProcesses.Size = new System.Drawing.Size(341, 188);
            this.gridProcesses.TabIndex = 0;
            this.gridProcesses.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridProcesses_CellMouseUp);
            // 
            // checks
            // 
            this.checks.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.checks.FillWeight = 16F;
            this.checks.Frozen = true;
            this.checks.HeaderText = "";
            this.checks.Name = "checks";
            this.checks.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.checks.Width = 5;
            // 
            // processName
            // 
            this.processName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.processName.HeaderText = "Process Name";
            this.processName.Name = "processName";
            this.processName.ReadOnly = true;
            this.processName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.processName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProcessKillerSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.simpleDashGroupBox);
            this.Name = "ProcessKillerSettingsPanel";
            this.Size = new System.Drawing.Size(354, 214);
            this.simpleDashGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProcesses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox simpleDashGroupBox;
        private System.Windows.Forms.DataGridView gridProcesses;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checks;
        private System.Windows.Forms.DataGridViewTextBoxColumn processName;

    }
}