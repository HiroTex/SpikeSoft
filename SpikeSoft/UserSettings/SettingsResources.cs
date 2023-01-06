using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpikeSoft.UserSettings
{
    public static class SettingsResources
    {
        public static readonly List<Action> TXTResourceInitializers = new List<Action>
        {
            SetNamesList
        };
        public static readonly List<Action> IMGResourceInitializers = new List<Action>
        {
            SetCharaChip
        };

        public static List<string> CharaList { get; set; }
        public static ImageList CharaChip { get; set; }

        public static void SetNamesList()
        {
            var txt_path = Path.Combine(Properties.Settings.Default.CommonTXTPath, Properties.Settings.Default.CommonGAMEPath, "characters.txt");

            if (!FileManager.FileMan.ValidateFilePath(txt_path))
            {
                DataTypes.ExceptionMan.ThrowMessage(0x1000);
            }

            if (!File.Exists(txt_path))
            {
                // File Not Found Message
                DataTypes.ExceptionMan.ThrowMessage(0x1002, new string[] { txt_path });
                string defaultTXTPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "txt");
                string defaultCharTXTPath = Path.Combine(defaultTXTPath, Properties.Settings.Default.CommonGAMEPath, "characters.txt");

                // Asks User if Wants to Create Default Character.txt Here
                if (MessageBox.Show("Do you want to create a characters.txt\nFile at this location?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

            CharaList = new List<string>();
            CharaList.AddRange(File.ReadAllLines(txt_path));
        }

        public static void SetCharaChip()
        {
            var folderPath = Path.Combine(Properties.Settings.Default.CommonIMGPath, Properties.Settings.Default.CommonGAMEPath, "chara_chips");

            if (!FileManager.FileMan.ValidateFilePath(Path.Combine(folderPath, "0.png")))
            {
                DataTypes.ExceptionMan.ThrowMessage(0x1000);
            }

            if (CharaList == null)
            {
                SetNamesList();
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
