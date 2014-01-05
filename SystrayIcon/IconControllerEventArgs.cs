using System.Collections.Generic;
using System.Windows.Forms;
using CherryTomato.Core.EventsModel;

namespace CherryTomato.SystrayIcon
{
    public class IconControllerEventArgs : CherryEventArgs
    {
        public List<ToolStripItem> MenuItems { get; set; }

        public IconControllerEventArgs()
        {
            this.MenuItems = new List<ToolStripItem>();
        }
    }
}
