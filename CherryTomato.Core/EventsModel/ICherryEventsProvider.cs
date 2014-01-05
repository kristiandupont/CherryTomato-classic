using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Core.EventsModel
{
    /// <summary>
    /// The plugin which produces some events (for example user click, or pomodoro finish) 
    /// should implement the interface.
    /// </summary>
    public interface ICherryEventsProvider
    {
        /// <summary>
        /// Gets the list of events the plugin provides.
        /// </summary>
        /// <returns>The list of events which the plugin provides.</returns>
        IEnumerable<ICherryEvent> GetEvents();
    }
}
