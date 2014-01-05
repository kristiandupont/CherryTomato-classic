using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.WindowsController
{
    public class WindowCommandArgs : CherryCommandArgs
    {
        public Form Window { get; protected set; }

        public WindowCommandArgs(Form form)
        {
            this.Window = form;
        }
    }
}
