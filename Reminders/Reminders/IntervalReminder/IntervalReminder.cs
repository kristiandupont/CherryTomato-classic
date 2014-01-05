using System;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.TimeProvider;
using CherryTomato.Reminders.Core.Reminders;

namespace CherryTomato.Reminders.IntervalReminder
{
    public class IntervalReminder : Reminder
    {
        public override string TypeName { get { return "Interval Reminder"; } }

        public TimeSpan FromInterval { get; set; }
        public TimeSpan ToInterval { get; set; }

        public IntervalReminder()
        {
        }

        public IntervalReminder(PluginRepository plugins)
            : base(plugins)
        {
        }

        private bool scheduled;

        /// <summary>
        /// Time must be in UTC format only!
        /// </summary>
        /// <returns>The Quartz trigger.</returns>
        public override void Schedule()
        {
            if (!this.scheduled)
            {
                this.scheduled = true;
                this.ScheduleSingleAction(new SingleActionCommandArgs(
                    this.Name + " " + this.TypeName,
                    this.GetNextFiringTime(),
                    () =>
                    {
                        this.scheduled = false;
                        this.Notify();
                        this.Schedule();
                    }));
            }
        }

        public override bool Unschedule()
        {
            this.scheduled = false;
            var args = new TimeTriggerCommandArgs(this.Name + " " + this.TypeName);
            return this.RemoveExistingTimeTrigger(args);
        }

        private Random random = new Random();

        private DateTime GetNextFiringTime()
        {
            var randomSeconds = (this.ToInterval - this.FromInterval).TotalSeconds * random.NextDouble();
            var randomTime = TimeSpan.FromSeconds(randomSeconds);
            return this.LastNotificationTime + this.FromInterval + randomTime;
        }
    }
}
