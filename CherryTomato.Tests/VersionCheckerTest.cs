using System;
using NUnit.Framework;

namespace CherryTomato.VersionCheck.Tests
{
    [TestFixture]
    class VersionCheckerTest
    {
        [Test]
        public void SimpleTest()
        {
            var vc = new VersionChecker();
            var v = vc.GetNewestVersion();
            if (v == null)
            {
                // The internet is not available.
                return;
            }

            Assert.That(v, Is.EqualTo(new Version(0, 4)), "Test is not up to date");
        }

        [Test, Explicit]
        public void TestDialog()
        {
            var xmlData = "<cherrytomatoVersionHistory>" + 
                "<version major=\"0\" minor=\"2\">" + 
                "<comment>First release</comment>" + 
                "</version>" + 
                "</cherrytomatoVersionHistory>";

            var currentVersion = new Version(0, 1);

            var vc = new VersionChecker(xmlData, currentVersion);
            vc.CheckNewestVersion(new Version(0, 1));
        }
    }
}
