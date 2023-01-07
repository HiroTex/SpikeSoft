using SpikeSoft.UserSettings;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpikeSoft.GUI
{
    class UIMan
    {
        IEditor Interface;

        public Control GetEditorUI(string filePath)
        {
            foreach (var FileType in SpikeSoft.DataTypes.SupportedTypes.FileExtensions)
            {
                if (Path.GetExtension(filePath) != (FileType.Key) || FileType.Value == null)
                {
                    continue;
                }

                Type UIEditor = FileType.Value.Invoke(filePath);

                if (UIEditor == null)
                {
                    // No Editor Implemented for this File (Yet)
                    return null;
                }

                if (!typeof(IEditor).IsAssignableFrom(UIEditor))
                {
                    throw new Exception($"Class {UIEditor} is not UI Interface Assignable");
                }

                Interface = (IEditor)Activator.CreateInstance(UIEditor);

                if (Interface == null)
                {
                    throw new Exception($"Could not Create Instance of Class {UIEditor}");
                }

                Interface.InitializeComponent(filePath);
                return Interface.UIEditor;
            }

            return null;
        }

        public Size GetEditorUISize(string editor)
        {
            if (Interface == null)
            {
                throw new ArgumentNullException("WindowSize", "Unrecognized Editor");
            }
            return Interface.UISize;
        }
    }
}
