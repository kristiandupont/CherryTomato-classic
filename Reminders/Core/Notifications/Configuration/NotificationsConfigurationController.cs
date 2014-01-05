using System.Collections.Generic;
using System.Linq;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Reminders.Core.Notifications.Configuration
{
    public class NotificationsConfigurationController : IPlugin, ICherryCommandsProvider
    {
        private NotificationsConfigurationPanel panel;

        public NotificationsConfigurationController()
        {
            this.getNotificationsConfigutationControl = new CherryCommand(
                "Get Notifications Configuration Control",
                ca => this.Panel,
                "Returns Control which edits all kind of notifications.");
            this.populateNotificationsConfigutation = new CherryCommand(
                "Populate Notifications Configuration",
                ca => this.Populate((ca as CompositeNotificationCommandArgs).CompositeNotification),
                "Update data on the UI panel with given notifications.");
            this.saveNotificationsConfigutation = new CherryCommand(
                "Save Notifications Configuration",
                ca => this.SaveToCompositeNotification((ca as CompositeNotificationCommandArgs).CompositeNotification),
                "Store all user input data to the notifications object.");
        }

        public NotificationsConfigurationPanel Panel
        {
            get
            {
                if (this.panel == null)
                {
                    this.panel = new NotificationsConfigurationPanel(
                        this.GetAllNotifiers().All.
                        Select(n => n.GetGuiController().GetSettingsPanel()));
                    this.panel.TestButtonClicked +=
                        () =>
                        {
                            var cn = new CompositeNotification(this.plugins);
                            this.SaveToCompositeNotification(cn);
                            cn.Notify();
                        };
                }

                return this.panel;
            }
        }

        private ICherryCommand getAllNotifyPluginsCommand;

        private NotifyPluginsRepository GetAllNotifiers()
        {
            return this.getAllNotifyPluginsCommand.Do(null) as NotifyPluginsRepository;
        }

        public bool SaveToCompositeNotification(CompositeNotification compositeNotification)
        {
            foreach (var notifier in this.GetAllNotifiers().All)
            {
                notifier.GetGuiController().SaveSettingsToNotification(
                    compositeNotification.GetNotification(notifier.NotificationTypeName));
            }

            return true;
        }

        public bool Populate(CompositeNotification compositeNotification)
        {
            foreach (var notifier in this.GetAllNotifiers().All)
            {
                notifier.GetGuiController().LoadSettingsFromNotification(
                    compositeNotification.GetNotification(notifier.NotificationTypeName));
            }

            return true;
        }

        public string PluginName
        {
            get { return "Notifications Configuration Controller"; }
        }

        private PluginRepository plugins;

        public void TieEvents(PluginRepository plugins)
        {
            this.plugins = plugins;
            this.getAllNotifyPluginsCommand = plugins.CherryCommands["Get All Notify Plugins"];
        }

        private CherryCommand getNotificationsConfigutationControl;
        private CherryCommand populateNotificationsConfigutation;
        private CherryCommand saveNotificationsConfigutation;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.getNotificationsConfigutationControl;
            yield return this.populateNotificationsConfigutation;
            yield return this.saveNotificationsConfigutation;
        }
    }
}
