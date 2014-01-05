using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CherryTomato.Core;
using System.Windows.Forms;
using CherryTomato.Core.Pomodoro;

namespace CherryTomato.PomodoroEvaluation
{
    public class TaggingController
    {
        private List<string> tagCloud = new List<string>();

        private TaggingControl control;
        private PomodoroEvaluationForm pomodoroEvaluationForm;
        private CompletedPomodoro pomodoroData;

        public TaggingController(TaggingControl control, PomodoroEvaluationForm ownerForm)
        {
            this.control = control;
            this.pomodoroEvaluationForm = ownerForm;

            this.control.TextBoxControl.KeyPress += this.tagTextBox_KeyPress;
            this.control.TextBoxControl.Enter += this.tagTextBox_Enter;
            this.control.TextBoxControl.Leave += this.tagTextBox_Leave;
        }

        public void SetData(CompletedPomodoro data)
        {
            this.pomodoroData = data;
        }

        public void PopulateData()
        {
            foreach (var tag in this.tagCloud)
            {
                this.AddTagCheckBox(tag, false);
            }
        }

        private void tagTextBox_Enter(object sender, EventArgs e)
        {
            this.pomodoroEvaluationForm.DisableDefaultAcceptButton();
        }

        private void tagTextBox_Leave(object sender, EventArgs e)
        {
            this.pomodoroEvaluationForm.EnableDefaultAcceptButton();
        }

        private void AddTagCheckBox(string tag, bool isChecked)
        {
            var cb = new CheckBox
            {
                Text = tag,
                Appearance = Appearance.Button,
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                CheckState = isChecked ? CheckState.Checked : CheckState.Unchecked,
            };

            this.control.Panel.Controls.Add(cb);
        }

        private void tagTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.AddTags(this.control.TextBoxControl.Text);
                this.control.TextBoxControl.Text = string.Empty;
            }
        }

        private void AddTags(string tagsString)
        {
            var tags = tagsString.Split(' ');

            foreach (var tag in tags)
            {
                var tagLowered = tag.ToLower();

                if (this.tagCloud.Contains(tag, StringComparer.InvariantCultureIgnoreCase))
                {
                    var cb = this.control.Panel.Controls.
                        Cast<Control>().
                        OfType<CheckBox>().
                        Single(c => c.Text.ToLower() == tagLowered);
                    cb.Checked = true;
                }
                else
                {
                    this.tagCloud.Add(tag);
                    this.AddTagCheckBox(tag, true);
                }
            }
        }
    }
}
