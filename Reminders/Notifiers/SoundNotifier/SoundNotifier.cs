using System.Xml;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.SoundController;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Notifications.Configuration;

namespace CherryTomato.Reminders.SoundNotifier
{
    public class SoundNotifier : Notifier
    {
        private ICherryCommand playSound;

        public override void Notify(INotification notification)
        {
            var n = (SoundNotification)notification;
            this.playSound.Do(new PlaySoundCommandArgs(n.SoundPath));
        }

        public override void TieEvents(PluginRepository plugins)
        {
            this.playSound = plugins.CherryCommands["Play Sound"];
        }

        protected override INotificationController CreateGuiControllerInternal()
        {
            return new SoundNotificationGuiController();
        }

        public override INotification CreateNotification()
        {
            return new SoundNotification();
        }

        public override void SaveNotificationToXml(INotification notification, XmlElement notificationElement)
        {
            var n = (SoundNotification)notification;
            notificationElement.SetAttribute("soundPath", n.SoundPath);
        }

        public override void LoadNotificationFromXml(INotification notification, XmlElement notificationElement)
        {
            var n = (SoundNotification)notification;
            n.SoundPath = notificationElement.GetAttribute("soundPath");
        }
    }
}
