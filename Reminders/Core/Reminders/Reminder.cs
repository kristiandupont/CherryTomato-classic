using System;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Core.TimeProvider;

namespace CherryTomato.Reminders.Core.Reminders
{
    public abstract class Reminder : IReminder
    {
        private ICherryCommand getCurrentTimeCommand;
        private ICherryCommand scheduleActionCommand;
        private ICherryCommand removeExistingTimeTriggerCommand;
        protected PluginRepository plugins;

        public Reminder()
	    {
	    }

        public Reminder(PluginRepository plugins)
        {
            this.plugins = plugins;

            this.getCurrentTimeCommand = plugins.CherryCommands["Get Current Time"];
            this.scheduleActionCommand = plugins.CherryCommands["Schedule Single Action"];
            this.removeExistingTimeTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];

            this.CompositeNotification = new CompositeNotification(plugins);
            this.CompositeCondition = new CompositeCondition(plugins);

            this.LastNotificationTime = this.Now;
        }

        private bool enabled = true;
        public virtual bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled && !value)
                {
                    this.enabled = value;
                    this.Unschedule();
                }
                else if (!this.enabled && value)
                {
                    this.enabled = value;
                    this.Unschedule(); // just in case.
                    this.Schedule();
                }
            }
        }

        public DateTime LastNotificationTime { get; set; }

        public CompositeNotification CompositeNotification { get; set; }

        public CompositeCondition CompositeCondition { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public abstract string TypeName { get; }

        public abstract void Schedule();

        public abstract bool Unschedule();

        protected DateTime Now
        {
            get { return (DateTime)this.getCurrentTimeCommand.Do(null); }
        }

        public void Notify()
        {
            this.LastNotificationTime = this.Now;
            System.Diagnostics.Trace.WriteLine("Trying to invoke reminder '" + this.Name + "' at " + this.LastNotificationTime);
            if (this.CompositeCondition.IsTrue)
            {
                System.Diagnostics.Trace.WriteLine("Conditions allow notification. Notify.");
                this.CompositeNotification.Notify();
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("Conditions do not allow notifications. Keep silence.");
            }
        }

        protected void ScheduleSingleAction(SingleActionCommandArgs saca)
        {
            this.scheduleActionCommand.Do(saca);
        }

        protected bool RemoveExistingTimeTrigger(TimeTriggerCommandArgs ttca)
        {
            return (bool)this.removeExistingTimeTriggerCommand.Do(ttca);
        }
    }
}
