using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CherryTomato.Core.CommandsModel;
using CherryTomato.Core.EventsModel;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.Core.WindowsController
{
    /// <summary>
    /// Controls the windows appearence. Main purpose is to have only one CT window visible.
    /// </summary>
    public class WindowsControllerPlugin : IPlugin, ICherryCommandsProvider, ICherryEventsProvider
    {
        private Stack<Form> activeWindowChain = new Stack<Form>();

        public WindowsControllerPlugin()
        {
            this.showDialogCommand = new CherryCommand(
                "Show Window",
                ca => this.ShowWindow((ca as WindowCommandArgs).Window),
                "Shows the provided Windows form using correct thread.");

            this.showWindowNoActiveCommand = new CherryCommand(
                "Show Window No Activate",
                ca => this.ShowNoActivate((ca as WindowCommandArgs).Window),
                "Shows the given Windows form on the backgound and flashes the window in Windows taskbar.");

            this.getActiveWindowCommand = new CherryCommand(
                "Get Active Window",
                ca => new WindowCommandArgs(this.GetActiveForm()),
                "Returns the current visible CherryTomato window.");

            this.setActiveWindowForegroundCommand = new CherryCommand(
                "Set Active Window Foreground",
                ca =>
                {
                    var activeForm = this.GetActiveForm();
                    if (activeForm == null)
                    {
                        return false;
                    }

                    SetForegroundWindow(activeForm.Handle.ToInt32());
                    activeForm.BringToFront();
                    activeForm.Focus();
                    return true;
                },
                "Brings the current CT window to foreground and sets focus to it.");
        }

        private Form GetActiveForm()
        {
            return this.activeWindowChain.Count == 0 ? null : this.activeWindowChain.Peek();
        }

        private void SetActiveForm(Form form)
        {
            if (this.GetActiveForm() != form)
            {
                this.activeWindowChain.Push(form);
                this.activeWindowChangedEvent.Fire(new WindowEventArgs(form));
            }
        }

        private void ResetActiveForm(Form form)
        {
            if (this.GetActiveForm() == form)
            {
                this.activeWindowChain.Pop();
                this.activeWindowChangedEvent.Fire(new WindowEventArgs(this.activeWindowChain.LastOrDefault()));
            }
            else
            {
                throw new PluginException("Internal error. Trying to reset the window which is not active.");
            }
        }

        private DialogResult ShowWindow(Form form)
        {
            DialogResult dialogResult = DialogResult.None;
            form.FormClosing += this.ResetDelegate;
            form.UpdateControl(
                () =>
                {
                    var oldActiveWindow = this.GetActiveForm();
                    this.SetActiveForm(form);
                    dialogResult = form.ShowDialog(oldActiveWindow);
                });

            return dialogResult;
        }

        public string PluginName
        {
            get { return "Windows Controller"; }
        }

        public void TieEvents(PluginRepository plugins)
        {
        }

        private CherryCommand showDialogCommand;
        private CherryCommand showWindowNoActiveCommand;
        private CherryCommand getActiveWindowCommand;
        private CherryCommand setActiveWindowForegroundCommand;

        public IEnumerable<ICherryCommand> GetCommands()
        {
            yield return this.showDialogCommand;
            yield return this.showWindowNoActiveCommand;
            yield return this.getActiveWindowCommand;
            yield return this.setActiveWindowForegroundCommand;
        }

        #region Win32 imports

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        private const int SW_SHOWNOACTIVATE = 4;
        private const int FLASHW_ALL = 3;

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public Int32 dwFlags;
            public UInt32 uCount;
            public Int32 dwTimeout;
        }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

        #endregion

        private bool ShowNoActivate(Form form)
        {
            form.UpdateControl(
                () =>
                {
                    var fw = new FLASHWINFO
                    {
                        cbSize = Convert.ToUInt32(Marshal.SizeOf(typeof(FLASHWINFO))),
                        hwnd = form.Handle,
                        dwFlags = FLASHW_ALL,
                        uCount = 2
                    };

                    this.SetActiveForm(form);
                    FlashWindowEx(ref fw);
                    ShowWindow(form.Handle, SW_SHOWNOACTIVATE);
                    form.FormClosing += this.ResetDelegate;
                });

            return true;
        }

        private void ResetDelegate(object sender, FormClosingEventArgs e)
        {
            var form = sender as Form;
            this.ResetActiveForm(form);
            form.FormClosing -= this.ResetDelegate;
        }

        private CherryEvent activeWindowChangedEvent;

        public IEnumerable<ICherryEvent> GetEvents()
        {
            yield return this.activeWindowChangedEvent = new CherryEvent(
                "Active Window Changed",
                "Fired when a CherryTomato window is shown or hidden. Brings the window pointer with event arguments object.");
        }
    }
}
