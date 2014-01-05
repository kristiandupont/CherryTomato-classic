using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CherryTomato.Core.CommandsModel
{
    /// <summary>
    /// A data provider. Responses on every command call.
    /// </summary>
    public interface ICherryCommand
    {
        /// <summary>
        /// The unique ID of the data provider.
        /// </summary>
        string Name { get; }

        string Description { get; }

        Assembly ContainerAssembly { get; }

        /// <summary>
        /// Call this function in order to get response to the command.
        /// </summary>
        /// <param name="args">The command arguments.</param>
        /// <returns>The response data.</returns>
        object Do(CherryCommandArgs args);
    }
}
