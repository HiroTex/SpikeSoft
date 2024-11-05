using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Utilities
{
    public class ToolHandler
    {
        // Method to load menu items as a list of ToolStripMenuItems
        public List<ToolStripMenuItem> GetTools()
        {
            // Get the assembly containing the Tools classes
            var assembly = Assembly.GetExecutingAssembly();

            // Get all types in the assembly within the "YourNamespace.Tools" namespace
            var toolTypes = assembly.GetTypes()
                                    .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith("SpikeSoft.ZS3Utilities.Tools"));

            // Group types by their namespaces to reflect folder structure
            var groupedTypes = toolTypes.GroupBy(t => t.Namespace);

            var mainMenuItems = new List<ToolStripMenuItem>();

            foreach (var group in groupedTypes)
            {
                // Create a main ToolStripMenuItem for each sub-namespace (folder)
                string folderName = group.Key.Split('.').Last(); // Get the last part of the namespace as the folder name
                var mainMenuItem = new ToolStripMenuItem(folderName);

                mainMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                mainMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
                mainMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;

                foreach (var type in group)
                {
                    // Instantiate each tool class and retrieve its ToolBtn
                    var instance = Activator.CreateInstance(type);// Find the "ToolBtn" property or field

                    ToolStripMenuItem toolBtn = null;

                    // Check if there's a property named "ToolBtn" of type ToolStripMenuItem
                    var propertyInfo = type.GetProperty("ToolBtn", BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null && propertyInfo.PropertyType == typeof(ToolStripMenuItem))
                    {
                        toolBtn = propertyInfo.GetValue(instance) as ToolStripMenuItem;
                    }

                    // Check if there's a field named "ToolBtn" of type ToolStripMenuItem
                    var fieldInfo = type.GetField("ToolBtn", BindingFlags.Public | BindingFlags.Instance);
                    if (fieldInfo != null && fieldInfo.FieldType == typeof(ToolStripMenuItem))
                    {
                        toolBtn = fieldInfo.GetValue(instance) as ToolStripMenuItem;
                    }

                    // Add ToolBtn to mainMenu if it's found
                    if (toolBtn != null)
                    {
                        mainMenuItem.DropDownItems.Add(toolBtn);
                    }
                }

                // Add the main item (representing the folder) to the list
                mainMenuItems.Add(mainMenuItem);
            }

            return mainMenuItems;
        }
    }
}
