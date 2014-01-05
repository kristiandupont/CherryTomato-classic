using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core;

namespace CherryTomato.TestsCore
{
    /// <summary>
    /// <para>Easy to use class that helps to check if an event was fired (and amount of invokes) or not.</para>
    /// <para>Code Sample:</para>
    /// <code>
    /// var ps = new PomodoroSensor();
    /// var efc = new EventFiredChecker(ps);
    /// efc.WaitFor("Pomodoro Finished", "Pomodoro Finished");
    /// efc.StartPomodoro();
    /// efc.StopPomodoro(true);
    /// efc.AssertFired("Pomodoro Finished");
    /// efc.AssertFiredCount("Pomodoro Finished", 1);
    /// </code>
    /// </summary>
    public class EventFiredChecker
    {
        private Dictionary<string, CherryEventListener> listeners;
        private Dictionary<string, int> eventFired;
        private CherryService cherryService;

        public EventFiredChecker(CherryService cs)
        {
            this.cherryService = cs;
        }

        /// <summary>
        /// Subscribes to the given events to count the amount of invokes.
        /// </summary>
        /// <param name="eventNames">The events to listen to.</param>
        public void WaitFor(params string[] eventNames)
        {
            this.listeners = eventNames.ToDictionary(
                eventName => eventName,
                eventName => new CherryEventListener(
                    eventName,
                    ea => eventFired[eventName]++));

            this.eventFired = new Dictionary<string, int>();

            foreach (var listener in this.listeners.Values)
            {
                this.eventFired[listener.ListenTo] = 0;
                Assert.IsTrue(
                    this.cherryService.PluginRepository.CherryEvents.Subscribe(listener),
                    string.Format(
                        "Event named '{0}' was not found in events collection.",
                        listener.ListenTo));
            }
        }

        public object InvokeCommand(string listenerName, CherryCommandArgs args)
        {
            return this.cherryService.PluginRepository.CherryCommands[listenerName].Do(args);
        }

        /// <summary>
        /// Check if given events were fired.
        /// </summary>
        /// <param name="eventNames">The events to check.</param>
        public void AssertFired(params string[] eventNames)
        {
            foreach (var eventName in eventNames)
            {
                this.CheckEventName(eventName);
                Assert.IsTrue(
                    this.eventFired[eventName] > 0,
                    string.Format("The event '{0}' didn't fired but had to.", eventName));
            }
        }

        /// <summary>
        /// Check if given events were NOT fired.
        /// </summary>
        /// <param name="eventNames">The events to check.</param>
        public void AssertNotFired(params string[] eventNames)
        {
            foreach (var eventName in eventNames)
            {
                this.CheckEventName(eventName);
                Assert.IsTrue(
                    this.eventFired[eventName] == 0,
                    string.Format("The event '{0}' fired but hadn't to.", eventName));
            }
        }

        /// <summary>
        /// Check the event was fired that exactly number.
        /// </summary>
        /// <param name="eventName">The event to check.</param>
        /// <param name="numberOfInvokes">Number of invokes the event should have been fire.</param>
        public void AssertFiredCount(string eventName, int numberOfInvokes)
        {
            this.CheckEventName(eventName);
            Assert.AreEqual(
                numberOfInvokes,
                this.eventFired[eventName],
                string.Format(
                    "The event '{0}' had to fire {1} time(s) but fired {2} time(s).", 
                    eventName, 
                    numberOfInvokes,
                    this.eventFired[eventName]));
        }

        /// <summary>
        /// Reset all event counters to zero (non fired).
        /// </summary>
        public void Reset()
        {
            foreach (var eventName in this.eventFired.Keys.ToList())
            {
                this.eventFired[eventName] = 0;
            }
        }

        private void CheckEventName(string checkEventName)
        {
            Assert.IsTrue(
                this.eventFired.ContainsKey(checkEventName),
                string.Format(
                    "The {0} class was not expecting for event name '{1}'. " +
                    "Please use WaitFor() function to subscribe to the event.",
                    this.GetType().Name,
                    checkEventName));
        }
    }
}
