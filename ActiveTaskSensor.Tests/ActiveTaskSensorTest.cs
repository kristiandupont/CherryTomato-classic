using System;
using CherryTomato.ActiveTaskSensor;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;
using NUnit.Framework;
using CherryTomato.TestsCore;
using CherryTomato.Core.Database;

namespace CherryTomato.ActiveTaskSensor.Tests
{
    [TestFixture]
    public class ActiveTaskSensorTest
    {
        [Test]
        public void SimpleTest()
        {
            var fpaws = new FakeProcessAndWindowSpy();
            var ftp = new FakeTimeProvider {Now = new DateTime(2009, 05, 13)};

            var ats = new CherryTomato.ActiveTaskSensor.ActiveTaskSensor(fpaws);
            var cs = new CherryService();
            var ps = new PomodoroSensor();
            cs.PluginRepository.RegisterPlugin(ftp);
            cs.PluginRepository.RegisterPlugin(ats);
            cs.PluginRepository.RegisterPlugin(ps);
            cs.PluginRepository.RegisterPlugin(new DatabaseController());
            cs.InitializeCherryServiceEventsAndCommands();
            cs.PluginRepository.TieEvents();

            fpaws.ActiveTask = new TaskRegistration { TaskName = "Task1", ProcessName = "Task1" };
            ps.StartPomodoroInternal();
            fpaws.ActiveTask = new TaskRegistration { TaskName = "Task1", ProcessName = "Task1" };
            ats.CountActiveTask();
            ftp.AdvanceMinutes(10);
            ats.CountActiveTask();
            fpaws.ActiveTask = new TaskRegistration { TaskName = "Task2", ProcessName = "Task2" };
            ats.CountActiveTask();
        }
    }
}
