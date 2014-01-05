using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.TimeProvider;
using Quartz;
using System.Drawing;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.UsbLamp
{
    public partial class UsbLampController
    {
        private Flasher flasher;

        public class Flasher
        {
            private UsbLampController controller;

            public Flasher(UsbLampController c)
            {
                this.controller = c;
            }

            public bool IsFlashing { get; set; }

            public bool StartFlashing(FlashingState flashingData)
            {
                if ((flashingData.FirstColor == ColorType.Special && flashingData.FirstSpecialColor == null) ||
                    (flashingData.SecondColor == ColorType.Special && flashingData.SecondSpecialColor == null))
                {
                    throw new PluginException("Please provide color when trying to flash with a special color.");
                }

                if (this.IsFlashing)
                {
                    // Is any other flashing is going at the moment, then stop it and start new flashing.
                    this.StopFlashing();
                }

                this.IsFlashing = true;
                int counter = flashingData.FlashesCount * 2;
                this.controller.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                    "Quarter Second Elapsed for Usb Lamp Controller",
                    // fired on every 250 milliseconds
                    new SimpleTrigger(
                        "Quarter Second Elapsed for Usb Lamp Controller",
                        counter,
                        TimeSpan.FromMilliseconds(250)),
                    () =>
                    {
                        counter--;

                        if (counter % 2 == 0)
                        {
                            this.controller.renderer.SetColorType(flashingData.FirstColor, flashingData.FirstSpecialColor);
                        }
                        else
                        {
                            this.controller.renderer.SetColorType(flashingData.SecondColor, flashingData.SecondSpecialColor);
                        }

                        if (counter <= 0)
                        {
                            this.StopFlashing();
                        }
                    }));

                return true;
            }

            public void StopFlashing()
            {
                this.controller.removeExistingTriggerCommand.Do(new TimeTriggerCommandArgs(
                    "Quarter Second Elapsed for Usb Lamp Controller"));
                this.controller.renderer.SetColorType(ColorType.Idle);
                this.IsFlashing = false;
            }
        }
    }
}
