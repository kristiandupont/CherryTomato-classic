using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace CherryTomato.Reminders.ShakeNotifier
{
    public class WindowShaker
    {
        private ShakeState shakeSettings;

        private IntPtr windowPtr;
        private RECT windowRect = new RECT();
        private DateTime endtime;
        private Random random = new Random();

        private const int amplitude = 15;

        private double cx, cy;
        private double velX = 1.1;
        private double velY = 1.0; 

        public WindowShaker(ShakeState shakeSettings)
        {
            this.shakeSettings = shakeSettings;
        }

        public void Shake()
        {
            this.windowPtr = GetForegroundWindow();
            GetWindowRect(this.windowPtr, ref this.windowRect);
            this.endtime = DateTime.Now + TimeSpan.FromMilliseconds(this.shakeSettings.Timeout);
            new Thread(this.ShakeThread).Start();
        }

        private void ShakeThread()
        {
            var now = DateTime.Now;
            var a = now.Ticks;
            var c = (now + TimeSpan.FromMilliseconds(this.shakeSettings.Timeout / 2)).Ticks;

            while (DateTime.Now < endtime)
            {
                var smooth = 1.0 - Math.Abs((double)(DateTime.Now.Ticks - c) / (double)(c - a));
                smooth = smooth * smooth;
                
                var x = Math.Sin(cx) * amplitude * smooth;
                cx += velX * smooth;

                var y = Math.Cos(cy) * amplitude * smooth;
                cy += velY * smooth;

                Move((int)x, (int)y);
                Thread.Sleep(25);
            }

            this.Move(0, 0);
        }

        internal struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private void Move(int shiftX, int shiftY)
        {
            MoveWindow(
                this.windowPtr,
                this.windowRect.left + shiftX,
                this.windowRect.top + shiftY,
                this.windowRect.right - this.windowRect.left, 
                this.windowRect.bottom - this.windowRect.top, 
                true);
        }
    }

    public class WindowShakerTest
    {
        public void Test()
        {
            var shaker = new WindowShaker(new ShakeState { Timeout = 2000 });

            Debug.WriteLine("Shaking...");
            shaker.Shake();
            System.Threading.Thread.Sleep(3000);
            Debug.WriteLine("Done!");
        }
    }
}
