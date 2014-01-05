using System.Collections.Generic;
using System.IO;
using System.Linq;
using CherryTomato.Core;
using CherryTomato.Core.Database;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Reminders.Core;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.TestsCore;
using NUnit.Framework;

namespace CherryTomato.Reminders.Tests
{
    [TestFixture]
    public class ReadReminder
    {
        [Test]
        public void ReadReminderTest()
        {
            using (var cs = new CherryService())
            {
                cs.PluginRepository.RegisterPlugin(new FakeReminderPlugin());
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                cs.PluginRepository.RegisterPlugin(new RemindersCorePlugin());
                cs.PluginRepository.RegisterPlugin(new ReminderPluginsRepository());
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new FakeWindowsController());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());

                var csr = new StringReader("<cherryTomato><config>" +
                    "<reminders><reminder typeName=\"FakeReminder\" /></reminders>" +
                    "</config></cherryTomato>");

                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();
                cs.LoadConfiguration(csr);

                var allReminders = cs.PluginRepository.CherryCommands["Get All Reminders"].Do(null)
                    as IEnumerable<IReminder>;
                Assert.That(allReminders.FirstOrDefault().TypeName, Is.EqualTo("FakeReminder"));
            }
        }
    }
}
