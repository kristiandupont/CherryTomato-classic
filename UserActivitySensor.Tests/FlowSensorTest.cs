using System.Windows.Forms;
using System.Xml;
using NUnit.Framework;
using CherryTomato.UserActivity;
using CherryTomato.Core;
using CherryTomato.TestsCore;

namespace CherryTomato.UserActivitySensor.Tests
{
#pragma warning disable
    public class FakeUserActivityHook : IUserActivityHook
    {
        public event MouseEventHandler OnMouseActivity;
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;
        public void Start() { }
        public void Stop() { }
    }
#pragma warning restore

    [TestFixture]
    public class FlowSensorTest
    {
        [Test]
        public void SimpleTest()
        {
            var activityHook = new FakeUserActivityHook();
            var tp = new FakeTimeProvider();

            var fs = new FlowSensor(activityHook);
        }

        [Test]
        public void TestLoadConfiguration()
        {
            var fs = new FlowSensor();
            var configString = "<cherryTomato><config><plugins>" +
                "<plugin name=\"User Activity\" enabled=\"True\" />" +
                "</plugins></config></cherryTomato>";

            var xd = new XmlDocument();
            xd.LoadXml(configString);
            var userActivityElement = (XmlElement)xd.SelectSingleNode("cherryTomato/config/plugins/plugin");

            fs.LoadConfiguration(new ConfigurePluginEventArgs(xd));

            Assert.That(fs.Enabled);
        }

        [Test]
        public void TestSaveConfiguration()
        {
            var fs = new FlowSensor();

            var xmlHelper = new XmlConfigurationHelper();
            fs.SaveConfiguration(xmlHelper.GetEventArgs());

            var expectedConfig = "<plugins><plugin name=\"User Activity\" enabled=\"True\" /></plugins>";
            Assert.That(xmlHelper.GetPluginsElement().OuterXml, Is.EqualTo(expectedConfig));
        }
    }
}
