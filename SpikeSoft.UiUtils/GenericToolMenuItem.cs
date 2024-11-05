using System;
using System.Windows.Forms;

namespace SpikeSoft.UiUtils
{
    public abstract class GenericToolMenuItem<T> where T : GenericToolMenuItem<T>
    {
        public ToolStripMenuItem ToolBtn { get; private set; }

        protected GenericToolMenuItem(string buttonText)
        {
            // Initialize ToolBtn with customizable text
            ToolBtn = new ToolStripMenuItem
            {
                Text = buttonText
            };

            ToolBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ToolBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            ToolBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;

            // Attach the click event to a virtual method that can be overridden
            ToolBtn.Click += (sender, e) => OnToolBtnClick(sender, e);
        }

        // Virtual method for click event, to be overridden in derived classes
        protected virtual void OnToolBtnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Default action for " + ToolBtn.Text, "Action");
        }
    }
}
