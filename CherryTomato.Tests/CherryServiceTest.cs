using System;
using System.IO;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;
using NUnit.Framework;
using CherryTomato.SystrayIcon;
using CherryTomato.VersionCheck;
using CherryTomato.TestsCore;
using CherryTomato.SystrayIcon.FirstRun;
using CherryTomato.Core.Database;

namespace CherryTomato.Tests
{
    [TestFixture]
    public class CherryServiceTest
    {
        //[Test]
        //public void SimpleTest()
        //{
        //    using (var cs = new CherryService())
        //    {
        //        var iconController = new IconController(Assembly.GetExecutingAssembly());
        //        cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
        //        cs.PluginRepository.RegisterPlugin(new RemindersControllerPlugin());
        //        cs.PluginRepository.RegisterPlugin(iconController);
        //        cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
        //        cs.PluginRepository.TieEvents();

        //        var reminder = new FakeReminder(cs.PluginRepository);
        //        cs.PluginRepository.CherryCommands["Add New Reminder"].PerformCommand(
        //            new ReminderCommandArgs(reminder));
        //        reminder.NotifyOnPoll();

        //        cs.Poll();

        //        Assert.That(iconController.State, Is.EqualTo(IconControllerState.Flashing));
        //    }
        //}

        #region Pomodoro

        [Test]
        public void PomodoroTest()
        {
            using (var cs = new CherryService())
            {
                var ps = new PomodoroSensor();
                cs.PluginRepository.RegisterPlugin(ps);
                var tp = new FakeTimeProvider();
                cs.PluginRepository.RegisterPlugin(tp);
                cs.PluginRepository.RegisterPlugin(new FakeWindowsController());
                cs.PluginRepository.RegisterPlugin(new FakeSettingsController());
                var ic = new IconController();
                cs.PluginRepository.RegisterPlugin(ic);
                cs.PluginRepository.RegisterPlugin(new DatabaseController());

                var im = new FakeImController();
                cs.PluginRepository.RegisterPlugin(im);

                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();

                cs.PluginRepository.CherryCommands["Start Pomodoro"].Do(null);
                ic.UpdateToolTipTextMessage();

                Assert.True(ps.CurrentlyInPomodoro);
                Assert.That(ps.GetCurrentStatusData().Remaining.Minutes, Is.EqualTo(25));
                Assert.That(im.InPomodoro);
                Assert.That(ps.GetCurrentStatusData().MinutesLeft, Is.EqualTo(25));
                Assert.IsTrue(ic.ToolTipText.StartsWith("In pomodoro. 25 minutes left"));

                tp.AdvanceMinutes(3);
                ic.UpdateToolTipTextMessage();
                Assert.True(ps.CurrentlyInPomodoro);
                Assert.That(ps.GetCurrentStatusData().Remaining.Minutes, Is.EqualTo(22));
                Assert.That(ps.GetCurrentStatusData().MinutesLeft, Is.EqualTo(22));
                Assert.IsTrue(ic.ToolTipText.StartsWith("In pomodoro. 22 minutes left"));

                tp.AdvanceMinutes(22);
                ps.StopPomodoroInternal(true);
                Assert.That(ic.ToolTipText, Is.EqualTo("Pomodoro completed"));

                // Poll eniough times to make icon controller stop flashing.
                ic.StopFlashing();

                tp.AdvanceMinutes(10);
                ic.UpdateToolTipTextMessage();
                //Assert.That(cs.GetTimeSinceLastPomodoro().TotalMinutes, Is.EqualTo(10));
                Assert.That(im.InPomodoro, Is.False);
                Assert.IsTrue(ic.ToolTipText.StartsWith("Out of pomodoro for 10 minutes"));

                tp.AdvanceMinutes(59);
                ic.UpdateToolTipTextMessage();
                Assert.IsTrue(ic.ToolTipText.StartsWith("Out of pomodoro for 1 hour"));
            }
        }

        #endregion

        #region Config

        [Test]
        public void SaveConfigTest()
        {
            var ver = new Version(Application.ProductVersion);
            var dbFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cherrytomato\\database.sqlite";
            
            using (var cs = new CherryService())
            {
                var csw = new StringWriter();
                cs.PluginRepository.RegisterPlugin(new VersionChecker());
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());
                cs.PluginRepository.RegisterPlugin(new FirstRunSensor());
                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();

                cs.SaveConfiguration(csw);

                var expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                    "<cherryTomato>\r\n" +
                    "  <config>\r\n" +
                    "    <plugins />\r\n" +
                    "  </config>\r\n" +
                    "  <newestKnownVersion major=\"" + ver.Major + "\" minor=\"" + ver.Minor + "\" />\r\n" +
                    "  <databaseFile path=\"" + dbFile + "\" />\r\n" +
                    "  <hasRun />\r\n" +
                    "</cherryTomato>";

                var actual = csw.GetStringBuilder().ToString();

                Assert.That(actual, Is.EqualTo(expected));
            }
        }

        [Test]
        public void LoadConfigTest()
        {
            var dbFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\cherrytomato\\database.sqlite";
            var ver = new Version(Application.ProductVersion);
            var configString = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n" +
                    "<cherryTomato>\r\n" +
                    "  <hasRun />\r\n" +
                    "  <newestKnownVersion major=\"" + ver.Major + "\" minor=\"" + ver.Minor + "\" />\r\n" +
                    "  <databaseFile path=\"" + dbFile + "\" />\r\n" +
                    "  <config />\r\n" +
                    "</cherryTomato>";

            var reader = new StringReader(configString);
            using (var cs = new CherryService())
            {
                var vc = new VersionChecker();
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());
                var fr = new FirstRunSensor();
                cs.PluginRepository.RegisterPlugin(fr);
                cs.PluginRepository.RegisterPlugin(vc);
                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();
                cs.LoadConfiguration(reader);
                Assert.AreEqual(true, fr.HasRun);
                Assert.AreEqual(ver.Major, vc.NewestKnownVersion.Major);
                Assert.AreEqual(ver.Minor, vc.NewestKnownVersion.Minor);
            }
        }

        #endregion
    }
}