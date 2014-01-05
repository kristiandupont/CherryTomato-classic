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

namespace CherryTomato.Core.Tests.CommandsModel
{
    [TestFixture]
    public class CherryCommandTest
    {
        [Test]
        public void CreateCherryCommandListenerTest()
        {
            CherryCommandArgs sentArgs = new CherryCommandArgs();
            const string listenerName = "listener Name";
            var rl = new CherryCommand(
                listenerName,
                ra => 
                    {
                        Assert.AreEqual(sentArgs, ra);
                        return 1;
                    });

            Assert.AreEqual(listenerName, rl.Name);
            Assert.AreEqual(1, rl.Do(sentArgs));
        }
    }
}
