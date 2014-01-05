using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.WindowsController;

namespace CherryTomato.PomodoroEvaluation
{
    public class EvaluationSensor : IPlugin
    {
        private PomodoroEvaluationForm pomodoroEvaluationForm;
        private ICherryCommand showNoActivateCommand;

        public string PluginName
        {
            get { return "Evaluation Sensor"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.showNoActivateCommand = plugins.CherryCommands["Show Window No Activate"];
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finished",
                ea => 
                {
                    var pomodoro = (ea as PomodoroEventArgs).PomodoroData;
                    if (pomodoro.Successful)
                    {
                        this.pomodoroEvaluationForm.SetData(pomodoro);
                        this.showNoActivateCommand.Do(new WindowCommandArgs(this.pomodoroEvaluationForm));
                    }
                }));

            this.pomodoroEvaluationForm = new PomodoroEvaluationForm(plugins);
        }
    }
}
