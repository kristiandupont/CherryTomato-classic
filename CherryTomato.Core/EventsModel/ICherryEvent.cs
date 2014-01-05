using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CherryTomato.Core.EventsModel
{
    /// <summary>
    /// The vast important part of Cherry event system.
    /// Each plugin can implement as many event objects as it need.
    /// </summary>
    public interface ICherryEvent
    {
        /// <summary>
        /// The event unique ID.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// This property is for debugging only. Should return description of the event.
        /// When it fired, why it fired, what the event arguments, etc.
        /// </summary>
        string Description { get; }

        Assembly ContainerAssembly { get; }

        /// <summary>
        /// Adds one more listener to the event object. Makes all necessary verification of provided parameter.
        /// </summary>
        /// <param name="listener">The listener object to add.</param>
        /// <returns>True in case the object was added; otherwise - false.</returns>
        bool AddListener(ICherryEventListener listener);

        /// <summary>
        /// Invokes all the listeners using the event arguments provided.
        /// </summary>
        /// <param name="eventArgs">The object to send.</param>
        void Fire(CherryEventArgs eventArgs);

        IEnumerable<ICherryEventListener> Listeners { get; }

        bool RemoveListener(CherryEventListener cherryEventListener);
    }
}
