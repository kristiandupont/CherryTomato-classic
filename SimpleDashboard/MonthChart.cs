using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.SimpleDashboard
{
    public partial class MonthChart : UserControl
    {
        public enum ViewModeEnum
        {
            Productivity,
            Pomodoros,
            PPerP
        };

        public ViewModeEnum ViewMode
        {
            get { return viewMode; }
            set
            {
                viewMode = value;
                Invalidate();
            }
        }
        private ViewModeEnum viewMode;

        public Dictionary<DateTime, PomodorosProductivity> productivityIndices = new Dictionary<DateTime, PomodorosProductivity>();
        private ToolTip toolTip = new ToolTip();

        public MonthChart()
        {
            InitializeComponent();
            toolTip.Active = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var sourceDate = DateTime.Now;

            var padding = 2;

            CalendarRectangles.Clear();

            var thisMonth = new DateTime(sourceDate.Year, sourceDate.Month, 1);
            var thisMonthRect = new Rectangle(Size.Width / 2 + padding * 2, 0, Size.Width / 2 - padding, Size.Height);
            RenderMonth(thisMonth, e.Graphics, thisMonthRect);

            var lastMonth = thisMonth.AddMonths(-1);
            var lastMonthRect = new Rectangle(0, 0, Size.Width / 2 - padding, Size.Height);
            RenderMonth(lastMonth, e.Graphics, lastMonthRect);
        }

        private Dictionary<Rectangle, DateTime> CalendarRectangles = new Dictionary<Rectangle, DateTime>();

        private void RenderMonth(DateTime month, Graphics graphics, Rectangle rect)
        {
            var dayWidth = rect.Width / 7;
            var dayHeight = rect.Height / 6;

		    var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            var day = month;
            int daysAhead = (int)day.DayOfWeek - (int)firstDayOfWeek;
            if (daysAhead < 0) daysAhead += 7;

            day -= TimeSpan.FromDays(daysAhead);

            for (var week = 0; week != 6; ++week)
            {
                for (var weekDay = 0; weekDay != 7; ++weekDay)
                {
                    if (day.Month == month.Month && day <= DateTime.Today)
                    {
                        var value = 0;
                        if (productivityIndices.ContainsKey(day))
                        {
                            if (viewMode == ViewModeEnum.Productivity)
                                value = productivityIndices[day].Rating;
                            else if (viewMode == ViewModeEnum.Pomodoros)
                                value = productivityIndices[day].Pomodoros * 8;
                            else if (viewMode == ViewModeEnum.PPerP && productivityIndices[day].Pomodoros > 0)
                                value = (productivityIndices[day].Rating / productivityIndices[day].Pomodoros) * 8;
                        }

                        var color = CalculateColor(value);
                        var brush = new SolidBrush(color);

                        var r = new Rectangle(rect.X + weekDay*dayWidth, rect.Y + week*dayHeight, dayWidth - 1, dayHeight - 1);
                        graphics.FillRectangle(brush, r);
                        if (day == DateTime.Today) graphics.DrawRectangle(Pens.Red, r);

                        CalendarRectangles[r] = day;
                    }
                    day += TimeSpan.FromDays(1);
                }
            }
        }

        DateTime lastTooltipDate;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            foreach (var r in CalendarRectangles.Keys)
            {
                if (r.Contains(e.X, e.Y))
                {
                    var date = CalendarRectangles[r];

                    if(date == lastTooltipDate) return;
                    lastTooltipDate = date;

                    var pomodoroCount = 0;
                    var productivity = 0;
                    if (productivityIndices.ContainsKey(date))
                    {
                        pomodoroCount = productivityIndices[date].Pomodoros;
                        productivity = productivityIndices[date].Rating;
                    }

                    if(viewMode == ViewModeEnum.Productivity)
                        toolTip.SetToolTip(this, date.ToShortDateString() + ": " + productivity + " (" + pomodoroCount + " pomodoros)");
                    else if(viewMode == ViewModeEnum.Pomodoros)
                        toolTip.SetToolTip(this, date.ToShortDateString() + ": " + pomodoroCount + " pomodoros  (" + productivity + " productivity points)");
                    else if (viewMode == ViewModeEnum.PPerP)
                    {
                        if(pomodoroCount > 0)
                            toolTip.SetToolTip(this, date.ToShortDateString() + ": " + productivity / pomodoroCount + " points per pomodoro");
                        else
                            toolTip.SetToolTip(this, date.ToShortDateString() + ": no pomodoros");
                    }

                    toolTip.Active = true;
                    return;
                }
            }

            toolTip.Active = false;
        }

        readonly Color baseColor = Color.FromArgb(245, 245, 245);
        readonly Color highColor = Color.FromArgb(56, 60, 154);
        readonly Color overdriveColor = Color.FromArgb(160, 64, 174);

        private Color CalculateColor(int productivity)
        {
            var high = 100;
            var overdrive = 200;

            if (productivity <= high)
            {
                var balance = (double)productivity / high;
                return BalanceColor(baseColor, highColor, balance);
            }
            if (productivity <= overdrive)
            {
                var balance = (double)(productivity - high) / (overdrive - high);
                return BalanceColor(highColor, overdriveColor, balance);
            }

            return overdriveColor;
        }

        private Color BalanceColor(Color low, Color high, double balance)
        {
            var r = low.R * (1.0 - balance) + high.R * balance;
            var g = low.G * (1.0 - balance) + high.G * balance;
            var b = low.B * (1.0 - balance) + high.B * balance;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }
    }
}
