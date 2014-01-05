using System.Collections.Generic;
using System.Windows.Forms;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Reminders;

namespace CherryTomato.Reminders.Core
{
    /// <summary>
    /// Controls the list of reminders on the CT settings 'Reminders'.
    /// </summary>
    public class RemindersPanelController : IPlugin, ICherryCommandsProvider
    {
        private PluginRepository pluginRepository;
        private ReminderPluginsRepository reminderPlugins;
        private IEnumerable<IReminder> existingReminders;
        private RemindersPanel panel;

        public RemindersPanelController()
	    {
            this.getRemindersPanelCommand = new CherryCommand(
                "Get Reminders List Panel", 
                ca => this.GetPanel(),
                "Returns Control which intended to edit reminders.");
	    }

        public Control GetPanel()
        {
            return this.panel ?? (this.panel = this.CreatePanel());
        }

        private RemindersPanel CreatePanel()
        {
            this.existingReminders = this.getAllRemindersCommand.Do(null) as IEnumerable<IReminder>;
            this.reminderPlugins = this.getAllReminderPluginsCommand.Do(null) as ReminderPluginsRepository;

            var newPanel = new RemindersPanel();

            newPanel.DeleteReminderClicked += this.DeleteReminder;
            newPanel.EditReminderClicked += this.EditReminder;

            foreach (var plugin in this.reminderPlugins.All)
            {
                newPanel.AddReminderPlugin(plugin, this.NewReminder);
            }

            foreach (var reminder in this.existingReminders)
            {
                newPanel.AddReminder(reminder);
            }

            return newPanel;
        }

        private void EditReminder(IReminder reminder)
        {
            if ((DialogResult)this.editReminderCommand.Do(new ReminderCommandArgs(reminder)) == DialogResult.OK)
            {
                this.panel.UpdateReminder(reminder);
            }
        }

        private void DeleteReminder(IReminder reminder)
        {
            if (MessageBox.Show(
                this.GetPanel().FindForm(), 
                "Are you sure you want to delete the reminder '" + reminder.Name + "'?", 
                "Delete Reminder?", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.removeReminderCommand.Do(new ReminderCommandArgs(reminder));
                this.panel.RemoveReminder(reminder);
            }
        }

        private void NewReminder(IReminderPlugin plugin)
        {
            var newReminder = plugin.CreateDefaultReminder(this.pluginRepository);
            newReminder.Name = plugin.PluginName;

            if ((DialogResult)this.editReminderCommand.Do(new ReminderCommandArgs(newReminder)) == DialogResult.OK)
            {
                this.addNewReminderCommand.Do(new ReminderCommandArgs(newReminder));

                this.panel.AddReminder(newReminder);

                newReminder.Enabled = true;
            }
        }

        public string PluginName
        {
            get { return "Reminders List Controller"; }
        }

        private ICherryCommand removeReminderCommand;
        private ICherryCommand addNewReminderCommand;
        private ICherryCommand editReminderCommand;
        private ICherryCommand getAllRemindersCommand;
        private ICherryCommand getAllReminderPluginsCommand;

        public void TieEvents(PluginRepository plugins)
        {
            this.pluginRepository = plugins;
            this.removeReminderCommand = plugins.CherryCommands["Remove Existing Reminder"];
            this.addNewReminderCommand = plugins.CherryCommands["Add New Reminder"];
            this.editReminderCommand = plugins.CherryCommands["Edit Reminder"];
            this.getAllRemindersCommand = plugins.CherryCommands["Get All Reminders"];
            this.getAllReminderPluginsCommand = plugins.CherryCommands["Get All Reminder Plugins"];
        }

        private CherryCommand getRemindersPanelCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getRemindersPanelCommand;
        }
    }
}
