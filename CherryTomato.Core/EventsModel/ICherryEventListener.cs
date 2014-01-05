using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CherryTomato.Core.EventsModel
{
    /// <summary>
    /// Any plugin can create as many listener objects as it need. 
    /// Each event provides data (EventArgsType) and each listened can return data to event owner (ResultType).
    /// </summary>
    public interface ICherryEventListener
    {
        /// <summary>
        /// The event name (ID) to listen.
        /// </summary>
        string ListenTo { get; }

        Assembly ContainerAssembly { get; }

        /// <summary>
        /// The callback (handler) function of the listener.
        /// </summary>
        void Handle(CherryEventArgs ea);
    }
}
