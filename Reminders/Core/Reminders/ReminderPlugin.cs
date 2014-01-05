using System.Xml;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders.Configuration;

namespace CherryTomato.Reminders.Core.Reminders
{
    public abstract class ReminderPlugin : IReminderPlugin
    {
        protected PluginRepository pluginRepository;

        public IReminder LoadReminder(XmlElement fromElement)
        {
            var plugName = fromElement.GetAttribute("typeName");
            if (plugName != this.PluginName)
            {
                throw new PluginException("typeName attribute is not " + this.PluginName);
            }

            IReminder result = this.LoadReminderInternal(fromElement);
            result.Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
            result.Name = fromElement.GetAttribute("name");
            result.Description = ((XmlElement)fromElement.SelectSingleNode("description")).InnerText;

            var compositeNotificationNode = (XmlElement)fromElement.SelectSingleNode("compositeNotification");
            result.CompositeNotification.Load(compositeNotificationNode);

            var compositeConditionNode = (XmlElement)fromElement.SelectSingleNode("compositeCondition");
            result.CompositeCondition.Load(compositeConditionNode);

            return result;
        }

        protected abstract IReminder LoadReminderInternal(XmlElement fromElement);

        public void SaveReminder(IReminder reminder, XmlElement parentElement)
        {
            var reminderElement = parentElement.OwnerDocument.CreateElement("reminder");

            parentElement.AppendChild(reminderElement);

            reminderElement.SetAttribute("enabled", reminder.Enabled.ToString());
            reminderElement.SetAttribute("typeName", reminder.TypeName);
            reminderElement.SetAttribute("name", reminder.Name);
            var descriptionElement = reminderElement.OwnerDocument.CreateElement("description");
            descriptionElement.InnerText = reminder.Description;
            reminderElement.AppendChild(descriptionElement);

            this.SaveReminderInternal(reminder, reminderElement);

            reminder.CompositeNotification.Save(reminderElement);
            reminder.CompositeCondition.Save(reminderElement);
        }

        protected abstract void SaveReminderInternal(IReminder reminder, XmlElement reminderElement);

        private IReminderController guiController;

        public IReminderController GetGuiController()
        {
            return this.guiController ?? (this.guiController = this.CreateGuiController());
        }

        protected abstract IReminderController CreateGuiController();

        public abstract IReminder CreateDefaultReminder(PluginRepository pluginRepository);

        public abstract IReminder CreateEmptyReminder();

        public string PluginName
        {
            get { return this.CreateEmptyReminder().TypeName; }
        }

        public virtual void TieEvents(PluginRepository plugins)
        {
            this.pluginRepository = plugins;
        }
    }
}
