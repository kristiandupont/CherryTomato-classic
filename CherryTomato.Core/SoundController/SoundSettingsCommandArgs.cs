using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.SoundController
{
    public class SoundSettingsCommandArgs : CherryCommandArgs
    {
        public SoundSettings Settings { get; protected set; }

        public SoundSettingsCommandArgs(SoundSettings settings)
        {
            this.Settings = settings;
        }
    }
}
