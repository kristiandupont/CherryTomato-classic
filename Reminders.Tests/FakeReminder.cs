using System;
using System.Collections.Generic;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Reminders.Core.Conditions;
using CherryTomato.Reminders.Core.Notifications;
using CherryTomato.Reminders.Core.Reminders;
using CherryTomato.Reminders.SystrayIconNotifier;

namespace CherryTomato.Reminders.Tests
{
    public class FakeReminder : IReminder
    {
        public void NotifyOnPoll()
        {
        }

        public string Name { get; set; }
        public string TypeName { get { return "FakeReminder"; } }
        public string PrettyTypeName { get { return "Fake Reminder";  } }

        public bool Enabled { get { return true; } set {} }
        public bool IsGenerated { get { return false; } }

        private PluginRepository pluginRepository;

        public FakeReminder(PluginRepository pluginRepository)
        {
            this.pluginRepository = pluginRepository;
        }

        public void Schedule()
        {
        }

        public bool Unschedule()
        {
            return true;
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }


        public CompositeNotification CompositeNotification
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public CompositeCondition CompositeCondition
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}