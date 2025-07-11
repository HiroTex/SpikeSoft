using SpikeSoft.UtilityManager;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.TourOpponentInfo
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public ZS3EditorTourOpponentInfo Editor;
        public Control UIEditor { get { return Editor; } }

        public string[] FileNamePatterns => new string[] { "opponent_param" };

        public void Initialize(string filePath)
        {
            List<string> zitemList = SettingsResources.ZitemList;
            zitemList.Add("Empty");

            Editor = new ZS3EditorTourOpponentInfo(filePath, zitemList);
        }
    }
}
