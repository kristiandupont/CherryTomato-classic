using CherryTomato.Core.CommandsModel;

namespace CherryTomato.UsbLamp
{
    public class FlashUsbLampCommandArgs : CherryCommandArgs
    {
        public FlashingState FlashingSettings { get; protected set; }

        public FlashUsbLampCommandArgs(FlashingState flashingSettings)
        {
            this.FlashingSettings = flashingSettings;
        }
    }
}
