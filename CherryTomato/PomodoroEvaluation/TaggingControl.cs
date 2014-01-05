using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CherryTomato.PomodoroEvaluation
{
    public partial class TaggingControl : UserControl
    {
        public TaggingControl()
        {
            this.InitializeComponent();
        }

        public FlowLayoutPanel Panel { get { return this.tagsPanel; } }

        public TextBox TextBoxControl { get { return this.tagTextBox; } }
    }
}
