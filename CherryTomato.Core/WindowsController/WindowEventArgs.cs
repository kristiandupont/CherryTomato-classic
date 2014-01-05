using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CherryTomato.Core.EventsModel;

namespace CherryTomato.Core.WindowsController
{
    public class WindowEventArgs : CherryEventArgs
    {
        public Form Window { get; protected set; }

        public WindowEventArgs(Form form)
        {
            this.Window = form;
        }
    }
}
