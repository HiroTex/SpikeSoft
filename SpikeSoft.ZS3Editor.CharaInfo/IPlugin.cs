using System.Windows.Forms;
using System.Drawing;

namespace SpikeSoft.ZS3Editor.CharaInfo
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public ZS3EditorCharaInfo Editor;
        public Control UIEditor { get { return Editor; } }
        public Size UISize { get { return new Size(770, 375); } set { } }

        public void Initialize(string filePath)
        {
            Editor = new ZS3EditorCharaInfo(filePath);
        }
    }
}
