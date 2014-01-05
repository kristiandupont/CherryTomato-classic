using System.Collections.Generic;
using System.Windows.Forms;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.WindowsController;

namespace CherryTomato.Reminders.Core.Reminders.Configuration
{
    /// <summary>
    /// Controls the reminder editor window.
    /// </summary>
    public class ReminderConfigurationController : IPlugin, ICherryCommandsProvider
    {
        private ReminderPluginsRepository reminderPlugins;
        private PluginRepository pluginsRepository;

        public ReminderConfigurationController()
        {
            this.editRemindercommand = new CherryCommand(
                "Edit Reminder",
                ca => this.EditReminder((ca as ReminderCommandArgs).Reminder),
                "Shows the reminder edition window.");
        }

        public DialogResult EditReminder(IReminder reminder)
        {
            var reminderGuiController = this.reminderPlugins.GetPlugin(reminder).GetGuiController();
            var form = new ReminderConfigurationForm(this.pluginsRepository, reminder, reminderGuiController);
            return (DialogResult)this.showDialogCommand.Do(new WindowCommandArgs(form));
        }

        public string PluginName
        {
            get { return "Reminder Configuration Editor"; }
        }

        private ICherryCommand showDialogCommand;

        public void TieEvents(PluginRepository plugins)
        {
            this.pluginsRepository = plugins;
            this.showDialogCommand = plugins.CherryCommands["Show Window"];
            plugins.CherryEvents.Subscribe(
                "Application Started",
                () =>
                    {
                        this.reminderPlugins = 
                            plugins.CherryCommands["Get All Reminder Plugins"].Do(null) as ReminderPluginsRepository;
                    });
        }

        private CherryCommand editRemindercommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.editRemindercommand;
        }
    }
}
