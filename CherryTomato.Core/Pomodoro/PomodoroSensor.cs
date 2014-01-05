using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.Database;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.TimeProvider;
using Quartz;

namespace CherryTomato.Core.Pomodoro
{
    public class PomodoroSensor : IPlugin, ICherryEventsProvider, ICherryCommandsProvider, IDisposable
    {
        private DatabaseConnection databaseConnection;
        private DateTime pluginStarted;

        public TimeSpan PomodoroTimeSpan { get; private set; }
        public readonly TimeSpan defaultPomodoroTimeSpan = new TimeSpan(0, 0, 25, 0);

        public PomodoroSensor()
        {
            this.PomodoroTimeSpan = defaultPomodoroTimeSpan;
            this.TodayProductivity = new PomodorosProductivity();

            this.startPomodoroCommand = new CherryCommand(
                "Start Pomodoro",
                cra => { this.StartPomodoroInternal(); return true; },
                "Starts the pomodoro. The corresponding event is being fired.");

            this.voidPomodoroCommand = new CherryCommand(
                "Void Pomodoro",
                cra => { this.StopPomodoroInternal(false); return true; },
                "Void (cancel) the currently running pomodoro.");

            this.stopPomodoroCommand = new CherryCommand(
                "Stop Pomodoro",
                cra => { this.StopPomodoroInternal(true); return true; },
                "Finishes (successfully) the pomodoro. The corresponding event is being fired.");

            this.getTodayProductivityCommand = new CherryCommand(
                "Get Today Productivity",
                cra => this.TodayProductivity,
                "Returns instance of PomodorosProductivity class which brings information about today productivity.");

            this.getProductivityCommand = new CherryCommand(
                "Get Productivity",
                cra => this.GetProductivity(cra as ProductivityCommandArgs),
                "Returns instance of PomodorosProductivity class which brings information about productivity within requested time.");

            this.isInPomodoroCommand = new CherryCommand(
                "Is In Pomodoro",
                cra => this.IsInPomodoro,
                "Shortcut for 'Get Current Pomodoro Status Data' command. Returns bool indication if currently system is in pomodoro.");

            this.getRunningPomodoroDataCommand = new CherryCommand(
                "Get Current Pomodoro Status Data",
                cra => this.GetCurrentStatusData(),
                "Returns instance of RunningPomodoro class which brings information about running or last finished pomodoro. Returns null if no pomodoros were ever run.");

            this.approvePomodoroCommand = new CherryCommand(
                "Approve Pomodoro",
                pca => this.RegisterPomodoro((pca as PomodoroCommandArgs).PomodoroData),
                "Add one more pomodoro record to the DB.");
        }

        private bool IsInPomodoro { get { return this.pomodoroData != null; } }

        public string PluginName { get { return "Pomodoro Sensor"; } }

        public bool CurrentlyInPomodoro { get { return pomodoroData != null; } }

        public void ConnectToDatabase(DatabaseConnection newDatabaseConnection)
        {
            this.pluginStarted = this.Now;
            this.databaseConnection = newDatabaseConnection;

            if (!this.databaseConnection.TableExists("PomodoroRegistrations"))
            {
                this.databaseConnection.ExecuteNonQuery(
                    "create table PomodoroRegistrations (TimeStamp datetime not null, Duration integer not null, Evaluation int not null);");
            }

            this.ReadAllPomodoros();

            var todayStart = this.Now.Date;
            var todayPomodoros = this.allRegisteredPomodoros.Where(p => p.Start >= todayStart);
            this.TodayProductivity = new PomodorosProductivity(todayPomodoros);

            this.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                "Noon Crossed",
                TriggerUtils.MakeDailyTrigger(0, 0), // fired on every noon
                () => this.SetPomodorosToday(new PomodorosProductivity()))); // reset pomodoro counts for today
        }

        private void ReadAllPomodoros()
        {
            this.allRegisteredPomodoros.Clear();
            var todaysPomodoros = this.databaseConnection.ExecuteReader("select * from PomodoroRegistrations");
            foreach (DataRow r in todaysPomodoros.Rows)
            {
                var p = new PomodoroRegistration()
                {
                    Start = Convert.ToDateTime(r["TimeStamp"]),
                    Duration = TimeSpan.FromTicks(Convert.ToInt64(r["Duration"])),
                    Rating = Convert.ToInt32(r["Evaluation"]),
                };

                this.allRegisteredPomodoros.Add(p);
            }
        }

        private CompletedPomodoro pomodoroData;
        private CompletedPomodoro previousPomodoro;
        private List<PomodoroRegistration> allRegisteredPomodoros = new List<PomodoroRegistration>();

        public PomodorosProductivity TodayProductivity { get; private set; }

        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;
        private ICherryCommand scheduleActionCommand;
        private ICherryCommand removeExistingTimeTriggerCommand;

        private ICherryCommand now;
        private DateTime Now
        {
            get
            {
                return (DateTime)this.now.Do(null);
            }
        }

        public RunningPomodoro GetCurrentStatusData()
        {
            var pomo = new RunningPomodoro();
            pomo.ScheduledPomodoroDuration = this.PomodoroTimeSpan;

            if (this.CurrentlyInPomodoro)
            {
                pomo.Started = this.pomodoroData.Start;
                pomo.Elapsed = this.Now - this.pomodoroData.Start;
                pomo.OutOfPomodoro = this.Now - (previousPomodoro == null ? this.pluginStarted : this.previousPomodoro.End);
                pomo.IsInPomodoro = true;
            }
            else
            {
                if (this.previousPomodoro != null)
                {
                    pomo.Started = this.previousPomodoro.Start;
                    pomo.Elapsed = this.Now - this.previousPomodoro.Start;
                    pomo.OutOfPomodoro = this.Now - this.previousPomodoro.End;
                }
                else
                {
                    pomo = null;
                }
            }

            return pomo;
        }

        public void StartPomodoroInternal()
        {
            this.pomodoroData = new CompletedPomodoro();
            this.pomodoroData.Start = this.Now;

            this.scheduleActionCommand.Do(new SingleActionCommandArgs(
                "Finish Pomodoro",
                this.Now.Add(this.PomodoroTimeSpan),
                () => this.StopPomodoroInternal(true)));

            this.pomodoroStartedEvent.Fire(new PomodoroEventArgs(this.pomodoroData));

            this.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                "Pomodoro Minute Elapsed",
                TriggerUtils.MakeMinutelyTrigger(), // fired on every minute
                () => this.pomodoroMinuteElapsed.Fire(new PomodoroEventArgs(this.GetCurrentStatusData()))));
        }

        public void StopPomodoroInternal(bool completed)
        {
            this.previousPomodoro = this.pomodoroData;
            this.pomodoroData = null;

            var args = new TimeTriggerCommandArgs("Finish Pomodoro");
            this.removeExistingTimeTriggerCommand.Do(args); // should be removed in case the user voided the pomodoro

            // remove the trigger as of no need
            this.removeExistingTriggerCommand.Do(new TimeTriggerCommandArgs("Pomodoro Minute Elapsed"));

            this.previousPomodoro.Successful = completed;

            this.previousPomodoro.Duration = this.Now - this.previousPomodoro.Start;

            this.pomodoroFinishingEvent.Fire(new PomodoroEventArgs(this.previousPomodoro));

            this.pomodoroFinishedEvent.Fire(new PomodoroEventArgs(this.previousPomodoro));
        }

        public bool RegisterPomodoro(CompletedPomodoro pomodoro)
        {
            this.SetPomodorosToday(new PomodorosProductivity(
                this.TodayProductivity.Pomodoros + 1,
                this.TodayProductivity.Rating + pomodoro.Rating));

            this.allRegisteredPomodoros.Add(pomodoro);

            if (this.databaseConnection != null)
            {
                this.databaseConnection.ExecuteNonQuery(
                    "insert into PomodoroRegistrations (TimeStamp, Duration, Evaluation) values (@p1, @p2, @p3);",
                    pomodoro.Start,
                    pomodoro.Duration.Ticks,
                    pomodoro.Rating);

                return true;
            }

            return false;
        }

        private PomodorosProductivity GetProductivity(ProductivityCommandArgs productivityCommandArgs)
        {
            var sinceTime = productivityCommandArgs.SinceTime;
            bool allowPartialPomodoros = !productivityCommandArgs.CountPartialPomodoros;
            var acceptablePomodoros = this.allRegisteredPomodoros.Where(p => sinceTime <= (allowPartialPomodoros ? p.End : p.Start));
            return new PomodorosProductivity(acceptablePomodoros);
        }

        private void SetPomodorosToday(PomodorosProductivity todayProductivity)
        {
            this.TodayProductivity = todayProductivity;
            this.numberOfPomodorosChangedEvent.Fire(new PomodoroEventArgs(this.TodayProductivity));
        }

        private CherryEvent pomodoroStartedEvent = new CherryEvent(
            "Pomodoro Started",
            "Fired just after the pomodoro start. Brings pomodoro data with event arguments.");
        private CherryEvent pomodoroFinishingEvent = new CherryEvent(
            "Pomodoro Finishing",
            "This event should be used in order to add necessary data to pomodoro object.");
        private CherryEvent pomodoroFinishedEvent = new CherryEvent(
            "Pomodoro Finished",
            "This event should be used in order to get all the data from pomodoro object.");
        private CherryEvent numberOfPomodorosChangedEvent = new CherryEvent(
            "Number of Pomodoro Changed",
            "Fired when number of pomodoros increased. Brings today productivity data with event arguments.");
        private CherryEvent pomodoroMinuteElapsed = new CherryEvent(
            "Pomodoro Minute Elapsed",
            "Fired each minute of pomodoro. This means it will be fired 26 (twenty six) times.");
        private CherryCommand getTodayProductivityCommand;
        private CherryCommand getProductivityCommand;
        private CherryCommand startPomodoroCommand;
        private CherryCommand voidPomodoroCommand;
        private CherryCommand isInPomodoroCommand;
        private CherryCommand getRunningPomodoroDataCommand;
        private CherryCommand approvePomodoroCommand;
        
        /// <summary>
        /// TODO: This command should be removed! The sensor should stop pomodoro itself! but not by external command.
        /// </summary>
        private CherryCommand stopPomodoroCommand;

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.pomodoroStartedEvent;
            yield return this.pomodoroFinishingEvent;
            yield return this.pomodoroFinishedEvent;
            yield return this.numberOfPomodorosChangedEvent;
            yield return this.pomodoroMinuteElapsed;
        }

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.startPomodoroCommand;
            yield return this.voidPomodoroCommand;
            yield return this.stopPomodoroCommand;
            yield return this.getTodayProductivityCommand;
            yield return this.getProductivityCommand;
            yield return this.isInPomodoroCommand;
            yield return this.getRunningPomodoroDataCommand;
            yield return this.approvePomodoroCommand;
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.now = plugins.CherryCommands["Get Current Time"];

            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];
            this.scheduleActionCommand = plugins.CherryCommands["Schedule Single Action"];
            this.removeExistingTimeTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Configuration Event",
                cea => this.LoadConfig(cea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Configuration Event",
                cea => this.SaveConfig(cea as ConfigureEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Connect To Database Event",
                ea => this.ConnectToDatabase((ea as ConnectToDbEventArgs).DbConnection)));
        }

        private void SaveConfig(ConfigureEventArgs cea)
        {
            if (this.PomodoroTimeSpan != defaultPomodoroTimeSpan)
            {
                var configNode = cea.ConfigNode;
                var pomodoroNode = cea.Document.CreateElement("pomodoro");
                pomodoroNode.SetAttribute("timeSpan", this.PomodoroTimeSpan.ToString());
                configNode.AppendChild(pomodoroNode);
            }
        }

        private void LoadConfig(ConfigureEventArgs cea)
        {
            if (cea.ConfigNode != null)
            {
                var pomodoroNode = cea.ConfigNode.SelectSingleNode("pomodoro") as XmlElement;
                if (pomodoroNode != null)
                {
                    var timeSpanString = pomodoroNode.GetAttribute("timeSpan");
                    this.PomodoroTimeSpan = TimeSpan.Parse(timeSpanString);
                }
            }
        }

        public void Dispose()
        {
            if (this.IsInPomodoro)
            {
                this.StopPomodoroInternal(false);
            }
        }
    }
}
