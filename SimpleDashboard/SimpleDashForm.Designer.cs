namespace CherryTomato.SimpleDashboard
{
    partial class SimpleDashForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleDashForm));
            this.monthChart1 = new CherryTomato.SimpleDashboard.MonthChart();
            this.statusLabel = new System.Windows.Forms.Label();
            this.viewModeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // monthChart1
            // 
            this.monthChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.monthChart1.Location = new System.Drawing.Point(13, 70);
            this.monthChart1.Name = "monthChart1";
            this.monthChart1.Size = new System.Drawing.Size(317, 123);
            this.monthChart1.TabIndex = 3;
            this.monthChart1.ViewMode = CherryTomato.SimpleDashboard.MonthChart.ViewModeEnum.Productivity;
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(13, 13);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(317, 39);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Taking a break..";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // viewModeComboBox
            // 
            this.viewModeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.viewModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewModeComboBox.FormattingEnabled = true;
            this.viewModeComboBox.Items.AddRange(new object[] {
            "Productivity",
            "Pomodoros",
            "P-per-P"});
            this.viewModeComboBox.Location = new System.Drawing.Point(209, 172);
            this.viewModeComboBox.Name = "viewModeComboBox";
            this.viewModeComboBox.Size = new System.Drawing.Size(121, 21);
            this.viewModeComboBox.TabIndex = 4;
            this.viewModeComboBox.Visible = false;
            this.viewModeComboBox.SelectedIndexChanged += new System.EventHandler(this.viewModeComboBox_SelectedIndexChanged);
            // 
            // SimpleDashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 205);
            this.Controls.Add(this.viewModeComboBox);
            this.Controls.Add(this.monthChart1);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(218, 151);
            this.Name = "SimpleDashForm";
            this.Text = "CherryTomato Mini Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimpleDashForm_FormClosing);
            this.Resize += new System.EventHandler(this.SimpleDashForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private MonthChart monthChart1;
        public System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox viewModeComboBox;
    }
}