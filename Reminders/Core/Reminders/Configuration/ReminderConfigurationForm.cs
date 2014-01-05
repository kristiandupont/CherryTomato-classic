using System;
using System.Windows.Forms;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions.Configuration;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.Core.Reminders.Configuration
{
    public partial class ReminderConfigurationForm : Form
    {
        private IReminder reminder;
        private IReminderController guiController;
        private PluginRepository pluginsRepository;

        public ReminderConfigurationForm(PluginRepository plugins, IReminder reminder, IReminderController guiController)
        {
            this.pluginsRepository = plugins;
            this.reminder = reminder;
            this.guiController = guiController;
            this.InitializeComponent();

            var reminderControl = this.guiController.GetSettingsPanel();
            reminderControl.Dock = DockStyle.Fill;
            this.pnlRemider.Controls.Add(reminderControl);

            var notificationsControl = plugins.CherryCommands["Get Notifications Configuration Control"].Do(null) as Control;
            notificationsControl.Dock = DockStyle.Fill;
            this.notificationsTabPage.Controls.Add(notificationsControl);

            var conditionssControl = plugins.CherryCommands["Get Conditions Configuration Control"].Do(null) as Control;
            conditionssControl.Dock = DockStyle.Fill;
            this.conditionsTabPage.Controls.Add(conditionssControl);

            this.nameTextBox.Text = this.reminder.Name;
            this.descriptionTextBox.Text = this.reminder.Description;
            this.guiController.LoadSettingsFromReminder(reminder);
            plugins.CherryCommands["Populate Notifications Configuration"].Do(
                new CompositeNotificationCommandArgs(this.reminder.CompositeNotification));
            plugins.CherryCommands["Populate Conditions Configuration"].Do(
                new CompositeConditionCommandArgs(this.reminder.CompositeCondition));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.reminder.Name = this.nameTextBox.Text;
            this.reminder.Description = this.descriptionTextBox.Text;
            this.guiController.SaveSettingsToReminder(this.reminder);   
            this.pluginsRepository.CherryCommands["Save Notifications Configuration"].Do(
                new CompositeNotificationCommandArgs(this.reminder.CompositeNotification));            
            this.pluginsRepository.CherryCommands["Save Conditions Configuration"].Do(
                new CompositeConditionCommandArgs(this.reminder.CompositeCondition));
        }
    }
}
