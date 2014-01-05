using System;
using System.Reflection;
using System.Windows.Forms;
using CherryTomato;
using CherryTomato.Core;
using NUnit.Framework;
using CherryTomato.SystrayIcon;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.TimeProvider;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.SystrayIconNotifier;

namespace CherryTomato.SystrayIcon.Tests
{
    [TestFixture]
    public class IconControllerTest
    {
        [Test, Explicit]
        public void ManualUITest()
        {
            using (new IconController())
            {
                Assert.That(MessageBox.Show("Is there a systray icon?", "Manual assert", MessageBoxButtons.YesNo),
                            Is.EqualTo(DialogResult.Yes));
            }
        }

        [Test, Explicit]
        public void ManualFlashTest()
        {
            var cs = new CherryService();
            var tp = new TimeProvider();
            var ps = new PomodoroSensor();
            var ic = new IconController();
            var icn = new CherryTomato.Reminders.SystrayIconNotifier.IconNotifier();

            cs.PluginRepository.RegisterPlugin(tp);
            cs.PluginRepository.RegisterPlugin(ps);
            cs.PluginRepository.RegisterPlugin(ic);
            cs.PluginRepository.RegisterPlugin(icn);
            cs.InitializeCherryServiceEventsAndCommands();
            cs.PluginRepository.TieEvents();

            icn.Notify(new IconNotification { FlashCount = 10, FlashIconPath = "res://pomodoro.ico", NotificationText = "Flash test" });

            Assert.That(MessageBox.Show("Did the icon flash?", "Manual assert", MessageBoxButtons.YesNo),
                        Is.EqualTo(DialogResult.Yes));
            
            cs.Dispose();
        }
    }
}