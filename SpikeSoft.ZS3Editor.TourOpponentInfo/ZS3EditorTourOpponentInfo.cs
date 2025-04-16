using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            boxZItem1.DataSource = new List<string>(zItemList);
            boxZItem2.DataSource = new List<string>(zItemList);
            boxZItem3.DataSource = new List<string>(zItemList);
            boxZItem4.DataSource = new List<string>(zItemList);
            boxZItem5.DataSource = new List<string>(zItemList);
            boxZItem6.DataSource = new List<string>(zItemList);
            boxZItem7.DataSource = new List<string>(zItemList);
            boxZItem8.DataSource = new List<string>(zItemList);
            Location = new System.Drawing.Point(0, 0);
            Dock = DockStyle.Fill;
            Enabled = true;
            Visible = true;
            InitializeDefaults();

            foreach (Control control in this.Controls)
            {
                if (control is ComboBox || control is NumericUpDown)
                {
                    control.Enabled = true;
                }
            }
        }

        private void InitializeDefaults()
        {
            boxSelectDiff.SelectedIndex = 0;
            boxSelectRound.SelectedIndex = 0;
        }
        private void UpdateEditorData(object sender, EventArgs e)
        {
            if (!IsValidEventArgs(sender))
            {
                return;
            }

            int selectedDiff = boxSelectDiff.SelectedIndex;
            int selectedRound = boxSelectRound.SelectedIndex;

            if (selectedDiff < 0 || selectedRound < 0)
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
                boxZItem1.SelectedIndex = (item.ZItems[0] == -1) ? boxZItem1.Items.Count - 1 : item.ZItems[0];
                boxZItem2.SelectedIndex = (item.ZItems[1] == -1) ? boxZItem2.Items.Count - 1 : item.ZItems[1];
                boxZItem3.SelectedIndex = (item.ZItems[2] == -1) ? boxZItem3.Items.Count - 1 : item.ZItems[2];
                boxZItem4.SelectedIndex = (item.ZItems[3] == -1) ? boxZItem4.Items.Count - 1 : item.ZItems[3];
                boxZItem5.SelectedIndex = (item.ZItems[4] == -1) ? boxZItem5.Items.Count - 1 : item.ZItems[4];
                boxZItem6.SelectedIndex = (item.ZItems[5] == -1) ? boxZItem6.Items.Count - 1 : item.ZItems[5];
                boxZItem7.SelectedIndex = (item.ZItems[6] == -1) ? boxZItem7.Items.Count - 1 : item.ZItems[6];
                boxZItem8.SelectedIndex = (item.ZItems[7] == -1) ? boxZItem8.Items.Count - 1 : item.ZItems[7];
            }
            catch (Exception)
            {
                ExceptionMan.ThrowMessage(0x2001);
            }
        }
        private void ValueChanged(object sender, EventArgs e)
        {
            if (TmpMan.GetDefaultTmpFile() == string.Empty)
            {
                return;
            }

            var control = sender as Control;

            if (!(control is NumericUpDown) && !(control is ComboBox))
            {
                return;
            }

            int selectedDiff = boxSelectDiff.SelectedIndex;
            int selectedRound = boxSelectRound.SelectedIndex;

            if (selectedDiff < 0 || selectedRound < 0)
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

            if (control is NumericUpDown)
            {
                Obj.AI = (int)(control as NumericUpDown).Value;
            }
            else if (control is ComboBox)
            {
                int index = (control as ComboBox).SelectedIndex;
                if (index == -1)
                {
                    return;
                }
                if (control.Name.Contains("ZItem"))
                {
                    if (index == (control as ComboBox).Items.Count - 1)
                    {
                        index = -1;
                    }

                    var item = control.Name.Substring(control.Name.Length - 1, 1);
                    Obj.ZItems[int.Parse(item) - 1] = (short)index;
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

            var newImg = Properties.Resources.ResourceManager.GetObject($"Level_{boxSelectDiff.SelectedIndex + 1}") as Image;
            if (newImg != null)
            {
                pictureLevel.Image = newImg;
            }
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
