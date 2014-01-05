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
    public class CherryEventTest
    {
        public class TestEventsProvider : ICherryEventsProvider
        {
            public IEnumerable<ICherryEvent> GetEvents()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void CherryEventSimpleTest()
        {
            const string eventName = "event Name";

            var eventsProvider = new TestEventsProvider();
            var sentArgs = new CherryEventArgs();
            var cherryEvent = new CherryEvent(eventName);
            Assert.AreEqual(eventName, cherryEvent.Name);
            cherryEvent.Fire(null);

            bool listenerAdded = cherryEvent.AddListener(new CherryEventListener(
                eventName,
                ea => { }));
            Assert.IsTrue(listenerAdded);

            cherryEvent.Fire(sentArgs);
        }
    }
}
