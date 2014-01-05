using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using CherryTomato.Core;
using System.Diagnostics;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation
{
    public class ChartRenderer
    {
        public int Max { get; set; }

        private readonly Pen fiveMinPen;
        private readonly Brush highlightsBrush;
        private readonly Brush[] lineBrushes = new Brush[2];
        private IEnumerable<TaskRegistration> highlights;
        private List<int> summedKeyboardActivity;
        private List<int> summedMouseActivity;
        private CompletedPomodoro pomodoroData;

        public ChartRenderer()
        {
            this.Max = 15;

            this.fiveMinPen = new Pen(new SolidBrush(Color.FromArgb(255, 230, 230, 230)));
            this.highlightsBrush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
            this.lineBrushes[0] = new SolidBrush(Color.FromArgb(128, 137, 165, 78));
            this.lineBrushes[1] = new SolidBrush(Color.FromArgb(128, 185, 205, 150));
        }

        public void SetData(CompletedPomodoro data)
        {
            this.pomodoroData = data;
            this.InitializeSummedData();
            this.highlights = null;
        }

        private void InitializeSummedData()
        {
            this.summedKeyboardActivity = this.GetSummedList(pomodoroData.KeyboardActivity);
            this.summedMouseActivity = this.GetSummedList(pomodoroData.MouseActivity);
        }

        private List<int> GetSummedList(List<int> source)
        {
            var result = new List<int>();

            var period = source.Count / 50;
            var periodLeft = period;
            var currentVal = 0;
            foreach (var i in source)
            {
                currentVal += i;
                periodLeft--;

                if (periodLeft <= 0)
                {
                    result.Add(currentVal);
                    periodLeft = period;
                    currentVal = 0;
                }
            }

            result.Add(currentVal);

            return result;
        }

        public void Render(Graphics graphics, Size size)
        {
            if (this.summedKeyboardActivity == null || this.summedMouseActivity == null)
            {
                this.InitializeSummedData();
            }

            var datasets= new List<List<int>>
            { 
                this.summedKeyboardActivity, 
                this.summedMouseActivity 
            };
            foreach (var dataSet in datasets)
                foreach (var v in dataSet)
                    this.Max = Math.Max(Max, v);

            graphics.SmoothingMode = SmoothingMode.HighQuality;

            this.RenderFiveMinuteLines(graphics, size);

            this.RenderDataSet(graphics, lineBrushes[1], size, this.summedMouseActivity);
            this.RenderDataSet(graphics, lineBrushes[0], size, this.summedKeyboardActivity);

            this.RenderHighlights(graphics, size);
        }

        private void RenderFiveMinuteLines(Graphics graphics, Size size)
        {
            var deltaX = (double)size.Width / 5.0;
            var x = 0.0;

            for (var i = 0; i != 4; ++i)
            {
                x += deltaX;
                graphics.DrawLine(this.fiveMinPen, (int)x, 0, (int)x, size.Height);
            }
        }

        private void RenderHighlights(Graphics graphics, Size size)
        {
            if (this.highlights == null || !this.highlights.Any())
            {
                return;
            }

            double pomodoroDurationMsec = pomodoroData.Duration.TotalMilliseconds;

            foreach (var task in this.highlights)
            {
                var taskStartX =
                    (task.TimeStamp - this.pomodoroData.Start).TotalMilliseconds /
                    pomodoroDurationMsec *
                    size.Width;

                var taskWidth =
                    (task.TimeStamp - this.pomodoroData.Start + task.Duration).TotalMilliseconds / 
                    pomodoroDurationMsec *
                    size.Width - taskStartX;

                var rect = new Rectangle((int)taskStartX, 0, (int)taskWidth, size.Height);
                //Trace.WriteLine(string.Format("Highlight rectangle = {0}", rect));
                graphics.FillRectangle(this.highlightsBrush, rect);
            }
        }

        private void RenderDataSet(Graphics graphics, Brush brush, Size size, List<int> dataSet)
        {
            if (dataSet.Count < 2) return;
            
            var deltaX = (double)size.Width / (dataSet.Count - 1);
            var factorY = (double)size.Height / Max;

            var points = new Point[dataSet.Count];
            var index = 0;
            var x = 0.0;
            foreach (var v in dataSet)
            {
                var y = (int)(size.Height - v * factorY);
                points[index] = new Point((int)x, y);
                x += deltaX;
                index++;
            }

            var p = new GraphicsPath();
            p.AddLine(size.Width, size.Height, 0, size.Height);
            p.AddCurve(points);

            graphics.FillPath(brush, p);
        }

        public void SetHighlightedTasks(IEnumerable<TaskRegistration> tasks)
        {
            this.highlights = tasks;
        }
    }
}