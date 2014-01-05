using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace CherryTomato.Core.EventsModel
{
    public class CherryEvent : ICherryEvent
    {
        private HashSet<ICherryEventListener> listeners =
            new HashSet<ICherryEventListener>();

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Assembly ContainerAssembly { get; protected set; }

        public IEnumerable<ICherryEventListener> Listeners { get { return this.listeners; } }

        public CherryEvent(string name, string description = null)
        {
            if (description == null)
            {
                description = string.Empty;
            }

            this.Initialize(name, description, Assembly.GetCallingAssembly());
        }

        public bool AddListener(ICherryEventListener listener)
        {
            // will return false if such listener object already exist.
            return this.listeners.Add(listener);
        }

        public void Fire(CherryEventArgs eventArgs)
        {
            try
            {
                Trace.WriteLine(string.Format("Firing event: '{0}'", this.Name));

                // We are cloning the listeners collection because it can be changed by one of the listeners.
                foreach (var listener in this.listeners.ToList())
                {
                    Trace.WriteLine(string.Format("Invoking event listener from assembly {0}", listener.ContainerAssembly));
                    listener.Handle(eventArgs);
                }

                Trace.WriteLine(string.Format("Event fired: '{0}'", this.Name));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception on event '{0}' fire {1}\n{2}", this.Name, ex.Message, ex.StackTrace));
            }
        }

        public bool RemoveListener(CherryEventListener listener)
        {
            return this.listeners.Remove(listener);
        }

        private void Initialize(string name, string description, Assembly assembly)
        {
            this.Name = name;
            this.Description = description;
            this.ContainerAssembly = assembly;
        }
    }
}
