using System.Diagnostics;
using System.Xml;
using CherryTomato.Core;
using NUnit.Framework;
using CherryTomato.SkypeController;
using CherryTomato.TestsCore;

namespace CherryTomato.SkypeController.Tests
{
    [TestFixture]
    public class SkypeControllerTest
    {
        [Test, Explicit]
        public void TestGetStatus()
        {
            var sc = new CherryTomato.SkypeController.SkypeController();
            Debug.WriteLine("Status: " + sc.Status + "\nStatus text: " + sc.StatusText);
        }

        [Test]
        public void TestLoadConfiguration()
        {
            var sc = new CherryTomato.SkypeController.SkypeController();
            var configString = "<cherryTomato><config><plugins>" +
                "<plugin name=\"Skype\" enabled=\"true\"><inPomodoroStatus>status</inPomodoroStatus></plugin>" +
                "</plugins></config></cherryTomato>";

            var xd = new XmlDocument();
            xd.LoadXml(configString);
            var skypeElement = (XmlElement)xd.SelectSingleNode("cherryTomato/config/plugins/plugin");

            sc.LoadConfiguration(new ConfigurePluginEventArgs(xd));

            Assert.That(sc.Enabled);
            Assert.That(sc.InPomodoroTextTemplate, Is.EqualTo("status"));
        }

        [Test]
        public void TestSaveConfiguration()
        {
            var sc = new CherryTomato.SkypeController.SkypeController();
            sc.InPomodoroTextTemplate = "status";

            var xmlHelper = new XmlConfigurationHelper();
            sc.SaveConfiguration(xmlHelper.GetEventArgs());

            var expectedConfig = "<plugins><plugin name=\"Skype\" enabled=\"True\"><inPomodoroStatus>status</inPomodoroStatus></plugin></plugins>";
            Assert.That(xmlHelper.GetPluginsElement().OuterXml, Is.EqualTo(expectedConfig));
        }

        [Test]
        public void TemplateFixTest()
        {
            var sc = new CherryTomato.SkypeController.SkypeController();

            var t1 = "Non-template text";
            Assert.That(sc.MatchesTemplate(t1), Is.False);

            var t2 = "In pomodoro (1 min to go). Please don't disturb me unless it's important. Thank you.";
            Assert.That(sc.MatchesTemplate(t2), Is.True);

            sc.InPomodoroTextTemplate = "{0} minutes left";

            var t3 = "10 minutes left";
            Assert.That(sc.MatchesTemplate(t3), Is.True);

            sc.InPomodoroTextTemplate = "Minutes left: {0}";

            var t4 = "Minutes left: 4";
            Assert.That(sc.MatchesTemplate(t4), Is.True);

            sc.InPomodoroTextTemplate = "In pomodoro!";

            var t5 = "In pomodoro!";
            Assert.That(sc.MatchesTemplate(t5), Is.True);
        }
    }
}
