using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace CherryTomato.Core.CommandsModel
{
    public class CherryCommand : ICherryCommand
    {
        private Func<CherryCommandArgs, object> responseFunction;

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public Assembly ContainerAssembly { get; protected set; }

        public object Do(CherryCommandArgs args)
        {
            try
            {
                return this.responseFunction(args);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception on command '{0}' invoke {1}\n{2}", this.Name, ex.Message, ex.StackTrace));

                return null;
            }
        }

        public CherryCommand(string name, Func<CherryCommandArgs, object> responseFunction, string description = null)
        {
            if (description == null)
            {
                description = string.Empty;
            }

            this.Initialize(name, description, responseFunction, Assembly.GetCallingAssembly());
        }

        private void Initialize(
            string name,
            string description,
            Func<CherryCommandArgs, object> responseFunction,
            Assembly assembly)
        {
            this.Name = name;
            this.Description = description;
            this.responseFunction = responseFunction;
            this.ContainerAssembly = assembly;
        }
    }
}
