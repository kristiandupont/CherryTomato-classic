using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using CherryTomato.Core.PluginArchitecture;
using System.Reflection;

namespace CherryTomato.Core.EventsModel
{
    /// <summary>
    /// The repository of events.
    /// </summary>
    public class CherryEventsCollection
    {
        /// <summary>
        /// Holds all the events in order to have fast search by event name.
        /// </summary>
        private Dictionary<string, ICherryEvent> events;
        
        public CherryEventsCollection()
        {
            this.events = new Dictionary<string, ICherryEvent>();
        }

        /// <summary>
        /// Adds one more plugin (events provider) to the events collection.
        /// Each provider can provide as many events as it need.
        /// </summary>
        /// <param name="ep">The event provider object.</param>
        public void AddProvider(ICherryEventsProvider ep)
        {
            var plug = ep as IPlugin;
            Trace.Write(string.Format(
                "Registering events from {0}: ", 
                plug != null ? "plugin " + plug.PluginName : "one provider"));

            foreach (ICherryEvent ce in ep.GetEvents())
            {
                this.AddEvent(ce);

                Trace.Write(string.Format("'{0}' ", ce.Name));
            }

            Trace.WriteLine(string.Empty);
        }

        public void AddEvent(ICherryEvent ce)
        {
            if (this.events.ContainsKey(ce.Name))
            {
                var message = string.Format("The event named '{0}' is already registered.", ce.Name);
                throw new PluginException(message);
            }

            this.events[ce.Name] = ce;
        }

        /// <summary>
        /// Subscribes the listener to a its event.
        /// </summary>
        /// <param name="listener">
        /// The listener object which knows what to subscribe to and the action to perform.
        /// </param>
        /// <param name="throwException">Should PluginException be thrown in case there is no such event.</param>
        /// <returns>True if subscription succeeded; otherwise - false.</returns>
        public bool Subscribe(ICherryEventListener listener, bool throwException = true)
        {
            ICherryEvent ce = null;
            if (this.events.TryGetValue(listener.ListenTo, out ce))
            {
                return ce.AddListener(listener);
            }

            if (throwException)
            {
                throw new PluginException(string.Format("'{0}' event not found.", listener.ListenTo));
            }
            else
            {
                return false;
            }
        }

        public bool Subscribe(string eventName, Action action)
        {
            return this.Subscribe(new CherryEventListener(
                eventName,
                ea => action(),
                Assembly.GetCallingAssembly()));
        }

        public IEnumerable<ICherryEvent> All
        {
            get { return this.events.Values; }
        }

        public ICherryEvent this[string name]
        {
            get
            {
                return this.events[name];
            }
        }
    }
}
