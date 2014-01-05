using System;
using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.AfterPomodoroReminder
{
    public class AfterPomodoroReminderPlugin : ReminderPlugin
    {
        protected override IReminder LoadReminderInternal(XmlElement fromElement)
        {
            return new AfterPomodoroReminder(this.pluginRepository)
            {
                Started = bool.Parse(fromElement.GetAttribute("started")),
                Rung = bool.Parse(fromElement.GetAttribute("rung")),
                Timeout = TimeSpan.Parse(fromElement.GetAttribute("timeout")),
            };
        }

        protected override void SaveReminderInternal(IReminder reminder, XmlElement reminderElement)
        {
            var apr = (AfterPomodoroReminder)reminder;

            reminderElement.SetAttribute("started", apr.Started.ToString());
            reminderElement.SetAttribute("rung", apr.Rung.ToString());
            reminderElement.SetAttribute("timeout", apr.Timeout.ToString());
        }

        protected override IReminderController CreateGuiController()
        {
            return new AfterPomodoroReminderController();
        }

        public override IReminder CreateEmptyReminder()
        {
            return new AfterPomodoroReminder();
        }

        public override IReminder CreateDefaultReminder(PluginRepository pluginRepository)
        {
            return new AfterPomodoroReminder(this.pluginRepository)
            {
                Timeout = new TimeSpan(0, 5, 0),
            };
        }
    }
}
