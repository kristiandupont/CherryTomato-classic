using System;
using System.Collections.Generic;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.TimeProvider;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.SoundNotifier;
using CherryTomato.Reminders.SystrayIconNotifier;

namespace CherryTomato.OutOfPomodoro
{
    public class OutOfPomodoroReminder : ICherryCommandsProvider, IPlugin
    {
        public bool Enabled { get; private set; }

        private CompositeNotification compositeNotification;

        public void SetTriggerAt(DateTime triggerPoint)
        {
            this.Enabled = true;

            this.scheduleActionCommand.Do(new SingleActionCommandArgs(
                "Out Of Pomodoro Remind",
                triggerPoint,
                () =>
                    {
                        if (this.Enabled)
                        {
                            this.compositeNotification.Notify();
                            this.Enabled = false;
                        }
                    }));
        }

        private CherryCommand outOfPomodoroTriggerCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.outOfPomodoroTriggerCommand;
        }

        #region IPlugin Members

        public string PluginName
        {
            get { return "Out Of Pomodoro Reminder"; }
        }

        private ICherryCommand scheduleActionCommand;
        private ICherryCommand getCurrentTimeCommand;

        private PluginRepository pluginRepository;

        public void TieEvents(PluginRepository plugins)
        {
            this.pluginRepository = plugins;

            this.scheduleActionCommand = plugins.CherryCommands["Schedule Single Action"];
            this.getCurrentTimeCommand = plugins.CherryCommands["Get Current Time"];
            plugins.CherryEvents.Subscribe("Pomodoro Finishing", () => this.Enabled = false);
            plugins.CherryEvents.Subscribe(
                "Application Started",
                () =>
                {
                    this.compositeNotification = new CompositeNotification(
                        this.pluginRepository,
                        new IconNotification
                        {
                            NotificationText = "Get back to work!",
                            FlashCount = 15,
                            FlashIconPath = "res://red.ico"
                        },
                        new SoundNotification { SoundPath = @"pomo_reminder.wav" });
                });
        }

        #endregion

        public OutOfPomodoroReminder()
        {
            this.outOfPomodoroTriggerCommand = new CherryCommand(
                "Out Of Pomodoro Trigger",
                ca =>
                {
                    var minutes = (ca as OutOfPomodoroCommandArgs).RemindAtMinutes;
                    if (minutes == 0)
                    {
                        return false;
                    }

                    // Uncomment to test reminder with 5,10,15 seconds delay. But not minutes!
                    //var triggeringTime = ((DateTime)getCurrentTimeCommand.Do(null)).AddSeconds(minutes);

                    var triggeringTime = ((DateTime)getCurrentTimeCommand.Do(null)).AddMinutes(minutes);
                    this.SetTriggerAt(triggeringTime);
                    return true;
                },
                "Sets next Out Of Pomodoro notification time.");
        }
    }
}
