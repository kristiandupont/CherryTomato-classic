using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CherryTomato
{
    static class Program
    {
        private static TraceForm traceForm;

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// To enable debug settings-file, use this command-line parameter:
        /// /c:cherrytomato.settings
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string configFilePath = null;

            foreach (var arg in args)
            {
                if (arg.ToLowerInvariant().StartsWith("/c:")) configFilePath = arg.Substring(3);

                if (arg.ToLowerInvariant() == "/t")
                {
                    if (AttachConsole(ATTACH_PARENT_PROCESS))  // Try to attach to existing console. Otherwise, create the trace window.
                    {
                        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
                        Trace.WriteLine("");
                    }
                    else
                    {
                        traceForm = new TraceForm();
                        Trace.Listeners.Add(new TraceFormListener(traceForm));
                        traceForm.Show();
                    }
                }
            }

            var applicationContext = new CherryTomatoApplicationContext(configFilePath);
            Application.Run(applicationContext);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowTraceMessageBox(e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowTraceMessageBox(e.ExceptionObject as Exception);
        }

        private static void ShowTraceMessageBox(Exception ex)
        {
            var text = string.Format("Unexpected error message:\n{0}\n\nStack trace:\n{1}", ex.Message, ex.StackTrace);
            MessageBox.Show(text, "Unexpected error appeared!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
