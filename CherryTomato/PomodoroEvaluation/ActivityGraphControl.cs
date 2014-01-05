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
    public partial class ActivityGraphControl : UserControl
    {
        public ActivityGraphControl()
        {
            this.InitializeComponent();
        }

        public Panel Panel { get { return this.panel1; } }

        public Label KeyboardLabel { get { return this.keyboardLabel; } }

        public Label MouseLabel { get { return this.mouseLabel; } }
    }
}
