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

namespace CherryTomato.Core.Tests.EventsModel
{
    [TestFixture]
    public class CherryEventListenerTest
    {
        [Test]
        public void CreateListenerTest()
        {
            const string eventName = "event Name";

            bool succeeded = false;
            var listener = new CherryEventListener(
                eventName,
                ea => succeeded = true);
            listener.Handle(null);
            Assert.AreEqual(eventName, listener.ListenTo);
            listener.Handle(null);
            Assert.IsTrue(succeeded);
        }
    }
}
