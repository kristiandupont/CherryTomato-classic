using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace CherryFirewallTest
{
    [TestFixture]
    public class CherryFirewallTest
    {
        [Test]
        public void SimpleTest()
        {
            var cfw = new CherryFirewall.CherryFirewall();

            var testBefore = WebRequest.Create("http://www.yahoo.com").GetResponse().GetResponseStream();
            Debug.WriteLine(testBefore);

            cfw.AddAddress("www.yahoo.com");

            cfw.StartPomodoro(TimeSpan.FromMinutes(25));
            cfw.StopPomodoro(false);
        }
    }
}
