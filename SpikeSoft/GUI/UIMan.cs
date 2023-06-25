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

                var ResultType = FileType.Value.Invoke(filePath);

                if (!typeof(IEditor).IsAssignableFrom(ResultType))
                {
                    continue;
                }

                Interface = (DataTypes.CommonMan.GetInterfaceObject(typeof(IEditor), ResultType) as IEditor);
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
