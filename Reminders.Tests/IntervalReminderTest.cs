using System;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.Database;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Reminders.Core;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.IntervalReminder;
using CherryTomato.Reminders.SoundNotifier;
using CherryTomato.Reminders.SystrayIconNotifier;
using CherryTomato.SystrayIcon;
using CherryTomato.TestsCore;
using NUnit.Framework;
using CherryTomato.Reminders.Core.Conditions;

namespace CherryTomato.Reminders.Tests
{
    [TestFixture]
    public class IntervalReminderTest
    {
        //[Test]
        //public void LargeSpanTest()
        //{
        //    var pluginRepository = new PluginRepository();
        //    var ir = new IntervalReminder(pluginRepository, new TimeSpan(0, 0, 50, 0), new TimeSpan(0, 1, 10, 0));

        //    Assert.That(ir.CurrentInterval, Is.GreaterThanOrEqualTo(new TimeSpan(0, 0, 50, 0)));
        //    Assert.That(ir.CurrentInterval, Is.LessThanOrEqualTo(new TimeSpan(0, 1, 10, 0)));
        //}

        [Test]
        public void LoadConfigTest()
        {
            using (var cs = new CherryService())
            {
                var irp = new IntervalReminderPlugin();
                var configString = "<cherryTomato><config><reminders>" +
                                   "<reminder enabled=\"True\" typeName=\"Interval Reminder\" name=\"name\" fromInterval=\"00:17:00\" toInterval=\"00:18:00\">" +
                                   "<compositeNotification>" +
                                   "<notification typeName=\"IconNotification\" enabled=\"True\" flashIconPath=\"res://pomodoro.ico\" flashCount=\"5\" notificationText=\"notificationtext\" />" +
                                   "<notification typeName=\"SoundNotification\" enabled=\"True\" soundPath=\"spacey.wav\" />" +
                                   "</compositeNotification>" +
                                   "<description>description</description>" +
                                   "</reminder>" +
                                   "</reminders></config></cherryTomato>";


                cs.PluginRepository.RegisterPlugin(new FakeIconController());
                cs.PluginRepository.RegisterPlugin(irp);
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Core.SoundController.SoundController());
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Reminders.SystrayIconNotifier.IconNotifier());
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Reminders.SoundNotifier.SoundNotifier());
                cs.PluginRepository.RegisterPlugin(new NotifyPluginsRepository());
                cs.PluginRepository.RegisterPlugin(new ConditionCheckerPluginsRepository());
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new FakeWindowsController());
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());
                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();

                var xd = new XmlDocument();
                xd.LoadXml(configString);
                var reminderElement = (XmlElement)xd.SelectSingleNode("cherryTomato/config/reminders/reminder");
                var ir = (CherryTomato.Reminders.IntervalReminder.IntervalReminder)irp.LoadReminder(reminderElement);
                Assert.That(ir.Name, Is.EqualTo("name"));
                Assert.That(ir.Enabled);
                Assert.That(ir.FromInterval, Is.EqualTo(new TimeSpan(0, 0, 17, 0)));
                Assert.That(ir.ToInterval, Is.EqualTo(new TimeSpan(0, 0, 18, 0)));
                Assert.That(ir.CompositeNotification.GetNotification("IconNotification").Enabled);
                Assert.That(
                    ((IconNotification)ir.CompositeNotification.GetNotification("IconNotification"))
                        .FlashCount, Is.EqualTo(5));
                Assert.That(
                    ((IconNotification)ir.CompositeNotification.GetNotification("IconNotification"))
                        .FlashIconPath, Is.EqualTo("res://pomodoro.ico"));
                Assert.That(ir.CompositeNotification.GetNotification("SoundNotification").Enabled);
                Assert.That(
                    ((SoundNotification)
                     ir.CompositeNotification.GetNotification("SoundNotification")).SoundPath,
                    Is.EqualTo("spacey.wav"));
                Assert.That(ir.Description, Is.EqualTo("description"));
            }
        }

        [Test]
        public void SaveConfigTest()
        {
            using (var cs = new CherryService())
            {
                var irp = new IntervalReminderPlugin();
                cs.PluginRepository.RegisterPlugin(new FakeIconController());
                cs.PluginRepository.RegisterPlugin(irp);
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Core.SoundController.SoundController());
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Reminders.SystrayIconNotifier.IconNotifier());
                cs.PluginRepository.RegisterPlugin(new CherryTomato.Reminders.SoundNotifier.SoundNotifier());
                cs.PluginRepository.RegisterPlugin(new NotifyPluginsRepository());
                cs.PluginRepository.RegisterPlugin(new ConditionCheckerPluginsRepository());
                cs.PluginRepository.RegisterPlugin(new FakeTimeProvider());
                cs.PluginRepository.RegisterPlugin(new FakeWindowsController());
                cs.PluginRepository.RegisterPlugin(new DatabaseController());
                cs.PluginRepository.RegisterPlugin(new PomodoroSensor());
                cs.InitializeCherryServiceEventsAndCommands();
                cs.PluginRepository.TieEvents();

                var ir = new CherryTomato.Reminders.IntervalReminder.IntervalReminder(cs.PluginRepository)
                {
                    FromInterval = new TimeSpan(0, 0, 17, 0),
                    ToInterval = new TimeSpan(0, 0, 18, 0),
                    Enabled = false,
                    Name = "name",
                    Description = "description",
                    CompositeNotification = new CompositeNotification(cs.PluginRepository,
                        new IconNotification { Enabled = true, NotificationText = "ToolTipText", FlashCount = 5, FlashIconPath = "res://pomodoro.ico" },
                        new SoundNotification { SoundPath = "spacey.wav" }),
                };

                var xd = new XmlDocument();
                var remindersElement = xd.CreateElement("reminders");

                irp.SaveReminder(ir, remindersElement);

                var expectedConfig = "<reminders>" +
                   "<reminder enabled=\"False\" typeName=\"Interval Reminder\" name=\"name\" fromInterval=\"00:17:00\" toInterval=\"00:18:00\">" +
                   "<description>description</description>" +
                   "<compositeNotification>" +
                   "<notification typeName=\"IconNotification\" enabled=\"True\" notificationText=\"ToolTipText\" flashIconPath=\"res://pomodoro.ico\" flashCount=\"5\" />" +
                    "<notification typeName=\"SoundNotification\" enabled=\"False\" soundPath=\"spacey.wav\" />" +
                   "</compositeNotification>" +
                   "<compositeCondition />" +
                   "</reminder>" +
                   "</reminders>";
                Assert.That(remindersElement.OuterXml, Is.EqualTo(expectedConfig));
            };
        }
    }
}