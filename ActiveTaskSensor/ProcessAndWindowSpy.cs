using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.ActiveTaskSensor
{
    public class ProcessAndWindowSpy : IProcessAndWindowSpy
    {
        private int foregroundWindowHandle;

        public TaskRegistration GetActiveTask()
        {
            foregroundWindowHandle = GetForegroundWindow();

            string activeProcess = this.GetActiveProcessFilename();

            if (activeProcess.Length > 100)
                activeProcess = activeProcess.Substring(0, 100) + " ...";

            var activeTask = this.GetActiveWindowTitle();

            if (string.IsNullOrEmpty(activeTask))
                activeTask = activeProcess;

            if (activeTask.Length > 100)
                activeTask = activeTask.Substring(0, 100) + " ...";

            return new TaskRegistration
            {
                TaskName = activeTask,
                ProcessName = activeProcess,
            };
        }

        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        private string GetActiveWindowTitle()
        {
            var result = "";

            const int nChars = 256;
            var Buf = new StringBuilder(nChars);
            if (GetWindowText(foregroundWindowHandle, Buf, nChars) > 0)
                result = Buf.ToString();
            return result;
        }

        private Dictionary<int, string> windowFilenames = new Dictionary<int, string>();

        private string GetActiveProcessFilename()
        {
            if (!windowFilenames.ContainsKey(foregroundWindowHandle))
            {
                var filename = "";
                try
                {
                    int processID;
                    GetWindowThreadProcessId(new IntPtr(foregroundWindowHandle), out processID);

                    var p = Process.GetProcessById(processID);
                    filename = p.ProcessName;
                }
                catch { }
                windowFilenames[foregroundWindowHandle] = filename;
            }

            return windowFilenames[foregroundWindowHandle];
        }
    }
}
