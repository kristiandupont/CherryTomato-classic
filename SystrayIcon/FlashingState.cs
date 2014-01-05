using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CherryTomato.SystrayIcon
{
    public class FlashingState
    {
        public int FlashesCount { get; set; }
        public string ToolTipMessage { get; set; }
        public IconType FirstIcon { get; set; }
        public IconType SecondIcon { get; set; }
        public Icon FirstSpecialIcon { get; set; }
        public Icon SecondSpecialIcon { get; set; }
    }
}
