using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.WindowsController;
using CherryTomato.SystrayIcon;
using CherryTomato.PomodoroEvaluation;

namespace CherryTomato.DebugHelper
{
    public class DebugHelper : IPlugin
    {
        private PluginRepository plugins;

        public string PluginName
        {
            get { return "DebugHelperPlugin"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
            this.plugins = plugins;
            this.plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect Menu Items",
                ea =>
                {
                    var items = (ea as IconControllerEventArgs).MenuItems;
#if DEBUG
                    items.Add(new ToolStripSeparator());
                    items.Add(new ToolStripMenuItem(
                            "[DEBUG] Fake evaluation window", 
                            null, 
                            (a, b) => this.RunFakeEvaluation()));
                    items.Add(new ToolStripMenuItem(
                        "[DEBUG] Finish Pomodoro",
                        null,
                        (a, b) => this.plugins.CherryCommands["Stop Pomodoro"].Do(null)));
                    items.Add(new ToolStripMenuItem(
                        "[DEBUG] Browse all events and commands",
                        null,
                        (a, b) => this.BrowseEventsAndCommands()));
#endif
                }));

        }

        private void BrowseEventsAndCommands()
        {
            new EventsAndCommandsBrowserForm(this.plugins).Show();
        }

        private void RunFakeEvaluation()
        {
            var mal = new List<int>();
            var kal = new List<int>();
            int max = 10;
            for (int i = 0; i < 6000; i++)
            {
                mal.Add((int)(i % max * Math.Abs(Math.Sin(i / 100)) * (i % 1000) * (i / 1000.0) / 10000.0));
                kal.Add((int)(i % max * Math.Abs(Math.Cos(i / 100)) * (i % 1000) * (i / 1000.0) / 10000.0));
            }

            CompletedPomodoro data = new CompletedPomodoro()
            {
                Start = DateTime.Now,
                Duration = TimeSpan.FromMinutes(25),
                MouseActivity = mal,
                KeyboardActivity = kal,
                TaskRegistrations = new List<TaskRegistration>()
                {
                    new TaskRegistration()
                    {
                        TaskName = "Task 0",
                        ProcessName = "Process 0",
                        TimeStamp = DateTime.Now,
                        Duration = TimeSpan.FromMinutes(1),
                    },
                    new TaskRegistration()
                    {
                        TaskName = "Task 1",
                        ProcessName = "Process 1",
                        TimeStamp = DateTime.Now + TimeSpan.FromMinutes(1),
                        Duration = TimeSpan.FromMinutes(9),
                    },
                    new TaskRegistration()
                    {
                        TaskName = "Task 2",
                        ProcessName = "Process 1",
                        TimeStamp = DateTime.Now + TimeSpan.FromMinutes(10),
                        Duration = TimeSpan.FromMinutes(5),
                    },
                    new TaskRegistration()
                    {
                        TaskName = "Task 3",
                        ProcessName = "Process 2",
                        TimeStamp = DateTime.Now + TimeSpan.FromMinutes(15),
                        Duration = TimeSpan.FromMinutes(5),
                    },
                    new TaskRegistration()
                    {
                        TaskName = "Task 4",
                        ProcessName = "Process 2",
                        TimeStamp = DateTime.Now + TimeSpan.FromMinutes(20),
                        Duration = TimeSpan.FromMinutes(5),
                    },
                }
            };

            var form = new PomodoroEvaluationForm(this.plugins);
            form.SetData(data);

            this.plugins.CherryCommands["Show Window No Activate"].Do(new WindowCommandArgs(form));
        }
    }
}
