using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using NUnit.Framework;

namespace CherryTomato.Core.Tests
{
    [TestFixture]
    public class HelpersTest
    {
        [Test]
        public void ToAgeStringTest()
        {
            var oneMinute = TimeSpan.FromMinutes(1);
            Assert.That(oneMinute.ToAgeString(false), Is.EqualTo("1 minute"));

            var justOneMinute = oneMinute.Add(TimeSpan.FromMilliseconds(-1));
            Assert.That(justOneMinute.ToAgeString(true), Is.EqualTo("1 minute"));
        }

        [Test]
        public void LoadIconTest()
        {
            Assert.IsNotNull(Helpers.LoadIcon("res://off.ico"));
            Assert.IsNotNull(Helpers.LoadIcon("res://pomodoro.ico"));
            Assert.IsNotNull(Helpers.LoadIcon("res://test.ico"));
        }
    }
}
