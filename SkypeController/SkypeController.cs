using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using SKYPE4COMLib;

namespace CherryTomato.SkypeController
{
    public class SkypeController : IPlugin
    {
        private readonly SkypeClass skype;

        public SkypeController()
        {
            this.skype = new SkypeClass { Timeout = 10 };
        }

        public bool Enabled
        {
            get { return this.enabled; }
            set
            {
                if (value && !this.enabled)
                {
                    this.enabled = value;
                    this.Initialize();
                }

                this.enabled = value;
            }
        }

        private bool enabled = true;

        public void Initialize()
        {
            if (this.Enabled)
            {
                this.UpdateRegex();

                this.skype._ISkypeEvents_Event_ConnectionStatus += (conStatus) =>
                {
                    this.IsConnected = conStatus == TConnectionStatus.conOnline;
                };
                this.skype._ISkypeEvents_Event_AttachmentStatus += (attachStatus) =>
                {
                    this.IsConnected = false;
                    try { this.IsConnected = this.skype.ConnectionStatus == TConnectionStatus.conOnline; }
                    catch { }
                };

                this.TryConnect();
            }
        }

        public enum ImStatus
        {
            Online,
            Busy,
            Other
        }

        public ImStatus Status
        {
            get
            {
                try
                {
                    if (!IsConnected || skype.ConnectionStatus != TConnectionStatus.conOnline)
                        return ImStatus.Other;

                    switch (skype.CurrentUserStatus)
                    {
                        case TUserStatus.cusOnline: return ImStatus.Online;
                        case TUserStatus.cusDoNotDisturb: return ImStatus.Busy;
                        default: return ImStatus.Other;
                    }
                }
                catch
                {
                    return ImStatus.Other;
                }
            }

            set
            {
                try
                {
                    if (!IsConnected || skype.ConnectionStatus != TConnectionStatus.conOnline)
                        return;

                    switch (value)
                    {
                        case ImStatus.Online: skype.CurrentUserStatus = TUserStatus.cusOnline; break;
                        case ImStatus.Busy: skype.CurrentUserStatus = TUserStatus.cusDoNotDisturb; break;
                        default: throw new NotSupportedException("Can only set user status to online or busy");
                    }
                }
                catch { }
            }
        }

        public string StatusText
        {
            get
            {
                try
                {
                    return !IsConnected || skype.ConnectionStatus != TConnectionStatus.conOnline ? "" : skype.CurrentUserProfile.MoodText;
                }
                catch { return ""; }
            }
            set
            {
                try
                {
                    if (!IsConnected || skype.ConnectionStatus != TConnectionStatus.conOnline)
                        return;
                    skype.CurrentUserProfile.MoodText = value;
                }
                catch { }
            }
        }

        public string InPomodoroTextTemplate
        {
            get { return inPomodoroTextTemplate; }
            set 
            { 
                inPomodoroTextTemplate = value;
                UpdateRegex();
            }
        }
        private string inPomodoroTextTemplate = "In pomodoro ({0} min to go). Please don't disturb me unless it's important. Thank you.";
        private string oldStatusText;

        private Regex templateRegex;

        public void UpdateRegex()
        {
            // Todo: escape template properly..
            var regexStr = string.Format(inPomodoroTextTemplate, "[0-9]+");
            templateRegex = new Regex(regexStr, RegexOptions.Compiled);
        }

        public bool MatchesTemplate(string statusText)
        {
            if(statusText == null) return false;
            if (statusText == inPomodoroTextTemplate) return true;

            //var m = templateRegex.Match(statusText);
            //return m.Captures.Count != 0;

            var ss = inPomodoroTextTemplate.Split(new string[1] { "{0}" }, StringSplitOptions.None);
            foreach (var s in ss)
            {
                if(!statusText.Contains(s)) return false;
            }
            return true;
        }

        public bool IsConnected { get; private set; }

        public void TryConnect()
        {
            IsConnected = false;
            try
            {
                var forDebugging = skype.ConnectionStatus;
                IsConnected = true;
            }
            catch (Exception e)
            {
                Trace.WriteLine("Skype connection failed. Message: " + e.Message);
            }
        }

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                this.Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
                var inPomodoroStatusNode = (XmlElement)fromElement.SelectSingleNode("inPomodoroStatus");
                inPomodoroTextTemplate = inPomodoroStatusNode.InnerText;
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);
            pluginElement.SetAttribute("enabled", this.Enabled.ToString());

            var inPomodoroStatusNode = pluginElement.OwnerDocument.CreateElement("inPomodoroStatus");
            inPomodoroStatusNode.InnerText = inPomodoroTextTemplate;
            pluginElement.AppendChild(inPomodoroStatusNode);
        }

        public void StartPomodoroInternal()
        {
            if (!this.Enabled) return;

            if (!IsConnected) TryConnect();
            if (IsConnected && Status == ImStatus.Online)
            {
                Status = ImStatus.Busy;
                oldStatusText = StatusText;

                // If for some reason the old status text contained our template, we probably didn't clean up properly...
                if (MatchesTemplate(oldStatusText))
                    oldStatusText = "";
            }
        }

        public void StopPomodoroInternal(bool completed)
        {
            if (!this.Enabled || !IsConnected) return;

            if (Status == ImStatus.Busy)
            {
                StatusText = oldStatusText;
                Status = ImStatus.Online;
            }
        }

        public string PluginName { get { return "Skype"; } }

        public void TieEvents(PluginRepository plugins)
        {
            plugins.CherryEvents.Subscribe("Pomodoro Started", this.StartPomodoroInternal);

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Finishing",
                ea => this.StopPomodoroInternal((ea as PomodoroEventArgs).PomodoroData.Successful)));

            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Collect GUI Configurable Plugins",
                ea =>
                {
                    var info = new GuiConfigurablePluginInfo(
                        this.PluginName,
                        new SkypeSettingsPanel(this),
                        Helpers.LoadIcon("res://skype.ico"));
                    (ea as GuiConfigurablePluginEventArgs).PluginInfos.Add(info);
                }));

            plugins.CherryEvents.Subscribe("Application Started", this.Initialize);
            
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Load Plugin Configuration Event",
                cpea => this.LoadConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Save Plugin Configuration Event",
                cpea => this.SaveConfiguration(cpea as ConfigurePluginEventArgs)));
            plugins.CherryEvents.Subscribe(new CherryEventListener(
                "Pomodoro Minute Elapsed",
                cea =>
                {
                    if (this.Enabled && this.IsConnected)
                    {
                        this.StatusText = string.Format(
                            this.InPomodoroTextTemplate, 
                            (cea as PomodoroEventArgs).RunnigPomodoroData.MinutesLeft);
                    }
                }));
        }
    }
}
