using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CherryTomato.Core.EventsModel
{
    public class CherryEventListener : ICherryEventListener
    {
        private Action<CherryEventArgs> callbackHandler;

        public string ListenTo { get; protected set; }

        public Assembly ContainerAssembly { get; protected set; }

        public void Handle(CherryEventArgs args)
        {
            this.callbackHandler(args);
        }

        public CherryEventListener(string listenTo, Action<CherryEventArgs> callbackHandler, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            this.Initialize(listenTo, callbackHandler, assembly);
        }

        private void Initialize(string listenTo, Action<CherryEventArgs> callbackHandler, Assembly assembly)
        {
            this.ListenTo = listenTo;
            this.callbackHandler = callbackHandler;
            this.ContainerAssembly = assembly;
        }
    }
}
