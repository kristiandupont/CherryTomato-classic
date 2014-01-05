using System;
using System.Collections.Generic;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.TimeProvider;
using Quartz;

namespace CherryTomato.ActiveTaskSensor
{
    public class ActiveTaskSensor : IPlugin
    {
        private readonly IProcessAndWindowSpy processAndWindowSpy;

        private TaskRegistration currentlyActiveTask;
        private DateTime taskStart;

        public ActiveTaskSensor() : this(new ProcessAndWindowSpy()) { }

        public ActiveTaskSensor(IProcessAndWindowSpy processAndWindowSpy)
        {
            this.processAndWindowSpy = processAndWindowSpy;
        }

        public string PluginName { get { return "Active Task Sensor"; } }

        private ICherryCommand now;

        private DateTime Now
        {
            get
            {
                return (DateTime)this.now.Do(null);
            }
        }

        private DateTime lastPoll;

        private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);

        public void CountActiveTask()
        {
            if (this.lastPoll == default(DateTime))
            {
                return;
            }

            var timeNow = this.Now;

            // If it's more than a minute since we polled last time, the computer has likely been in stand-by etc.
            var timeSinceLastPoll = timeNow - this.lastPoll;
            var interrupted = timeSinceLastPoll > OneMinute;

            var newTask = processAndWindowSpy.GetActiveTask();
            if (newTask.TaskName != currentlyActiveTask.TaskName || interrupted)
            {
                var endTime = interrupted ? this.lastPoll : timeNow;
                this.AddTaskRegistration(newTask, endTime);
            }

            this.lastPoll = timeNow;
        }

        private void AddTaskRegistration(TaskRegistration newTask, DateTime endTime)
        {
            // calculate duration of previous task and register it.
            if (this.currentlyActiveTask != null && !string.IsNullOrEmpty(this.currentlyActiveTask.TaskName))
            {
                this.currentlyActiveTask.TimeStamp = taskStart;
                this.currentlyActiveTask.Duration = endTime - taskStart;
                
                this.PomodoroRegistrations.Add(currentlyActiveTask);
            }

            // now we have a new task running.
            this.taskStart = this.Now;
            this.currentlyActiveTask = newTask;
        }

        public List<TaskRegistration> PomodoroRegistrations { get; private set; }

        public void StartPomodoroInternal()
        {
            var timeNow = this.Now;
            this.lastPoll = timeNow;
            this.taskStart = timeNow;
            this.PomodoroRegistrations = new List<TaskRegistration>();
            this.AddTaskRegistration(processAndWindowSpy.GetActiveTask(), timeNow);

            this.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                "Quarter Second Elapsed for Active Task Sensor",
                // fired on every 250 milliseconds
                new SimpleTrigger("Quarter Second Elapsed", int.MaxValue,  TimeSpan.FromMilliseconds(250)),
                this.CountActiveTask)); // add the task to array
        }

        public void StopPomodoroInternal(CompletedPomodoro pomodoroData)
        {
            this.AddTaskRegistration(this.processAndWindowSpy.GetActiveTask(), this.Now);
            pomodoroData.TaskRegistrations = this.PomodoroRegistrations;

            // remove the trigger as of no need
            this.removeExistingTriggerCommand.Do(new TimeTriggerCommandArgs("Quarter Second Elapsed for Active Task Sensor"));

            this.lastPoll = default(DateTime);
        }

        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;

        public void TieEvents(PluginRepository plugins)
        {
            now = plugins.CherryCommands["Get Current Time"];
            this.lastPoll = this.Now;

            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData)));

            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];
        }
    }
}
