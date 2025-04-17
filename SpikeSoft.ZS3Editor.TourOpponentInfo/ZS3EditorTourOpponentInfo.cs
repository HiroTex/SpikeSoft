using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.TourOpponentInfo
{
    public partial class ZS3EditorTourOpponentInfo : UserControl
    {
        private StructMan<TourOpponentInfo> Data;
        public ZS3EditorTourOpponentInfo(string filepath, List<string> zItemList)
        {
            Data = new StructMan<TourOpponentInfo>(filepath);
            InitializeComponent();
            int boxIndex = 0;
            foreach (ComboBox box in zItemPanel.Controls.OfType<ComboBox>().OrderBy(c => c.Name))
            {
                box.Tag = boxIndex++;
                box.DataSource = new List<string>(zItemList);
                box.Enabled = true;
            }

            Location = new System.Drawing.Point(0, 0);
            Dock = DockStyle.Fill;
            Enabled = true;
            Visible = true;
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            SelectIndexOrDefault(boxSelectDiff, 0);
            SelectIndexOrDefault(boxSelectRound, 0);
        }

        private void SelectIndexOrDefault(ComboBox combo, int defaultIndex)
        {
            if (combo.Items.Count > defaultIndex)
            {
                combo.SelectedIndex = defaultIndex;
            }
            else if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
            else
            {
                combo.SelectedIndex = -1;
            }
        }

        private void UpdateEditorData(object sender, EventArgs e)
        {
            if (!IsValidEventArgs(sender) || !TryGetSelectedIndex(out int selectedDiff, out int selectedRound))
            {
                return;
            }

            // If Temp File is already created, try to update Data List with Item from Temp File.

            try
            {
                if (File.Exists(TmpMan.GetDefaultTmpFile()))
                {
                    Data.IUpdateTableItemFromTmp((selectedDiff * 5) + selectedRound);
                }
            }
            catch (IndexOutOfRangeException)
            {
                // Catch Invalid Index and set to First Item on List is possible.
                InitializeDefaults();
                return;
            }

            // Sets Data Values on Editor Controls
            TourOpponentInfo item = Data[(selectedDiff * 5) + selectedRound];

            try
            {
                aiNumeric.Value = item.AI;

                int boxIndex = 0;
                foreach (ComboBox box in zItemPanel.Controls.OfType<ComboBox>().OrderBy(c => c.Name))
                {
                    box.SelectedIndex = (item.ZItems[boxIndex] == -1) ? box.Items.Count - 1 : item.ZItems[boxIndex];
                    boxIndex++;
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2001, new string[] { ex.Message });
            }
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            var control = sender as Control;

            if (TmpMan.GetDefaultTmpFile() == string.Empty || (!(control is NumericUpDown) && !(control is ComboBox)) || !TryGetSelectedIndex(out int selectedDiff, out int selectedRound))
            {
                return;
            }

            int n = selectedRound + (selectedDiff * 5);

            TourOpponentInfo Obj = new TourOpponentInfo();

            // Get Current Selected Character Information
            try
            {
                Obj = Data[n];
            }
            catch (IndexOutOfRangeException)
            {
                // Set First Item on List if Current Selected is Invalid
                InitializeDefaults();
                return;
            }

            // Update Item Data

            if (control is NumericUpDown numeric)
            {
                Obj.AI = (int)numeric.Value;
            }
            else if (control is ComboBox box)
            {
                int index = box.SelectedIndex;
                if (index == -1)
                {
                    return;
                }
                if (control.Name.Contains("ZItem"))
                {
                    if (index == box.Items.Count - 1)
                    {
                        index = -1;
                    }
                    var zIndex = (int)control.Tag;
                    Obj.ZItems[zIndex] = (short)index;
                }
            }

            // Update Item Data on List
            Data[n] = Obj;

            // Update Binary File
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), DataMan.StructToData(Obj), n * Marshal.SizeOf(Obj.GetType()));
        }
        private void UpdateLevelImage(object sender, EventArgs e)
        {
            UpdateEditorData(sender, e);

            Image newImg = Properties.Resources.ResourceManager.GetObject($"Level_{boxSelectDiff.SelectedIndex + 1}") as Image;
            if (newImg != null)
            {
                pictureLevel.Image = newImg;
            }
        }
        private bool TryGetSelectedIndex(out int diff, out int round)
        {
            diff = boxSelectDiff.SelectedIndex;
            round = boxSelectRound.SelectedIndex;

            return diff >= 0 && round >= 0;
        }

        private bool IsValidEventArgs(object sender)
        {
            if (sender == null || !(sender is ComboBox))
            {
                return false;
            }

            ComboBox comboBox = (ComboBox)sender;
            return comboBox.SelectedIndex != -1;
        }
    }
}
