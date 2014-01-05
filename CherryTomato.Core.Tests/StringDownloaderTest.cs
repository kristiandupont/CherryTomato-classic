using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CherryTomato.Core;
using System.Threading;

namespace CherryTomato.Core.Tests
{
    [TestFixture]
    public class StringDownloaderTest
    {
        [Test, Explicit]
        public void SimpleTest()
        {
            //var sd = new StringDownloader("http://www.chrylers.com/cherrytomato/versionhistory.xml", DisplayResult);
            var sd = new StringDownloader("http://localhost", DisplayResult);
            sd.Run();
            Thread.Sleep(10000);
            Debug.WriteLine("Test stopped.");
        }

        private void DisplayResult(string returnValue)
        {
            Debug.WriteLine("Output: " + returnValue);
        }

        [Test, Explicit]
        public void TestDownloadString()
        {
            Debug.WriteLine("Output: " + Helpers.DownloadString("http://www.google.com"));

        }
    }
}
