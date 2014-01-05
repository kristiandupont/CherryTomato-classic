using System;
using System.Collections.Generic;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using Quartz;
using Quartz.Impl;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.TimeProvider
{
    public class TimeProvider : IPlugin, ICherryCommandsProvider, IDisposable
    {
        private const string jobsGroup = "DEFAULT";
        private const string jobSuffix = " Job";
        private const string triggerSuffix = " Job";

        private CherryCommand nowCommand;
        private CherryCommand scheduleActionCommand;
        private CherryCommand addNewTriggerCommand;
        private CherryCommand removeExistingTriggerCommand;
        private IScheduler sched;

        public TimeProvider()
        {
            this.nowCommand = new CherryCommand(
                "Get Current Time", 
                ca => DateTime.Now,
                "Returns current time. Present in the system for unit testing purposes.");
            this.addNewTriggerCommand = new CherryCommand(
                "Add New Time Trigger",
                ca => this.ScheduleTrigger(ca as TimeTriggerCommandArgs),
                "Adds time trigger to the system. Uses standard Quartz trigger. Make sure time is in UTC format.");
            this.removeExistingTriggerCommand = new CherryCommand(
                "Remove Existing Time Trigger",
                ca =>
                {
                    if (!this.sched.IsShutdown)
                    {
                        var jobName = (ca as TimeTriggerCommandArgs).ActionName + jobSuffix;
                        return this.sched.DeleteJob(jobName, jobsGroup);
                    }

                    return true;
                },
                "Removes existing time trigger by its name.");
            this.scheduleActionCommand = new CherryCommand(
                "Schedule Single Action",
                ca => this.ScheduleAction(ca as SingleActionCommandArgs),
                "Schedule action to execute it once. After the execution the time trigger is removed automatically. Make sure time is in local time format.");

            this.InitializeScheduler();
        }

        private bool ScheduleTrigger(TimeTriggerCommandArgs ttca)
        {
            var job = new JobDetail(ttca.ActionName + jobSuffix, typeof(ActionJob));
            job.JobDataMap["action"] = ttca.Action;
            job.Group = jobsGroup;
            Trigger trigger = ttca.TimeTrigger;
            trigger.Name = ttca.ActionName + triggerSuffix;
            this.ScheduleTriggerForJob(job, trigger);

            return true;
        }

        private bool ScheduleAction(SingleActionCommandArgs saca)
        {
            var job = new JobDetail(saca.ActionName + jobSuffix, typeof(ActionJob));
            job.JobDataMap["action"] = new Action(() =>
                {
                    this.sched.DeleteJob(job.Name, jobsGroup);
                    saca.Action();
                });
            Trigger trigger = new SimpleTrigger(
                saca.ActionName + triggerSuffix,
                saca.LocalTime.ToUniversalTime());
            this.ScheduleTriggerForJob(job, trigger);
            System.Diagnostics.Trace.WriteLine("Will be triggered at " + saca.LocalTime);

            return true;
        }

        private void ScheduleTriggerForJob(JobDetail job, Trigger trigger)
        {
            System.Diagnostics.Trace.WriteLine("Scheduling job named: '" + job.Name + "'.");
            this.sched.ScheduleJob(job, trigger);
        }

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.nowCommand;
            yield return this.scheduleActionCommand;
            yield return this.addNewTriggerCommand;
            yield return this.removeExistingTriggerCommand;
        }

        public string PluginName
        {
            get { return "Time Provider"; }
        }

        public void InitializeScheduler()
        {
            this.sched = new StdSchedulerFactory().GetScheduler();
            this.sched.Start();
        }

        public void TieEvents(PluginRepository plugins)
        {
            // Time provider provide commands but do not depend on any other stuff.
        }

        private class ActionJob : IJob
        {
            public void Execute(JobExecutionContext context)
            {
                try
                {
                    (context.JobDetail.JobDataMap["action"] as Action)();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.WriteLine(
                        "Unhandled exception was caught. Message: " + ex.Message + Environment.NewLine +
                        "Stack trace: " + Environment.NewLine +
                        ex.StackTrace);
                }
            }
        }

        public void Dispose()
        {
            this.sched.Shutdown();
        }
    }
}
