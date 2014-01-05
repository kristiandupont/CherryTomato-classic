using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CherryTomato;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using NUnit.Framework;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.TestsCore;
using CherryTomato.Core.Database;

namespace CherryTomato.Core.Tests
{
    [TestFixture]
    public class PomodoroSensorTest : DatabaseTest
    {
        [Test]
        public void SimpleTest()
        {
            var cs = new CherryService();
            var ftp = new FakeTimeProvider { Now = new DateTime(2009, 05, 13) };
            var ps = new PomodoroSensor();
            cs.PluginRepository.RegisterPlugin(ftp);
            cs.PluginRepository.RegisterPlugin(ps);
            cs.PluginRepository.RegisterPlugin(new DatabaseController());
            cs.InitializeCherryServiceEventsAndCommands();
            cs.PluginRepository.TieEvents();
            ps.ConnectToDatabase(dbCon);

            // Create a couple of fake pomodoros.. One for yesterday and two for today.

            dbCon.ExecuteNonQuery(
                "insert into PomodoroRegistrations (TimeStamp, Duration, Evaluation) values (@p1, @p2, @p3);",
                new DateTime(2009, 05, 12),
                new TimeSpan(0, 0, 25, 0).Ticks,
                5);

            dbCon.ExecuteNonQuery(
                "insert into PomodoroRegistrations (TimeStamp, Duration, Evaluation) values (@p1, @p2, @p3);",
                new DateTime(2009, 05, 13, 12, 30, 0),
                new TimeSpan(0, 0, 25, 0).Ticks,
                5);

            dbCon.ExecuteNonQuery(
                "insert into PomodoroRegistrations (TimeStamp, Duration, Evaluation) values (@p1, @p2, @p3);",
                new DateTime(2009, 05, 13, 13, 30, 0),
                new TimeSpan(0, 0, 25, 0).Ticks,
                5);

            // And connect again to load those into memory
            ps.ConnectToDatabase(dbCon);

            // There are two pomodoros for today. 5 points each.
            Assert.That(ps.TodayProductivity.Pomodoros, Is.EqualTo(2));
            Assert.That(ps.TodayProductivity.Rating, Is.EqualTo(10));

            ps.StartPomodoroInternal();
            ftp.AdvanceMinutes(10);
            ps.StopPomodoroInternal(false);
        }

        [Test]
        public void PomodoroAllEventsTest()
        {
            var cs = new CherryService();
            var ftp = new FakeTimeProvider { Now = new DateTime(2009, 05, 13) };
            var ps = new PomodoroSensor();
            cs.PluginRepository.RegisterPlugin(ftp);
            cs.PluginRepository.RegisterPlugin(ps);
            cs.PluginRepository.RegisterPlugin(new DatabaseController());
            cs.InitializeCherryServiceEventsAndCommands();
            cs.PluginRepository.TieEvents();
            var eventChecker = new EventFiredChecker(cs);
            eventChecker.WaitFor("Pomodoro Started", "Pomodoro Finishing", "Pomodoro Finished");

            Assert.IsTrue((bool)eventChecker.InvokeCommand("Start Pomodoro", null));

            eventChecker.AssertFired("Pomodoro Started");
            eventChecker.AssertNotFired("Pomodoro Finishing", "Pomodoro Finished");
            ftp.AdvanceMinutes(25);
            Assert.IsTrue((bool)eventChecker.InvokeCommand("Stop Pomodoro", null));
            eventChecker.AssertFired("Pomodoro Finishing", "Pomodoro Finished");

            eventChecker.Reset();

            Assert.IsTrue((bool)eventChecker.InvokeCommand("Start Pomodoro", null));

            eventChecker.AssertFired("Pomodoro Started");
            eventChecker.AssertNotFired("Pomodoro Finishing", "Pomodoro Finished");
            ftp.AdvanceMinutes(25);
            Assert.IsTrue((bool)eventChecker.InvokeCommand("Void Pomodoro", null));
            eventChecker.AssertFired("Pomodoro Finishing", "Pomodoro Finished");
        }
    }
}
