using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.CharaInfo
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public ZS3EditorCharaInfo Editor;
        public Control UIEditor { get { return Editor; } }

        public string[] FileNamePatterns => new string[] { "common_character_info" };

        public void Initialize(string filePath)
        {
            Editor = new ZS3EditorCharaInfo(filePath);
        }
    }
}
