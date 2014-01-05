using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CherryTomato.PomodoroEvaluation;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation.Tests
{
    [TestFixture]
    public class TasksListControllerTest
    {
        [Test]
        public void DurationFormettingTest()
        {
            var tlc = new TasksListController(null);
            tlc.SetData(new CompletedPomodoro()
                {
                    Duration = TimeSpan.FromMinutes(25),
                    //Start = DateTime.Now,
                    //TaskRegistrations = new List<TaskRegistration>()
                    //{
                    //    new TaskRegistration()
                    //    {
                    //        Duration = TimeSpan.FromSeconds(61),
                    //        ProcessName = "process",
                    //        TaskName = "task",
                    //        TimeStamp = DateTime.Now.AddMinutes(-1),
                    //    }
                    //}
                });

            var durationString = tlc.GetFormattedDuration(TimeSpan.FromSeconds(61));
            Assert.AreEqual("4% (01:01)", durationString);
        }
    }
}
