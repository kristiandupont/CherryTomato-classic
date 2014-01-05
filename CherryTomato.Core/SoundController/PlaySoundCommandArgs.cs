using CherryTomato.Core.CommandsModel;

namespace CherryTomato.Core.SoundController
{
    public class PlaySoundCommandArgs : CherryCommandArgs
    {
        public string FileName { get; protected set; }

        public PlaySoundCommandArgs(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
