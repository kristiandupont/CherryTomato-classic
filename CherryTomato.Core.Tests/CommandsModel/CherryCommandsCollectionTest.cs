using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CherryTomato;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using NUnit.Framework;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.Tests.CommandsModel
{
    [TestFixture]
    public class CherryCommandsCollectionTest
    {
        const string listenerName = "listener Name";

        private class TestCherryCommandListenersProvider : ICherryCommandsProvider
        {
            private CherryCommand listener = new CherryCommand(
                listenerName,
                ra => ra);

            public IEnumerable<ICherryCommand> GetCommands()
            {
                yield return this.listener;
            }
        }

        [Test]
        public void CherryCommandListenersCollectionSimpleTest()
        {
            var collection = new CherryCommandsCollection();
            var listenersProvider = new TestCherryCommandListenersProvider();
            collection.AddProvider(listenersProvider);

            var ra = new CherryCommandArgs();
            Assert.AreEqual(ra, collection[listenerName].Do(ra));

            Assert.Throws<PluginException>(() => collection["non existent listener"].Do(null));

            // Do not add same listener name twice.
            Assert.Throws<PluginException>(() => collection.AddProvider(new TestCherryCommandListenersProvider()));
        }
    }
}
