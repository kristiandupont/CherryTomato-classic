using System;
using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.IntervalReminder
{
    public class IntervalReminderPlugin : ReminderPlugin
    {
        protected override IReminder LoadReminderInternal(XmlElement fromElement)
        {
            return new IntervalReminder(this.pluginRepository)
            {
                FromInterval = TimeSpan.Parse(fromElement.GetAttribute("fromInterval")),
                ToInterval = TimeSpan.Parse(fromElement.GetAttribute("toInterval")),
            };
        }

        protected override void SaveReminderInternal(IReminder reminder, XmlElement reminderElement)
        {
            var ir = (IntervalReminder)reminder;

            reminderElement.SetAttribute("fromInterval", ir.FromInterval.ToString());
            reminderElement.SetAttribute("toInterval", ir.ToInterval.ToString());
        }

        public override IReminder CreateEmptyReminder()
        {
            return new IntervalReminder();
        }

        public override IReminder CreateDefaultReminder(PluginRepository pluginRepository)
        {
            var reminder = new IntervalReminder(pluginRepository)
            {
                FromInterval = new TimeSpan(0, 1, 50, 0),
                ToInterval = new TimeSpan(0, 2, 10, 0),
            };

            return reminder;
        }

        protected override IReminderController CreateGuiController()
        {
            return new IntervalReminderController();
        }
    }
}
