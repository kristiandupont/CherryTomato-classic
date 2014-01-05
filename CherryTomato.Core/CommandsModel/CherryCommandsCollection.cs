using System.Collections.Generic;
using System.Diagnostics;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.CommandsModel
{
    /// <summary>
    /// The repository of data (response) providers.
    /// </summary>
    public class CherryCommandsCollection
    {
        /// <summary>
        /// Holds all the commands in order to have fast search by its name.
        /// </summary>
        private Dictionary<string, ICherryCommand> commands;

        public CherryCommandsCollection()
        {
            this.commands = new Dictionary<string, ICherryCommand>();
        }

        /// <summary>
        /// Add one more plugin (commands provider) to the listeners collection.
        /// Each provider can provide as many commands as it need.
        /// </summary>
        /// <param name="cp">Commands provider object.</param>
        public void AddProvider(ICherryCommandsProvider cp)
        {
            var plug = cp as IPlugin;
            Trace.Write(string.Format(
                "Registering command listeners from {0}: ",
                plug != null ? "plugin " + plug.PluginName : "one provider"));

            foreach (var cc in cp.GetCommands())
            {
                this.AddCommand(cc);

                Trace.Write(string.Format("'{0}' ", cc.Name));
            }

            Trace.WriteLine(string.Empty);
        }

        public void AddCommand(ICherryCommand cc)
        {
            if (this.commands.ContainsKey(cc.Name))
            {
                var message = string.Format("The command listener named '{0}' is already registered.", cc.Name);
                throw new PluginException(message);
            }

            this.commands[cc.Name] = cc;
        }

        /// <summary>
        /// Retrieves a command from the collection.
        /// </summary>
        /// <param name="commandName">The command name to search </param>
        /// <param name="throwException">Throw exception in case the command is absent?</param>
        /// <returns>The command object; or null it was not found.</returns>
        public ICherryCommand this[string commandName, bool throwException = true]
        {
            get
            {
                ICherryCommand rl = null;
                if (this.commands.TryGetValue(commandName, out rl))
                {
                    return rl;
                }

                if (throwException)
                {
                    throw new PluginException(string.Format("'{0}' command not found.", commandName));
                }

                return null;
            }
        }

        public IEnumerable<ICherryCommand> All
        {
            get { return this.commands.Values; }
        }
    }
}
