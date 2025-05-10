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
            {"songs.txt", "BgmList" },
            {"referees.txt", "RefereeList" }
        };
        public static readonly List<Action> IMGResourceInitializers = new List<Action>
        {
            SetCharaChip,
            SetMapChip
        };
        
        public static List<string> ZitemList { get; set; }
        public static List<string> MapList { get; set; }
        public static List<string> BgmList { get; set; }
        public static List<string> RefereeList { get; set; }
        public static List<string> CharaList { get; set; }

        public static ImageList CharaChip { get; set; }
        public static ImageList MapChip { get; set; }

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

        public static void SetImageListFromItemList(ImageList list, string imagePath, List<string> Source, string txtSource)
        {
            var folderPath = Path.Combine(Properties.Settings.Default.CommonIMGPath, Properties.Settings.Default.CommonGAMEPath, imagePath);

            if (!FileMan.ValidateFilePath(Path.Combine(folderPath, "0.png")))
            {
                ExceptionMan.ThrowMessage(0x1000);
            }

            if (Source == null)
            {
                Source = SetNamesList(txtSource);
            }

            list.Images.Clear();

            for (var i = 0; i < Source.Count; i++)
            {
                var imgPath = Path.Combine(folderPath, $"{i}.png");
                if (File.Exists(imgPath))
                {
                    list.Images.Add(Image.FromFile(imgPath));
                }
                else
                {
                    var Placeholder = new Bitmap(64, 64);
                    list.Images.Add(Placeholder);
                }
            }
        }

        public static void SetCharaChip()
        {
            CharaChip = new ImageList();
            CharaChip.ColorDepth = ColorDepth.Depth32Bit;
            CharaChip.ImageSize = new Size(64, 64);
            SetImageListFromItemList(CharaChip, "chara_chip", CharaList, "characters.txt");
        }

        public static void SetMapChip()
        {
            MapChip = new ImageList();
            MapChip.ColorDepth = ColorDepth.Depth32Bit;
            MapChip.ImageSize = new Size(64, 64);
            SetImageListFromItemList(MapChip, "map_chip", MapList, "maps.txt");
        }
    }
}
