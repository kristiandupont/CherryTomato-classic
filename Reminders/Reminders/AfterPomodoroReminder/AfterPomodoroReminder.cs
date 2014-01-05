using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.TimeProvider;

namespace CherryTomato.Reminders.AfterPomodoroReminder
{
    public class AfterPomodoroReminder : Reminder
    {
        private CherryEventListener startedListener;
        private CherryEventListener stoppedListener;

        private bool EventsSubscribed { get; set; }

        /// <summary>
        /// Minutes.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        public bool Started { get; set; }

        public bool Rung { get; set; }

        public AfterPomodoroReminder()
        {
        }

        public AfterPomodoroReminder(PluginRepository plugins) : base(plugins)
        {
            this.Rung = true;
        }

        public override string TypeName
        {
            get { return "After Pomodoro Reminder"; }
        }

        private RunningPomodoro GetPomoData()
        {
            return (RunningPomodoro)this.plugins.CherryCommands["Get Current Pomodoro Status Data"].Do(null);
        }

        public override void Schedule()
        {
            this.SchedulePreviousPomodoro();

            this.SubscribePomodoroEvents();
        }

        private void SchedulePreviousPomodoro()
        {
            var pomoData = this.GetPomoData();
            if (pomoData == null)
            {
                // pomodoros never run before, thus don't schedule anything
                return;
            }

            if (this.Started)
            {
                this.ScheduleSingleNotification(pomoData.Started);
            }

            if (this.Rung)
            {
                this.ScheduleSingleNotification(this.Now - pomoData.OutOfPomodoro);
            }
        }

        private void SubscribePomodoroEvents()
        {
            if (!this.EventsSubscribed)
            {
                this.EventsSubscribed = true;

                this.startedListener = new CherryEventListener(
                    "Pomodoro Started",
                    pea =>
                    {
                        if (this.Started)
                        {
                            this.ScheduleSingleNotification(this.Now);
                        }
                    });
                this.plugins.CherryEvents.Subscribe(this.startedListener);

                this.stoppedListener = new CherryEventListener(
                    "Pomodoro Finished",
                    pea =>
                    {
                        if (this.Rung)
                        {
                            this.ScheduleSingleNotification(this.Now);
                        }
                    });
                this.plugins.CherryEvents.Subscribe(this.stoppedListener);
            }
        }

        private void UnsubscribePomodoroEvents()
        {
            if (this.EventsSubscribed)
            {
                this.EventsSubscribed = false;

                this.plugins.CherryEvents["Pomodoro Started"].RemoveListener(this.startedListener);
                this.plugins.CherryEvents["Pomodoro Finished"].RemoveListener(this.stoppedListener);
            }
        }

        private bool ScheduleSingleNotification(DateTime countFromTime)
        {
            var triggerTime = countFromTime + this.Timeout;
            if (this.Now >= triggerTime)
            {
                // the time of action passed by. Thus do not sсhedule anything.
                return false;
            }

            this.RemoveExistingTimeTrigger(new TimeTriggerCommandArgs(this.Name + " " + this.TypeName));

            this.ScheduleSingleAction(new SingleActionCommandArgs(
                this.Name + " " + this.TypeName,
                triggerTime,
                this.Notify));

            return true;
        }

        public override bool Unschedule()
        {
            this.UnsubscribePomodoroEvents();
            return this.RemoveExistingTimeTrigger(new TimeTriggerCommandArgs(this.Name + " " + this.TypeName));
        }
    }
}
