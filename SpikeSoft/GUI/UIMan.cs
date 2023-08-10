using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpikeSoft.UtilityManager;

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

                try
                {
                    Interface = (CommonMan.GetInterfaceObject(typeof(IEditor), ResultType) as IEditor);
                    Interface.Initialize(filePath);
                    return Interface.UIEditor;
                }
                catch (TypeLoadException)
                {
                    continue;
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
                }
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
