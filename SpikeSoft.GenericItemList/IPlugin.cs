using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SpikeSoft.GenericItemList
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public GenericItemListUI Editor;
        public Control UIEditor { get { return Editor; } }
        public Size UISize { get { return new Size(1035, 575); } set { } }
        public static string[] FileNamePatterns = new string[]
        {
            "random_chara_list",
            "random_map_list",
            "random_bgm_list",
            "character_select_order"
        };

        public void Initialize(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            ImageList img = null;
            string[] items = null;
            if (fileName.Contains("chara"))
            {
                img = UtilityManager.SettingsResources.CharaChip;
                items = UtilityManager.SettingsResources.CharaList.ToArray();
            }
            else if (fileName.Contains("map"))
            {
                img = UtilityManager.SettingsResources.MapChip;
                items = UtilityManager.SettingsResources.MapList.ToArray();
            }
            else if (fileName.Contains("bgm"))
            {
                img = new ImageList();
                img.ColorDepth = ColorDepth.Depth32Bit;
                img.ImageSize = new Size(64, 64);
                items = UtilityManager.SettingsResources.BgmList.ToArray();
                Image bgm = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("SpikeSoft.GenericItemList.BGM.png"));
                foreach (var item in items)
                {
                    img.Images.Add(bgm);
                }
            }

            List<int> list = new List<int>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            using (var br = new BinaryReader(fs))
            {
                while (fs.Position != fs.Length)
                {
                    list.Add(br.ReadInt32());
                }
            }

            Editor = new GenericItemListUI(filePath, img, items, list.ToArray());
        }
    }
}
