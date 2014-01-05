using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using CherryTomato;
using System.Diagnostics;

namespace Tests
{
    [TestFixture]
    public class MSNControllerTest
    {
        [Test, Explicit]
        public void TestGetStatus()
        {
            var mc = new MSNController();
            Debug.WriteLine("Status: " + mc.Status + "\nStatus text: " + mc.StatusText);
        }

        [Test]
        public void TestLoadConfiguration()
        {
            var mc = new MSNController();
            var configString = "<cherryTomato><config><plugins>" +
                "<plugin name=\"MSN\" enabled=\"true\"><inPomodoroStatus>status</inPomodoroStatus></plugin>" +
                "</plugins></config></cherryTomato>";

            var xd = new XmlDocument();
            xd.LoadXml(configString);
            var skypeElement = (XmlElement)xd.SelectSingleNode("cherryTomato/config/plugins/plugin");
            
            mc.LoadConfiguration(skypeElement);

            Assert.That(mc.Enabled);
            Assert.That(mc.InPomodoroTextTemplate, Is.EqualTo("status"));
        }

        [Test]
        public void TestSaveConfiguration()
        {
            var mc = new MSNController();
            mc.InPomodoroTextTemplate = "status";

            var xd = new XmlDocument();
            var skypeElement = xd.CreateElement("plugins");

            mc.SaveConfiguration(skypeElement);

            var expectedConfig = "<plugins><plugin name=\"MSN\" enabled=\"True\"><inPomodoroStatus>status</inPomodoroStatus></plugin></plugins>";
            Assert.That(skypeElement.OuterXml, Is.EqualTo(expectedConfig));
        }
    }
}
