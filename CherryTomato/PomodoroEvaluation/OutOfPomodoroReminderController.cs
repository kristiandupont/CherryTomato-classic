using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.OutOfPomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.PomodoroEvaluation
{
    public class OutOfPomodoroReminderController
    {
        private PluginRepository pluginRepository;

        private OutOfPomodoroReminderControl control;

        public OutOfPomodoroReminderController(OutOfPomodoroReminderControl control, PluginRepository pluginRepository)
        {
            this.pluginRepository = pluginRepository;
            this.control = control;
        }

        public void PopulateData()
        {
            int minutes =
                this.control.Check5Min.Checked ? 5 :
                this.control.Check10Min.Checked ? 10 :
                this.control.Check15Min.Checked ? 15 :
                0;

            this.pluginRepository.CherryCommands["Out Of Pomodoro Trigger"].Do(
                new OutOfPomodoroCommandArgs(minutes));
        }
    }
}
