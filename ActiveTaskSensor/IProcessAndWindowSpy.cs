using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.ActiveTaskSensor
{
    public interface IProcessAndWindowSpy
    {
        TaskRegistration GetActiveTask();
    }
}
