using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.ActiveTaskSensor.Tests
{
    class FakeProcessAndWindowSpy : IProcessAndWindowSpy
    {
        public TaskRegistration ActiveTask;
        public TaskRegistration GetActiveTask() { return ActiveTask; }
    }
}
