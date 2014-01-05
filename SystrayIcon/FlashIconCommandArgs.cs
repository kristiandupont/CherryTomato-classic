using CherryTomato.Core.CommandsModel;
using CherryTomato.SystrayIcon;

namespace CherryTomato.SystrayIcon
{
    public class FlashIconCommandArgs : CherryCommandArgs
    {
        public FlashingState FlashingSettings { get; protected set; }

        public FlashIconCommandArgs(FlashingState flashingSettings)
        {
            this.FlashingSettings = flashingSettings;
        }
    }
}
