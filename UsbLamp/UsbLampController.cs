using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.Pomodoro;
using System.Collections.Generic;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.UsbLamp
{
    /// <summary>
    /// This plugin controls a USB lamp. 
    /// </summary>
    public partial class UsbLampController : IPlugin, ICherryCommandsProvider
    {
        public UsbLampController()
        {
            this.flasher = new Flasher(this);
            this.renderer = new Renderer(this);
            this.flashLampCommand = new CherryCommand(
                "Flash Usb Lamp",
                fulca => this.flasher.StartFlashing((fulca as FlashUsbLampCommandArgs).FlashingSettings),
                "Perform USB lamp flashing. Command arguments should bring all necessary data.");
        }

        public string PluginName { get { return "Usb Lamp"; } }

        public void StartPomodoroInternal()
        {
            this.renderer.SetColorType(ColorType.Pomodoro);
        }

        public void StopPomodoroInternal(bool completed)
        {
            if (completed)  // Finished pomodoro
            {
                this.flasher.StartFlashing(new FlashingState
                {
                    FlashesCount = 20,
                    FirstColor = ColorType.Idle,
                    SecondColor = ColorType.Pomodoro,
                });
            }
            else
            {
                this.renderer.SetColorType(ColorType.Idle);
            }
        }

        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData.Successful)));

            plugins.CherryEvents.Subscribe("Application Started", this.renderer.Initialize);

            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];
        }

        private CherryCommand flashLampCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.flashLampCommand;
        }
    }
}
