using SpikeSoft.DataTypes;
using SpikeSoft.DataTypes.ZS3;
using SpikeSoft.FileManager;
using SpikeSoft.ZS3Editor.CharaInfo;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SpikeSoft.GUI.ZS3.CharaInfo
{
    public class CharaInfoUIHandler : IEditor
    {
        public Control UIEditor { get { return Editor; } }
        public Size UISize {
            get { return new Size(764, 340); }
            set { }
        }

        private ZS3EditorCharaInfo Editor;
        private CharaInfoDataHandler Data;

        public void InitializeComponent(string filePath)
        {
            Data = new CharaInfoDataHandler();
            Data.InitializeTable(filePath);
            Editor = new ZS3EditorCharaInfo();
            Editor.Location = new System.Drawing.Point(0, 0);
            Editor.Dock = DockStyle.Fill;
            Editor.Enabled = true;
            Editor.Visible = true;
            Editor.SetCharaItemList(UserSettings.SettingsResources.CharaList.ToArray(), UserSettings.SettingsResources.CharaChip);
            Editor.ValidateCharaListItems(Data.GetTotalItems());
            Editor.SetCharaListDefault();
            Editor.ItemSelectedChanged += new EventHandler(ItemSelectedChanged);
            Editor.ValueChanged += new EventHandler(ValueChanged);
        }

        private void ItemSelectedChanged(object sender, EventArgs e)
        {
            if ((sender == null) || ((sender as ListView).SelectedItems.Count == 0))
            {
                return;
            }

            int SelectedChar = Editor.GetCharaListSelectedIndex();

            // If Temp File is already created, try to update Data List with Item from Temp File.

            try
            {
                if (File.Exists(TmpMan.GetDefaultTmpFile()))
                {
                    Data.UpdateTableItemFromTmp(SelectedChar);
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Catch Invalid Index and set to First Item on List is possible.
                Editor.SetCharaListDefault();
                return;
            }

            // Sets Data Values on NumericUpDown Controls
            object[] Values = new object[3];
            Values[0] = Data[SelectedChar].Initial_HP / 10000;
            Values[1] = Data[SelectedChar].Initial_KI / 20000;
            Values[2] = Data[SelectedChar].Max_Blast_Units;
            Editor.SetDataValues(Values);
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            int n = Editor.GetCharaListSelectedIndex();
            CharacterInfoTable.CharacterInfo CharInfo = new CharacterInfoTable.CharacterInfo();

            // Get Current Selected Character Information
            try
            {
                CharInfo = Data[n];
            }
            catch (IndexOutOfRangeException)
            {
                // Set First Item on List if Current Selected is Invalid
                Editor.SetCharaListDefault();
                return;
            }

            // Update Item Data
            var Sender = sender as NumericUpDown;
            switch (Sender.Name)
            {
                case "hpBarNumeric":
                    CharInfo.Initial_HP = (int)Sender.Value * 10000;
                    break;
                case "kiBarNumeric":
                    CharInfo.Initial_KI = (int)Sender.Value * 20000;
                    break;
                case "blastNumeric":
                    CharInfo.Max_Blast_Units = (int)Sender.Value;
                    break;
            }

            // Update Item Data on List
            Data[n] = CharInfo;

            // Update Binary File
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), DataMan.StructToData(CharInfo), n * Marshal.SizeOf(typeof(CharacterInfoTable.CharacterInfo)));
        }
    }
}
