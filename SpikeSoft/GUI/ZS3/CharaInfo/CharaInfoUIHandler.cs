using SpikeSoft.DataTypes;
using SpikeSoft.DataTypes.ZS3;
using SpikeSoft.FileManager;
using SpikeSoft.ZS3Editor.CharaInfo;
using System;
using System.Collections.Generic;
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
            Data = new CharaInfoDataHandler(filePath);
            Editor = new ZS3EditorCharaInfo();
            Editor.Location = new System.Drawing.Point(0, 0);
            Editor.Dock = DockStyle.Fill;
            Editor.Enabled = true;
            Editor.Visible = true;
            Editor.SetCharaItemList(UserSettings.SettingsResources.CharaList.ToArray(), UserSettings.SettingsResources.CharaChip);
            Editor.ValidateCharaListItems(Data.GetTotalItems());
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
                    Data.IUpdateTableItemFromTmp(SelectedChar);
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Catch Invalid Index and set to First Item on List is possible.
                Editor.SetCharaListDefault();
                return;
            }

            // Sets Data Values on NumericUpDown Controls
            object[] Values = GetValuesFromData(SelectedChar);

            try
            {
                Editor.SetDataValues(Values);
            }
            catch (Exception)
            {
                ExceptionMan.ThrowMessage(0x2001);
            }
        }

        private object[] GetValuesFromData(int SelectedChar)
        {
            object[] values = new object[]
            {
                Data[SelectedChar].Initial_HP / 10000,
                Data[SelectedChar].Initial_KI / 20000,
                Data[SelectedChar].Max_Blast_Units
            };

            return values;
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            if (TmpMan.GetDefaultTmpFile() == string.Empty || !(sender is NumericUpDown))
            {
                return;
            }

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
            UpdateCharacterData(sender as NumericUpDown, CharInfo);

            // Update Item Data on List
            Data[n] = CharInfo;

            // Update Binary File
            UpdateBinaryFile(n, CharInfo);
        }

        private void UpdateCharacterData(NumericUpDown numericUpDown, CharacterInfoTable.CharacterInfo charInfo)
        {
            int newValue = (int)numericUpDown.Value;
            string controlName = numericUpDown.Name;

            Dictionary<string, Action> numericActions = new Dictionary<string, Action>
            {
                { "hpBarNumeric", () => charInfo.Initial_HP = newValue * 10000 },
                { "kiBarNumeric", () => charInfo.Initial_KI = newValue * 20000 },
                { "blastNumeric", () => charInfo.Max_Blast_Units = newValue },
            };

            if (numericActions.ContainsKey(controlName))
            {
                numericActions[controlName].Invoke();
            }
        }

        private void UpdateBinaryFile(int index, CharacterInfoTable.CharacterInfo charInfo)
        {
            var charInfoData = DataMan.StructToData(charInfo);
            int offset = index * Marshal.SizeOf(typeof(CharacterInfoTable.CharacterInfo));
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), charInfoData, offset);
        }
    }
}
