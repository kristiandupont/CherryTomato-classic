using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.TimeProvider;
using Quartz;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.UserActivity
{
    public class FlowSensor : IPlugin, ICherryCommandsProvider, IDisposable
    {
        public bool Enabled { get; set; }

        private bool inPomodoro = false;

        private IUserActivityHook userActivityHook;
        //private DatabaseConnection databaseConnection;

        private double mouseCounter;
        private double keyboardCounter;

        private void uah_OnMouseActivity(object sender, MouseEventArgs e)
        {
            // Mouse movement doesn't count for much (there are so many of them). Button presses do.
            mouseCounter += e.Button == MouseButtons.None ? 0.1 : 1.0; 
        }

        private void uah_KeyDown(object sender, KeyEventArgs e) { keyboardCounter += 1.0; }

        public FlowSensor() : this(new UserActivityHook()) { }
        public FlowSensor(IUserActivityHook userActivityHook)
        {
            this.Enabled = true;

            this.userActivityHook = userActivityHook;
            this.isUserActivityPluginEnabled = new CherryCommand(
                "Is User Activity Sensor Enabled", 
                ca => this.Enabled,
                "Returns bool indicating of the UserActivity plugins is enabled/disabled.");

            this.userActivityHook.KeyDown += this.uah_KeyDown;
            this.userActivityHook.OnMouseActivity += this.uah_OnMouseActivity;
        }

        public void CountActivity()
        {
            if (!this.Enabled || !this.inPomodoro)
            {
                return;
            }

            PomodoroKeyboardActivity.Add((int)keyboardCounter);
            keyboardCounter = 0;

            PomodoroMouseActivity.Add((int)mouseCounter);
            mouseCounter = 0;
        }

        public List<int> PomodoroKeyboardActivity = new List<int>();
        public List<int> PomodoroMouseActivity = new List<int>();

        public void StartPomodoroInternal()
        {
            if (!this.Enabled) return;

            this.inPomodoro = true;

            this.PomodoroKeyboardActivity = new List<int>();
            this.PomodoroMouseActivity = new List<int>();

            this.userActivityHook.Start();

            this.addNewTriggerCommand.Do(new TimeTriggerCommandArgs(
                "Second Elapsed for Flow Sensor",
                TriggerUtils.MakeSecondlyTrigger(), // fired on every second
                this.CountActivity)); // add the activity counters to arrays and reset the counters
        }

        public void StopPomodoroInternal(CompletedPomodoro pomodoroData)
        {
            if (!this.Enabled) return;

            this.inPomodoro = false;

            this.userActivityHook.Stop();

            pomodoroData.KeyboardActivity = this.PomodoroKeyboardActivity;
            pomodoroData.MouseActivity = this.PomodoroMouseActivity;

            // remove the trigger as of no need
            this.removeExistingTriggerCommand.Do(new TimeTriggerCommandArgs("Second Elapsed for Flow Sensor"));
        }

        public string PluginName
        {
            get { return "User Activity"; }
        }

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                this.Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);
            pluginElement.SetAttribute("enabled", this.Enabled.ToString());
        }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData)));

            this.addNewTriggerCommand = plugins.CherryCommands["Add New Time Trigger"];
            this.removeExistingTriggerCommand = plugins.CherryCommands["Remove Existing Time Trigger"];

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Plugin Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Plugin Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigurePluginEventArgs)));
        }

        private ICherryCommand addNewTriggerCommand;
        private ICherryCommand removeExistingTriggerCommand;

        private CherryCommand isUserActivityPluginEnabled;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.isUserActivityPluginEnabled;
        }

        public void Dispose()
        {
            this.userActivityHook.Stop();
        }
    }
}
