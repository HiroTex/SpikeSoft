using System;
using SpikeSoft.ZS3Editor.TourUnlockables;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using SpikeSoft.FileManager;
using SpikeSoft.DataTypes;
using SpikeSoft.DataTypes.ZS3;
using System.Runtime.InteropServices;

namespace SpikeSoft.GUI.ZS3.TourUnlockables
{
    class TourUnlockablesUIHandler : IEditor
    {
        public Control UIEditor { get { return Editor; } }
        public Size UISize { get { return new Size(605, 670); } set { } }

        private ZS3EditorTourUnlockables Editor;
        private TourUnlockablesDataHandler Data;

        public void InitializeComponent(string filePath)
        {
            Data = new TourUnlockablesDataHandler(filePath);

            List<string> zitemList = SpikeSoft.UserSettings.SettingsResources.ZitemList;
            List<string> charaList = SpikeSoft.UserSettings.SettingsResources.CharaList;
            List<string> mapList = SpikeSoft.UserSettings.SettingsResources.MapList;
            List<string> bgmList = SpikeSoft.UserSettings.SettingsResources.BgmList;

            List<List<string>> lists = new List<List<string>>() { zitemList, charaList, mapList, bgmList };
            foreach (var list in lists)
            {
                list.Add("Empty");
            }

            InitializeEditor(lists[0].ToArray(), lists[1].ToArray(), lists[2].ToArray(), lists[3].ToArray());
        }

        private void InitializeEditor(string[] zitemList, string[] charaList, string[] mapList, string[] bgmList)
        {
            Editor = new ZS3EditorTourUnlockables(zitemList, charaList, mapList, bgmList);
            Editor.Location = new System.Drawing.Point(0, 0);
            Editor.Dock = DockStyle.Fill;
            Editor.Enabled = true;
            Editor.Visible = true;
            Editor.ModeSelectedChanged += UpdateEditorData;
            Editor.TourSelectedChanged += UpdateEditorData;
            Editor.DiffSelectedChanged += UpdateEditorData;
            Editor.ValueChanged += ValueChanged;
            Editor.InitializeDefaults();
        }

        private void UpdateEditorData(object sender, EventArgs e)
        {
            if (!IsValidEventArgs(sender))
            {
                return;
            }

            int selectedMode = Editor.GetCurrentModeSelected();
            int selectedTour = Editor.GetCurrentTourSelected();
            int selectedDiff = Editor.GetCurrentDiffSelected();

            if (!IsValidSelection(selectedMode, selectedTour, selectedDiff))
            {
                return;
            }

            // If Temp File is already created, try to update Data List with Item from Temp File.

            try
            {
                UpdateDataFromTempFile(selectedMode, selectedTour);
            }
            catch (IndexOutOfRangeException)
            {
                // Catch Invalid Index and set to First Item on List is possible.
                Editor.InitializeDefaults();
                return;
            }

            // Sets Data Values on Editor Controls
            object[] values = GetValuesFromData(selectedMode, selectedTour, selectedDiff);

            try
            {
                Editor.SetDataValues(values);
            }
            catch (Exception)
            {
                ExceptionMan.ThrowMessage(0x2001);
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

        private bool IsValidSelection(int selectedMode, int selectedTour, int selectedDiff)
        {
            return selectedMode != -1 && selectedTour != -1 && selectedDiff != -1;
        }

        private void UpdateDataFromTempFile(int selectedMode, int selectedTour)
        {
            if (File.Exists(TmpMan.GetDefaultTmpFile()))
            {
                Data.IUpdateTableItemFromTmp((selectedMode * 5) + selectedTour);
            }
        }

        private object[] GetValuesFromData(int selectedMode, int selectedTour, int selectedDiff)
        {
            TourUnlockablesTable.TourUnlockables item = Data[(selectedMode * 5) + selectedTour];

            object[] values = new object[]
            {
                item.Unknown_A,
                item.Unknown_B,
                item.Unknown_C,
                item.Unknown_D,
                item.Zeni_Winner[selectedDiff],
                item.Zeni_RunnerUp[selectedDiff],
                item.ZItem[selectedDiff],
                item.Chara1[selectedDiff],
                item.Chara2[selectedDiff],
                item.Map[selectedDiff],
                item.Bgm[selectedDiff]
            };

            return values;
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

            int SelectedMode = Editor.GetCurrentModeSelected();
            int SelectedTour = Editor.GetCurrentTourSelected();
            int SelectedDiff = Editor.GetCurrentDiffSelected();

            if (SelectedDiff == -1 || SelectedTour == -1 || SelectedMode == -1)
            {
                return;
            }

            int n = SelectedTour + (SelectedMode * 5);

            TourUnlockablesTable.TourUnlockables Obj = new TourUnlockablesTable.TourUnlockables();

            // Get Current Selected Character Information
            try
            {
                Obj = Data[n];
            }
            catch (IndexOutOfRangeException)
            {
                // Set First Item on List if Current Selected is Invalid
                Editor.InitializeDefaults();
                return;
            }

            // Update Item Data

            if (control is NumericUpDown)
            {
                UpdateNumericValue(control as NumericUpDown, Obj, SelectedDiff);
            }

            if (control is ComboBox)
            {
                UpdateComboBoxValue(control as ComboBox, Obj, SelectedDiff);
            }

            // Update Item Data on List
            Data[n] = Obj;

            // Update Binary File
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), DataMan.StructToData(Obj), n * Marshal.SizeOf(typeof(TourUnlockablesTable.TourUnlockables)));
        }

        private void UpdateNumericValue(NumericUpDown numericUpDown, TourUnlockablesTable.TourUnlockables obj, int selectedDiff)
        {
            int newValue = (int)numericUpDown.Value;
            string controlName = numericUpDown.Name;

            Dictionary<string, Action> numericActions = new Dictionary<string, Action>
            {
                { "numericUnknownA", () => obj.Unknown_A = newValue },
                { "numericUnknownB", () => obj.Unknown_B = newValue },
                { "numericUnknownC", () => obj.Unknown_C = newValue },
                { "numericUnknownD", () => obj.Unknown_D = newValue },
                { "numericZeni1", () => obj.Zeni_Winner[selectedDiff] = newValue },
                { "numericZeni2", () => obj.Zeni_RunnerUp[selectedDiff] = newValue }
            };

            if (numericActions.ContainsKey(controlName))
            {
                numericActions[controlName].Invoke();
            }
        }

        private void UpdateComboBoxValue(ComboBox comboBox, TourUnlockablesTable.TourUnlockables obj, int selectedDiff)
        {
            int newIndex = comboBox.SelectedIndex;
            if (newIndex == comboBox.Items.Count - 1)
            {
                newIndex = -1;
            }

            string controlName = comboBox.Name;

            Dictionary<string, Action> comboBoxActions = new Dictionary<string, Action>
            {
                { "comboZItem", () => obj.ZItem[selectedDiff] = newIndex },
                { "comboChara1", () => obj.Chara1[selectedDiff] = newIndex },
                { "comboChara2", () => obj.Chara2[selectedDiff] = newIndex },
                { "comboMap", () => obj.Map[selectedDiff] = newIndex },
                { "comboBgm", () => obj.Bgm[selectedDiff] = newIndex }
            };

            if (comboBoxActions.ContainsKey(controlName))
            {
                comboBoxActions[controlName].Invoke();
            }
        }
    }
}
