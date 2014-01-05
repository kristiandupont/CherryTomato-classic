using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CherryTomato.Reminders.TimeOfDayConditionChecker;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.TestsCore;
using CherryTomato.Core;

namespace TimeOfDayConditionChecker.Tests
{
    [TestFixture]
    public class CheckConditionTest
    {
        [Test]
        public void TestCondition()
        {
            var cond = new TimeOfDayCondition()
            {
                StartTime = TimeSpan.FromHours(8),
                EndTime = TimeSpan.FromHours(9),
                ShouldBeWithin = true,
            };

            var checker = new CherryTomato.Reminders.TimeOfDayConditionChecker.TimeOfDayConditionChecker();
            var cs = new CherryService();
            cs.PluginRepository.RegisterPlugin(checker);
            var tp = new FakeTimeProvider();
            cs.PluginRepository.RegisterPlugin(tp);
            cs.InitializeCherryServiceEventsAndCommands();
            cs.PluginRepository.TieEvents();

            tp.Now = DateTime.Today.AddHours(8).AddMinutes(1);
            Assert.IsTrue(checker.IsTrue(cond));

            tp.Now = DateTime.Today.AddHours(9).AddMinutes(1);
            Assert.IsFalse(checker.IsTrue(cond));

            cond.ShouldBeWithin = false;
            Assert.IsTrue(checker.IsTrue(cond));

            tp.Now = DateTime.Today.AddHours(8).AddMinutes(1);
            Assert.IsFalse(checker.IsTrue(cond));
        }
    }
}
