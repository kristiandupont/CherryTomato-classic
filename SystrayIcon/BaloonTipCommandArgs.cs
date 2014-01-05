using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.CommandsModel;

namespace CherryTomato.SystrayIcon
{
    public class BaloonTipCommandArgs : CherryCommandArgs
    {
        public BaloonState BaloonSettings { get; protected set; }

        public BaloonTipCommandArgs(BaloonState baloonSettings)
        {
            this.BaloonSettings = baloonSettings;
        }
    }
}
