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
    public partial class RatingControl : UserControl
    {
        public RatingControl()
        {
            this.InitializeComponent();
        }

        public TrackBar TrackBarControl { get { return this.trackBar; } }

        public Label RatingLabel { get { return this.evaluationLabel; } }
    }
}
