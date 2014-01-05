using System.IO;
using CherryTomato.Core;
using CherryTomato.Core.Database;
using CherryTomato.Core.Pomodoro;
using CherryTomato.SystrayIcon.FirstRun;
using CherryTomato.TestsCore;
using NUnit.Framework;

namespace FirstRun.Tests
{
    [TestFixture]
    public class HasRunTests
    {
        [Test]
        public void HasRunTest()
        {
            using (var cs = new CherryService())
            {
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                var fr = new FirstRunSensor();
                cs.PluginRepository.RegisterPlugin(fr);
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());
                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();
                var csr = new StringReader("<cherryTomato><hasRun /></cherryTomato>");
                cs.LoadConfiguration(csr);

                Assert.That(fr.HasRun, Is.True);
            }
        }
    }
}
