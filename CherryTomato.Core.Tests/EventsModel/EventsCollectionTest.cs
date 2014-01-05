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
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.Tests.EventsModel
{
    [TestFixture]
    public class EventsCollectionTest
    {
        private const string eventName1 = "event Name 1";
        private const string eventName2 = "event Name 2";

        public class TestEventsProvider : ICherryEventsProvider
        {
            private CherryEvent event1 = new CherryEvent(eventName1);
            private CherryEvent event2 = new CherryEvent(eventName2);

            public IEnumerable<ICherryEvent> GetEvents()
            {
                yield return event1;
                yield return event2;
            }

            public void FireFirst()
            {
                this.event1.Fire(new CherryEventArgs());
            }

            public void FireSecond()
            {
                this.event2.Fire(new CherryEventArgs());
            }
        }

        [Test]
        public void EventsCollectionSimpleTest()
        {
            bool firstFired = false;
            bool secondFired = false;

            var eventsProvider = new TestEventsProvider();
            CherryEventsCollection eventsCollection = new CherryEventsCollection();
            eventsCollection.AddProvider(eventsProvider);

            bool subscribeResult;

            var listener1 = new CherryEventListener(
                eventName1,
                ea => firstFired = true);
            subscribeResult = eventsCollection.Subscribe(listener1);
            Assert.IsTrue(subscribeResult);

            var listener2 = new CherryEventListener(
                eventName2,
                ea => secondFired = true);
            subscribeResult = eventsCollection.Subscribe(listener2);
            Assert.IsTrue(subscribeResult);

            subscribeResult = eventsCollection.Subscribe(listener2);
            Assert.IsFalse(subscribeResult);

            Assert.IsFalse(firstFired);
            Assert.IsFalse(secondFired);

            eventsProvider.FireSecond();
            Assert.IsFalse(firstFired);
            Assert.IsTrue(secondFired);

            eventsProvider.FireFirst();
            Assert.IsTrue(firstFired);
            Assert.IsTrue(secondFired);
        }

        public class TestEventsProvider2 : ICherryEventsProvider
        {
            private CherryEvent event1 = new CherryEvent(eventName1);

            public IEnumerable<ICherryEvent> GetEvents()
            {
                yield return event1;
            }

            public void FireFirst()
            {
                this.event1.Fire(new CherryEventArgs());
            }
        }

        [Test]
        public void NoSameEventNameTest()
        {
            var provider1 = new TestEventsProvider();
            var provider2 = new TestEventsProvider2();
            
            CherryEventsCollection eventsCollection = new CherryEventsCollection();
            eventsCollection.AddProvider(provider1);
            Assert.Throws<PluginException>(() => eventsCollection.AddProvider(provider1));
            Assert.Throws<PluginException>(() => eventsCollection.AddProvider(provider2));

            CherryEventsCollection eventsCollection2 = new CherryEventsCollection();
            eventsCollection2.AddProvider(provider2);
            Assert.Throws<PluginException>(() => eventsCollection.AddProvider(provider1));
        }
    }
}
