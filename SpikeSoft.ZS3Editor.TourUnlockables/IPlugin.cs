using SpikeSoft.UtilityManager;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace SpikeSoft.ZS3Editor.TourUnlockables
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public ZS3EditorTourUnlockables Editor;
        public Control UIEditor { get { return Editor; } }
        public Size UISize { get { return new Size(605, 670); } set { } }

        public void Initialize(string filePath)
        {
            List<string> zitemList = SettingsResources.ZitemList;
            List<string> charaList = SettingsResources.CharaList;
            List<string> mapList = SettingsResources.MapList;
            List<string> bgmList = SettingsResources.BgmList;

            List<List<string>> lists = new List<List<string>>() { zitemList, charaList, mapList, bgmList };
            foreach (var list in lists)
            {
                list.Add("Empty");
            }

            Editor = new ZS3EditorTourUnlockables(filePath, zitemList.ToArray(), charaList.ToArray(), mapList.ToArray(), bgmList.ToArray());
        }
    }
}
