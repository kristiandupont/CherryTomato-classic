using System;
using System.Drawing;
using System.Windows.Forms;
using CherryTomato.Core;
using CherryTomato.Core.Pomodoro;
using CherryTomato.Core.PluginArchitecture;

namespace CherryTomato.SystrayIcon
{
    public partial class IconController
    {
        private Renderer renderer;

        public class Renderer : IDisposable
        {
            private IconController controller;

            private NotifyIcon systrayIcon;
            private Icon idleIcon;
            private Icon pomodoroIcon;
            private Icon[] pomodoroIcons;
            private string toolTipTextCache;
            private Font smallFont = new Font("Small Fonts", 6);
            private DateTime lastMessageUpdate;

            private ContextMenuStrip contextMenuStrip;

            public Renderer(IconController c)
            {
                this.controller = c;
                this.contextMenuStrip = new ContextMenuStrip
                {
                    RenderMode = ToolStripRenderMode.Professional
                };
            }

            public NotifyIcon SystrayIcon
            {
                get
                {
                    this.CheckSystrayIconCreated();
                    return this.systrayIcon;
                }
            }

            public void CheckSystrayIconCreated()
            {
                if (this.systrayIcon == null)
                {
                    this.InitializeGuiIcon();
                    this.controller.InitializeMenuItems();
                }
            }

            public ContextMenuStrip ContextMenuStrip { get { return this.contextMenuStrip; } }

            public bool ContextMenuEnabled
            {
                get { return this.SystrayIcon.ContextMenuStrip != null; }
                set { this.SystrayIcon.ContextMenuStrip = value ? this.ContextMenuStrip : null; }
            }

            public void InitializeGuiIcon()
            {
                this.pomodoroIcons = new Icon[25];

                this.idleIcon = Helpers.LoadIcon("res://off.ico");
                this.pomodoroIcon = Helpers.LoadIcon("res://pomodoro.ico");

                this.systrayIcon = new NotifyIcon
                {
                    Text = "cherrytomato - initializing...",
                    Icon = this.idleIcon,
                    Visible = true,
                    ContextMenuStrip = this.contextMenuStrip,
                };

                this.SystrayIcon.MouseClick += this.controller.notifyIcon_MouseClick;
                this.SystrayIcon.MouseMove += (a, b) => this.controller.UpdateToolTipTextMessage();
            }

            public void UpdateIconTime()
            {
                if (!this.controller.ShowTimeInTray || this.SystrayIcon == null)
                {
                    return;
                }

                var currentPomodoro = this.controller.getCurrentPomodoroCommand.Do(null) as RunningPomodoro;
                if (!currentPomodoro.IsInPomodoro)
                {
                    throw new PluginException("Icon Controller tries to draw numbers on icon when out of pomodoro.");
                }

                int minuteCount = currentPomodoro.MinutesLeft;
                if (this.pomodoroIcons[minuteCount - 1] == null)
                {
                    this.pomodoroIcons[minuteCount - 1] = this.RenderPomodoroIcon(minuteCount);
                }

                this.SetIconType(IconType.Special, this.pomodoroIcons[minuteCount - 1]);
            }

            public void SetIconType(IconType iconType, Icon specialIcon = null)
            {
                if (iconType == IconType.Special)
                {
                    this.SetIcon(specialIcon);
                }
                else if (iconType == IconType.Idle)
                {
                    this.SetIcon(this.idleIcon);
                }
                else if (iconType == IconType.Pomodoro)
                {
                    this.SetIcon(this.pomodoroIcon);
                }
                else
                {
                    throw new PluginException("Icon Controller internal error.");
                }
            }

            public void SetIcon(Icon icon)
            {
                if (this.SystrayIcon != null)
                {
                    this.SystrayIcon.Icon = icon;
                }
            }

            private Icon RenderPomodoroIcon(int minuteCount)
            {
                var b = pomodoroIcon.ToBitmap();
                var g = Graphics.FromImage(b);

                var minuteString = minuteCount.ToString();
                var size = g.MeasureString(minuteString, smallFont);
                var cx = b.Width - (int)size.Width;
                var cy = b.Height - (int)size.Height;
                g.DrawString(minuteCount.ToString(), smallFont, Brushes.Black, cx, cy);
                g.DrawString(minuteCount.ToString(), smallFont, Brushes.White, cx + 1, cy + 1);
                return Icon.FromHandle(b.GetHicon());
            }

            public bool ShowBalloonTip(BaloonState baloonData)
            {
                this.SystrayIcon.ShowBalloonTip(
                    3600000, // one hour. But it will disappear after any user activity.
                    baloonData.Caption, 
                    baloonData.Message, 
                    baloonData.Icon);

                return true;
            }

            public string ToolTipText
            {
                get
                {
                    return this.toolTipTextCache;
                }

                set
                {
                    var text = string.IsNullOrEmpty(value) ? string.Empty : value;
                    if (text.Length >= 64)
                    { // Windows has 63 symbols limit for tray icon tool tip text. Cut it down if any.
                        text = text.Substring(0, 63);
                    }

                    if (this.toolTipTextCache != text)
                    {
                        if (this.SystrayIcon != null)
                        {
                            this.SystrayIcon.Text = text;
                        }

                        this.toolTipTextCache = text;
                    }
                }
            }

            public void UpdateToolTipTextMessage()
            {
                if ((DateTime.Now - this.lastMessageUpdate).TotalSeconds < 1)
                {
                    // Do not update tooltip message too often. If harms the application to hardly.
                    return;
                }

                if (this.controller.flasher.IsFlashing)
                {
                    // Leave the flashing message untouched.
                    return;
                }

                if (this.controller.getCurrentPomodoroCommand == null)
                {
                    // The plugin is not initialized for now.
                    return;
                }

                var currentPomodoro = this.controller.getCurrentPomodoroCommand.Do(null) as RunningPomodoro;
                string toolTipText;
                if (currentPomodoro == null)
                {
                    toolTipText = string.Format("Right-click the icon and select \"Start Pomodoro\".");
                }
                else
                {
                    if (currentPomodoro.IsInPomodoro)
                    {
                        toolTipText = string.Format(InPomodoroTextTemplate, currentPomodoro.Remaining.ToAgeString(true));
                    }
                    else
                    {
                        toolTipText = string.Format(OutOfPomoTextTemplate, currentPomodoro.OutOfPomodoro.ToAgeString(false));
                    }

                    var pomodoros = this.controller.getProductivityCommand.Do(null) as PomodorosProductivity;
                    toolTipText += string.Format(ProductivityTodayTemplate, pomodoros.Rating, pomodoros.Pomodoros);
                }

                this.ToolTipText = toolTipText;

                this.lastMessageUpdate = DateTime.Now;
            }

            public void Dispose()
            {
                if (this.SystrayIcon != null)
                {
                    this.SystrayIcon.Dispose();
                }
            }
        }
    }
}
