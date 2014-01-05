using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.PomodoroEvaluation;
using NUnit.Framework;

namespace CherryTomato.PomodoroEvaluation.Tests
{
    [TestFixture]
    public class PomodoroEvaluationTest
    {
        [Test, Ignore("Denne test virker ikke endnu..")]
        public void SimpleTest()
        {
            var cs = new CherryService();

            //cs.PluginRepository.RegisterPlugin(new IconController());
            cs.PluginRepository.RegisterPlugin(new PomodoroSensor());

            var pe = new PomodoroEvaluationForm(cs.PluginRepository);

            pe.ShowDialog();
        }
    }
}
