using System;
using System.Runtime.InteropServices;
using System.Xml;
using CherryTomato.Core;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;
using CherryTomato.Core.Pomodoro;
using MessengerAPI;

namespace CherryTomato.LiveMessengerController
{
    public class LiveMessengerController : IPlugin
    {
        public bool Enabled { get; set; }

        private enum StatusCategory
        {
            Music,
            Office,
            Games
        }
        private StatusCategory InPomodoroStatusCategory { get; set; }

        private enum ImStatus
        {
            Online,
            Busy,
            Other
        }

        private ImStatus Status
        {
            get
            {
                if (msn == null) return ImStatus.Other;
                switch (msn.MyStatus)
                {
                    case MISTATUS.MISTATUS_ONLINE: return ImStatus.Online;
                    case MISTATUS.MISTATUS_BUSY: return ImStatus.Busy;
                    default: return ImStatus.Other;
                }
            }

            set
            {
                if (msn == null) return;
                switch (value)
                {
                    case ImStatus.Online: msn.MyStatus = MISTATUS.MISTATUS_ONLINE; break;
                    case ImStatus.Busy: msn.MyStatus = MISTATUS.MISTATUS_BUSY; break;
                    default: throw new NotSupportedException("Can only set user status to online or busy");
                }
            }
        }

        public string InPomodoroTextTemplate
        {
            get { return inPomodoroTextTemplate; }
            set { inPomodoroTextTemplate = value; }
        }

        private string inPomodoroTextTemplate = "In pomodoro ({0} min to go). Please don't disturb me unless it's important. Thank you.";

        public bool IsConnected
        {
            get 
            {
                if (msn == null) return false;
                try
                {
                    var x = msn.MyStatus;   // Access a var on the object. Throws if connection was lost.
                    return true;
                }
                catch (Exception)
                {
                    msn = null;
                    return false;
                }
            }
        }

        public void TryConnect()
        {
            try
            {
                if (msn == null && FindMessengerWindow() > 0)
                {
                    msn = new Messenger();
                    msn.OnAppShutdown += msn__OnAppShutdown;
                }
            }
            catch { msn = null; }
        }

        #region Integration code

        private Messenger msn;

        public LiveMessengerController()
        {
            this.Enabled = true;
            this.InPomodoroStatusCategory = StatusCategory.Music;
        }

        void msn__OnAppShutdown()
        {
            msn = null;
        }

        private const short WM_COPYDATA = 74;

        [DllImport("user32", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(int Hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32", EntryPoint = "FindWindowExA")]
        private static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);

#pragma warning disable 219
        private struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }
#pragma warning restore 219

        private static void SetMSNStatus(bool enable, string category, string message)
        {
            var handle = FindMessengerWindow();
            if (handle <= 0) return;
            
            var buffer = "\\0" + category + "\\0" + (enable ? "1" : "0") + "\\0{0}\\0" + message + "\\0\\0\\0\\0\0";
            var data = new COPYDATASTRUCT
                           {
                               dwData = 0x0547,
                               lpData = VarPtr(buffer),
                               cbData = (buffer.Length * 2)
                           };
            SendMessage(handle, WM_COPYDATA, 0, VarPtr(data));
        }

        private static int FindMessengerWindow()
        {
            var handle = 0;
            handle = FindWindowEx(0, handle, "MsnMsgrUIManager", null);
            return handle;
        }

        private static int VarPtr(object e)
        {
            var GC = GCHandle.Alloc(e, GCHandleType.Pinned);
            var gc = GC.AddrOfPinnedObject().ToInt32();
            GC.Free();
            return gc;
        }

        #endregion

        public string PluginName { get { return "Live Messenger"; } }

        public void LoadConfiguration(ConfigurePluginEventArgs cpea)
        {
            XmlElement fromElement = cpea.GetMyNode(this.PluginName);
            if (fromElement != null)
            {
                Enabled = bool.Parse(fromElement.GetAttribute("enabled"));
                InPomodoroStatusCategory = (StatusCategory)Enum.Parse(typeof(StatusCategory), fromElement.GetAttribute("InPomodoroStatusCategory"));
                var inPomodoroStatusNode = (XmlElement)fromElement.SelectSingleNode("inPomodoroStatus");
                inPomodoroTextTemplate = inPomodoroStatusNode.InnerText;
            }
        }

        public void SaveConfiguration(ConfigurePluginEventArgs cpea)
        {
            var pluginElement = cpea.CreateNewPluginNode(this.PluginName);
            pluginElement.SetAttribute("enabled", Enabled.ToString());
            pluginElement.SetAttribute("InPomodoroStatusCategory", InPomodoroStatusCategory.ToString());

            var inPomodoroStatusNode = pluginElement.OwnerDocument.CreateElement("inPomodoroStatus");
            pluginElement.AppendChild(inPomodoroStatusNode);
            inPomodoroStatusNode.InnerText = inPomodoroTextTemplate;
        }

        public void StartPomodoroInternal()
        {
            if (!Enabled) return;

            if (!IsConnected) TryConnect();
            if (IsConnected && Status == ImStatus.Online)
            {
                Status = ImStatus.Busy;
            }
        }

        public void StopPomodoroInternal(bool completed)
        {
            if (!Enabled || !IsConnected) return;

            SetMSNStatus(false, StatusCategory.Music.ToString(), "");
            Status = ImStatus.Online;
        }

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
                        new LiveMessengerSettingsPanel(this),
                        Helpers.LoadIcon("res://live.ico"));
                    (ea as GuiConfigurablePluginEventArgs).PluginInfos.Add(info);
                }));

            plugins.CherryEvents.Subscribe(
                "Application Started",
                () =>
                {
                    if (this.Enabled)
                    {
                        this.TryConnect();
                    }
                });

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
                        SetMSNStatus(
                            true,
                            this.InPomodoroStatusCategory.ToString(),
                            string.Format(this.InPomodoroTextTemplate, (cea as PomodoroEventArgs).RunnigPomodoroData.MinutesLeft));
                    }
                }));
        }
    }
}
