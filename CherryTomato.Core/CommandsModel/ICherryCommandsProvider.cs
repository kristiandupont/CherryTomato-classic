using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CherryTomato.Core.CommandsModel
{
    /// <summary>
    /// The plugin which can provide a necessary data 
    /// (for example user pomodoros count, or current pomodoro time elapsed) 
    /// should implement the interface.
    /// </summary>
    public interface ICherryCommandsProvider
    {
        /// <summary>
        /// Retrieves the list of 'functions' (commands) which can response with data on an invoke.
        /// </summary>
        /// <returns>The list of commands which the plugin provides.</returns>
        IEnumerable<ICherryCommand> GetCommands();
    }
}
