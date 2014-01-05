using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.TimeProvider;
using CherryTomato.Core;
using Quartz;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.SystrayIcon
{
    public partial class IconController
    {
        private Flasher flasher;

        public class Flasher
        {
            private IconController controller;

            public Flasher(IconController c)
            {
                this.controller = c;
            }

            public bool IsFlashing { get; set; }

            public bool StartFlashing(FlashingState flashingData)
            {
                if ((flashingData.FirstIcon == IconType.Special && flashingData.FirstSpecialIcon == null) ||
                    (flashingData.SecondIcon == IconType.Special && flashingData.SecondSpecialIcon == null))
                {
                    throw new PluginException("Please provide icon when trying to flash with a special icon.");
                }

                if (this.IsFlashing)
                {
                    // Is any other flashing is going at the moment, then stop it and start new flashing.
                    this.StopFlashing();
                }

                this.IsFlashing = true;
                this.controller.renderer.ToolTipText = flashingData.ToolTipMessage;
                int counter = flashingData.FlashesCount * 2;
                this.controller.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                    "Quarter Second Elapsed for Icon Controller",
                    // fired on every 250 milliseconds
                    new SimpleTrigger(
                        "Quarter Second Elapsed for Icon Controller",
                        counter,
                        TimeSpan.FromMilliseconds(250)),
                    () =>
                    {
                        counter--;

                        if (counter % 2 == 0)
                        {
                            this.controller.renderer.SetIconType(flashingData.FirstIcon, flashingData.FirstSpecialIcon);
                        }
                        else
                        {
                            this.controller.renderer.SetIconType(flashingData.SecondIcon, flashingData.SecondSpecialIcon);
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
                    "Quarter Second Elapsed for Icon Controller"));
                this.controller.renderer.SetIconType(IconType.Idle);
                this.IsFlashing = false;
            }
        }
    }
}
