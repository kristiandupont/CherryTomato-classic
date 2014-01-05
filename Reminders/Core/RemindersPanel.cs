using System;
using System.Linq;
using System.Windows.Forms;
using CherryTomato.Reminders.Core.Reminders;

namespace CherryTomato.Reminders.Core
{
    public partial class RemindersPanel : UserControl
    {
        public RemindersPanel()
        {
            this.InitializeComponent();
        }

        public void AddReminder(IReminder reminder)
        {
            var item = new ListViewItem { Checked = reminder.Enabled, Text = reminder.Name };
            item.SubItems.Add(reminder.TypeName);
            this.remindersListView.Items.Add(item);

            // This is hack. We assign the Tag after the item addition to the list 
            // in order to not catch the 'remindersListView.ItemChecked' event.
            // We don't need to catch the event because it sets reminder.Enable which shedules the timetrigger.
            // Thus in order to shedule time trigger only once we need the hack.
            item.Tag = reminder;
        }

        public void AddReminderPlugin(IReminderPlugin plugin, Action<IReminderPlugin> addReminderMenuClicked)
        {
            this.newReminderContextMenuStrip.Items.Add(plugin.PluginName, null, (a, b) => addReminderMenuClicked(plugin));
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            this.newReminderContextMenuStrip.Show(newButton, 0, newButton.Height);
        }

        private void remindersListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.remindersListView.SelectedItems.Count != 0)
                {
                    this.remindersListViewContextMenuStrip.Show(this.remindersListView, e.Location);
                }
            }
        }

        public event Action<IReminder> DeleteReminderClicked;

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reminder = (IReminder)this.remindersListView.SelectedItems[0].Tag;
            if(reminder == null) return;
            this.DeleteReminderClicked(reminder);
        }

        public void RemoveReminder(IReminder reminder)
        {
            var listItem = this.remindersListView.Items.Cast<ListViewItem>().Where(item => item.Tag == reminder).First();
            this.remindersListView.Items.Remove(listItem);
        }

        public event Action<IReminder> EditReminderClicked;

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var reminder = (IReminder)this.remindersListView.SelectedItems[0].Tag;
            if (reminder == null) return;
            this.EditReminderClicked(reminder);
        }

        public void UpdateReminder(IReminder reminder)
        {
            var listItem = this.remindersListView.Items.Cast<ListViewItem>().Where(item => item.Tag == reminder).First();
            listItem.Text = reminder.Name;
        }

        private void remindersListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var reminder = (IReminder)e.Item.Tag;
            if (reminder != null)
            {
                reminder.Enabled = e.Item.Checked;
            }
        }
    }
}
