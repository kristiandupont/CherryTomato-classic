using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CherryTomato.Core.PluginArchitecture;
using System.Reflection;

namespace CherryTomato.DebugHelper
{
    public partial class EventsAndCommandsBrowserForm : Form
    {
        private PluginRepository plugins;

        public EventsAndCommandsBrowserForm()
        {
            InitializeComponent();
        }

        public EventsAndCommandsBrowserForm(PluginRepository plugins) : this()
        {
            this.plugins = plugins;

            foreach (var e in this.plugins.CherryEvents.All)
            {
                this.dataEvents.Rows.Add(new object[] 
                { 
                    e.Name, 
                    e.Listeners.Count(),
                    this.GetPrettyAssemblyName(e.ContainerAssembly), 
                    e.Description,
                });
            }

            foreach (var c in this.plugins.CherryCommands.All)
	        {
                this.dataCommands.Rows.Add(new object[] 
                { 
                    c.Name, 
                    this.GetPrettyAssemblyName(c.ContainerAssembly), 
                    c.Description,
                });
            }
        }

        private void dataEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataEvents.Columns[e.ColumnIndex] == this.eventListeners)
            {
                var cell = this.dataEvents.Rows[e.RowIndex].Cells[this.eventName.Name];
                var toolTipText = this.GetEventListenersText(cell.Value.ToString());

                var tt = new ToolTip();
                tt.Show(toolTipText, this, this.PointToClient(Cursor.Position), 5000);
            }
        }

        private string GetEventListenersText(string eventName)
        {
            return string.Join(
                ", ", 
                this.plugins.CherryEvents[eventName].
                    Listeners.
                    Select(l => this.GetPrettyAssemblyName(l.ContainerAssembly)).
                    ToArray());            
        }

        private string GetPrettyAssemblyName(Assembly assembly)
        {
            return assembly.GetName().Name.Replace("CherryTomato.", string.Empty);
        }
    }
}
