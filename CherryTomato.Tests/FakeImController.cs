using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Tests
{
    public class FakeImController : IPlugin
    {
        public bool InPomodoro;

        public string PluginName { get { return "Fake Im Controller"; } }

        public void StartPomodoroInternal()
        {
            InPomodoro = true;
        }

        public void StopPomodoroInternal(bool completed)
        {
            InPomodoro = false;
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData.Successful)));
        }
    }
}
