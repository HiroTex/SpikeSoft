using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    public static class SettingsResources
    {
        public static readonly Dictionary<string, string> TXTResourceInitializers = new Dictionary<string, string>
        {
            {"characters.txt", "CharaList" },
            {"items.txt", "ZitemList" },
            {"maps.txt", "MapList" },
            {"songs.txt", "BgmList" }
        };
        public static readonly List<Action> IMGResourceInitializers = new List<Action>
        {
            SetCharaChip
        };
        
        public static List<string> ZitemList { get; set; }
        public static List<string> MapList { get; set; }
        public static List<string> BgmList { get; set; }
        public static List<string> CharaList { get; set; }
        public static ImageList CharaChip { get; set; }

        public static List<string> SetNamesList(string txtName)
        {
            var txt_path = Path.Combine(SpikeSoft.UtilityManager.Properties.Settings.Default.CommonTXTPath, Properties.Settings.Default.CommonGAMEPath, txtName);

            if (!FileMan.ValidateFilePath(txt_path))
            {
                ExceptionMan.ThrowMessage(0x1000);
            }

            if (!File.Exists(txt_path))
            {
                // File Not Found Message
                ExceptionMan.ThrowMessage(0x1002, new string[] { txt_path });
                string defaultTXTPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "txt");
                string defaultCharTXTPath = Path.Combine(defaultTXTPath, Properties.Settings.Default.CommonGAMEPath, txtName);

                // Asks User if Wants to Create Default Character.txt Here
                if (MessageBox.Show($"Do you want to create a {txtName}\nFile at this location?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Create Default characters.txt file on Specified Path
                    File.Copy(defaultCharTXTPath, txt_path, true);
                }
                else
                {
                    // Reset Default Path for character.txt File
                    SettingsMan.Instance.ChangeResourcePath("CommonTXTPath", defaultTXTPath);
                    txt_path = defaultCharTXTPath;
                }
            }

            List<string> list = new List<string>();
            list.AddRange(File.ReadAllLines(txt_path));
            return list;
        }

        public static void SetCharaChip()
        {
            var folderPath = Path.Combine(SpikeSoft.UtilityManager.Properties.Settings.Default.CommonIMGPath, SpikeSoft.UtilityManager.Properties.Settings.Default.CommonGAMEPath, "chara_chip");

            if (!FileMan.ValidateFilePath(Path.Combine(folderPath, "0.png")))
            {
                ExceptionMan.ThrowMessage(0x1000);
            }

            if (CharaList == null)
            {
                CharaList = SetNamesList("characters.txt");
            }

            CharaChip = new ImageList();

            for (var i = 0; i < CharaList.Count; i++)
            {
                var imgPath = Path.Combine(folderPath, $"{i}.png");
                if (File.Exists(imgPath))
                {
                    CharaChip.Images.Add(Image.FromFile(imgPath));
                }
                else
                {
                    var Placeholder = new Bitmap(64, 64);
                    CharaChip.Images.Add(Placeholder);
                }
            }
        }
    }
}
