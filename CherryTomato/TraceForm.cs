using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using CherryTomato.Core;

namespace CherryTomato
{
    public partial class TraceForm : Form
    {
        public TraceForm()
        {
            InitializeComponent();
        }

        public void Write(string message)
        {
            textBox.UpdateControl(() =>
            {
                textBox.Text += message;
                textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
            });
        }
    }

    public class TraceFormListener : TraceListener
    {
        private TraceForm traceForm;

        public TraceFormListener(TraceForm traceForm)
        {
            this.traceForm = traceForm;
        }

        public override void Write(string message)
        {
            if (this.traceForm.Visible)
                traceForm.Write(message);
        }

        public override void WriteLine(string message)
        {
            if (this.traceForm.Visible)
                traceForm.Write(message + "\n");
        }
    }
}
