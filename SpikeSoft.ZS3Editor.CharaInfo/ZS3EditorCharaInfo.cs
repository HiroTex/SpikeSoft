using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SpikeSoft.UtilityManager;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpikeSoft.ZS3Editor.CharaInfo
{
    public partial class ZS3EditorCharaInfo : UserControl
    {
        private DataHandler.BinaryHandler Data;

        public ZS3EditorCharaInfo()
        {
            InitializeComponent();
        }

        public ZS3EditorCharaInfo(string filePath)
        {
            InitializeComponent();
            Data = new DataHandler.BinaryHandler(filePath);
            Location = new System.Drawing.Point(0, 0);
            Dock = DockStyle.Fill;
            Enabled = true;
            Visible = true;
            SetCharaItemList(SettingsResources.CharaList.ToArray(), SettingsResources.CharaChip);
            ValidateCharaListItems(Data.GetTotalItems());
        }

        private int LatestSelectedIndex;

        private void CharaListIndexChanged(object sender, EventArgs e)
        {
            if (charaList.SelectedItems.Count == 0)
            {
                return;
            }

            LatestSelectedIndex = charaList.SelectedIndices[0];

            ItemSelectedChanged(sender, e);
        }

        private void UpdateHUDImage(object sender, EventArgs e)
        {
            ValueChanged(sender, e);

            var imgBase = new Bitmap(hudPicture.BackgroundImage.Width, hudPicture.BackgroundImage.Height);
            var imgDraw = Graphics.FromImage(imgBase);
            var HP = (int)hpBarNumeric.Value;
            var HP_Unit = Properties.Resources.ResourceManager.GetObject($"HealthBar_3_{HP - 3}");
            var Ki = Properties.Resources.ResourceManager.GetObject($"HealthBar_Ki_{(int)kiBarNumeric.Value}");
            var B1 = Properties.Resources.ResourceManager.GetObject($"HealthBar_Blast_{(int)blastNumeric.Value}");

            if (B1 != null)
                imgDraw.DrawImageUnscaled((Bitmap)B1, new Point(0, 0));
            if (Ki != null)
                imgDraw.DrawImageUnscaled((Bitmap)Ki, new Point(0, 0));

            if (HP > 7)
            {
                imgDraw.DrawImageUnscaled(Properties.Resources.HealthBar_8, new Point(0, 0));
                imgDraw.DrawImageUnscaled(Properties.Resources.HealthBar_3_4, new Point(0, 0));
            }
            else if (HP > 2)
            {
                imgDraw.DrawImageUnscaled(Properties.Resources.HealthBar_3, new Point(0, 0));
            }
            else
            {
                imgDraw.DrawImageUnscaled((Bitmap)Properties.Resources.ResourceManager.GetObject($"HealthBar_{HP}"), new Point(0, 0));
            }

            if (HP_Unit != null)
                imgDraw.DrawImageUnscaled((Bitmap)HP_Unit, new Point(0, 0));

            hudPicture.Image = imgBase;
        }

        private void ItemSelectedChanged(object sender, EventArgs e)
        {
            if ((sender == null) || ((sender as ListView).SelectedItems.Count == 0))
            {
                return;
            }

            int SelectedChar = GetCharaListSelectedIndex();

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
                SetCharaListDefault();
                return;
            }

            // Sets Data Values on NumericUpDown Controls
            object[] Values = GetValuesFromData(SelectedChar);

            try
            {
                SetDataValues(Values);
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

            int n = GetCharaListSelectedIndex();
            DataInfo.CharaInfoObj.CharacterInfo CharInfo = new DataInfo.CharaInfoObj.CharacterInfo();

            // Get Current Selected Character Information
            try
            {
                CharInfo = Data[n];
            }
            catch (IndexOutOfRangeException)
            {
                // Set First Item on List if Current Selected is Invalid
                SetCharaListDefault();
                return;
            }

            // Update Item Data on List
            CharInfo = UpdateCharacterData(sender as NumericUpDown, CharInfo);
            Data[n] = CharInfo;

            // Update Binary File
            UpdateBinaryFile(n, CharInfo);
        }

        private DataInfo.CharaInfoObj.CharacterInfo UpdateCharacterData(NumericUpDown numericUpDown, DataInfo.CharaInfoObj.CharacterInfo charInfo)
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

            return charInfo;
        }

        private void UpdateBinaryFile(int index, DataInfo.CharaInfoObj.CharacterInfo charInfo)
        {
            var charInfoData = DataMan.StructToData(charInfo);
            int offset = index * Marshal.SizeOf(typeof(DataInfo.CharaInfoObj.CharacterInfo));
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), charInfoData, offset);
        }

        /// <summary>
        /// Set CharaList Item List Data and Image List
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="imageList"></param>
        public void SetCharaItemList(string[] itemList, ImageList imageList)
        {
            charaList.BeginUpdate();

            // Update imageList
            if (imageList != null)
            {
                imageList.ImageSize = new Size(64, 64);
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                charaList.SmallImageList = imageList;
            }

            // Set Items in List
            charaList.Items.Clear();
            if (itemList != null)
            {
                for (var i = 0; i < itemList.Length; i++)
                {
                    charaList.Items.Add(itemList[i]);
                    charaList.Items[i].ImageIndex = i;
                }
            }

            charaList.Columns[0].Width = charaList.Width - 18;
            charaList.MultiSelect = false;
            charaList.EndUpdate();
            LatestSelectedIndex = 0;
            SetCharaListDefault();
        }

        /// <summary>
        /// Deletes Not Editable List Items
        /// </summary>
        /// <param name="items">Valid Item List Count</param>
        public void ValidateCharaListItems(int items)
        {
            if (charaList.Items.Count > items)
            {
                do
                {
                    charaList.Items.RemoveAt(charaList.Items.Count - 1);
                } while (charaList.Items.Count > items);
            }
        }

        /// <summary>
        /// Sets ListView Current Selected Item to First
        /// </summary>
        public void SetCharaListDefault()
        {
            if (charaList.Items.Count < 1)
            {
                throw new IndexOutOfRangeException("Character List doesn't have Items");
            }

            charaList.Items[LatestSelectedIndex].Selected = true;
        }

        /// <summary>
        /// Get Current Character List Selected Index
        /// </summary>
        /// <returns></returns>
        public int GetCharaListSelectedIndex()
        {
            return LatestSelectedIndex;
        }

        /// <summary>
        /// Sets NumericUpDown Boxes Values
        /// </summary>
        /// <param name="values">[0] HP [1] Ki [2] Blasts</param>
        public void SetDataValues(object[] values)
        {
            if (values[0] != null)
            {
                hpBarNumeric.Value = (int)values[0];
            }
            if (values[1] != null)
            {
                kiBarNumeric.Value = (int)values[1];
            }
            if (values[2] != null)
            {
                blastNumeric.Value = (int)values[2];
            }
        }
    }
}
