using System.Drawing;

namespace CherryTomato.UsbLamp
{
    public class FlashingState
    {
        public int FlashesCount { get; set; }
        public ColorType FirstColor { get; set; }
        public ColorType SecondColor { get; set; }
        public Color FirstSpecialColor { get; set; }
        public Color SecondSpecialColor { get; set; }
    }
}
