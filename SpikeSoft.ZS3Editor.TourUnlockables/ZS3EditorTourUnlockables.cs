using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SpikeSoft.UtilityManager;
using System.Runtime.InteropServices;

namespace SpikeSoft.ZS3Editor.TourUnlockables
{
    public partial class ZS3EditorTourUnlockables: UserControl
    {
        private StructMan<TourUnlockables> Data;

        public ZS3EditorTourUnlockables()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        public ZS3EditorTourUnlockables(string filePath, string[] zitemList, string[] charaList, string[] mapList, string[] bgmList)
        {
            Data = new StructMan<TourUnlockables>(filePath);
            InitializeComponent();
            InitializeBoxItemList(comboZItem, zitemList);
            InitializeBoxItemList(comboChara1, charaList);
            InitializeBoxItemList(comboChara2, charaList);
            InitializeBoxItemList(comboMap, mapList);
            InitializeBoxItemList(comboBgm, bgmList);
            Location = new System.Drawing.Point(0, 0);
            Dock = DockStyle.Fill;
            Enabled = true;
            Visible = true;
            InitializeDefaults();
        }

        private void InitializeBoxItemList(ComboBox box, string[] itemList)
        {
            box.BeginUpdate();

            // Set Items in List
            box.Items.Clear();
            if (itemList != null)
            {
                box.Items.AddRange(itemList);
            }

            box.EndUpdate();
        }

        private void UpdateTourImage(object sender, EventArgs e)
        {
            UpdateEditorData(sender, e);

            var imgBase = new Bitmap(pictureTour.Image.Width, pictureTour.Image.Height);
            var imgDraw = Graphics.FromImage(imgBase);
            var newImg = Properties.Resources.ResourceManager.GetObject($"Tour_{(int)boxSelectTour.SelectedIndex + 1}");
            if (newImg != null)
            {
                imgDraw.DrawImageUnscaled((Bitmap)newImg, new Point(0, 0));
            }
            pictureTour.Image = imgBase;
        }

        private void UpdateLevelImage(object sender, EventArgs e)
        {
            UpdateEditorData(sender, e);

            var imgBase = new Bitmap(pictureLevel.Image.Width, pictureLevel.Image.Height);
            var imgDraw = Graphics.FromImage(imgBase);
            var newImg = Properties.Resources.ResourceManager.GetObject($"Level_{(int)boxSelectDiff.SelectedIndex + 1}");
            if (newImg != null)
            {
                imgDraw.DrawImageUnscaled((Bitmap)newImg, new Point(0, 0));
            }
            pictureLevel.Image = imgBase;
        }

        private void UpdateTourMode(object sender, EventArgs e)
        {
            UpdateEditorData(sender, e);
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            ValueChanged(sender, e);
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

            int SelectedMode = GetCurrentModeSelected();
            int SelectedTour = GetCurrentTourSelected();
            int SelectedDiff = GetCurrentDiffSelected();

            if (SelectedDiff == -1 || SelectedTour == -1 || SelectedMode == -1)
            {
                return;
            }

            int n = SelectedTour + (SelectedMode * 5);

            TourUnlockables Obj = new TourUnlockables();

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
                UpdateNumericValue(control as NumericUpDown, Obj, SelectedDiff);
            }

            if (control is ComboBox)
            {
                UpdateComboBoxValue(control as ComboBox, Obj, SelectedDiff);
            }

            // Update Item Data on List
            Data[n] = Obj;

            // Update Binary File
            BinMan.SetBytes(TmpMan.GetDefaultTmpFile(), DataMan.StructToData(Obj), n * Marshal.SizeOf(typeof(TourUnlockables)));
        }

        private void UpdateEditorData(object sender, EventArgs e)
        {
            if (!IsValidEventArgs(sender))
            {
                return;
            }

            int selectedMode = GetCurrentModeSelected();
            int selectedTour = GetCurrentTourSelected();
            int selectedDiff = GetCurrentDiffSelected();

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
                InitializeDefaults();
                return;
            }

            // Sets Data Values on Editor Controls
            object[] values = GetValuesFromData(selectedMode, selectedTour, selectedDiff);

            try
            {
                SetDataValues(values);
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
            TourUnlockables item = Data[(selectedMode * 5) + selectedTour];

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

        private void UpdateNumericValue(NumericUpDown numericUpDown, TourUnlockables obj, int selectedDiff)
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

        private void UpdateComboBoxValue(ComboBox comboBox, TourUnlockables obj, int selectedDiff)
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

        private IEnumerable<Control> GetControlHierarchy(Control root)
        {
            var queue = new Queue<Control>();

            queue.Enqueue(root);

            do
            {
                var control = queue.Dequeue();

                yield return control;

                foreach (var child in control.Controls.OfType<Control>())
                    queue.Enqueue(child);

            } while (queue.Count > 0);
        }

        private int GetSelectedTourSetting(object box)
        {
            if (box == null)
            {
                return -1;
            }

            var control = (this.Controls.Find((box as ComboBox).Name, true).FirstOrDefault()) as ComboBox;

            if (control == null)
            {
                return -1;
            }

            return control.SelectedIndex;
        }

        public void InitializeDefaults()
        {
            boxSelectMode.SelectedIndex = 0;
            boxSelectTour.SelectedIndex = 0;
            boxSelectDiff.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets Control Values
        /// </summary>
        /// <param name="values"></param>
        public void SetDataValues(object[] values)
        {
            for (int i = 0; i < 11; i++)
            {
                var controlCollection = GetControlHierarchy(this).Where(x => (string)x.Tag == i.ToString());

                if (controlCollection.Count() < 1)
                {
                    continue;
                }

                var control = controlCollection.FirstOrDefault();

                if (control == null || values[i] == null)
                {
                    continue;
                }

                if (control.GetType() == typeof(NumericUpDown))
                {
                    if ((int)values[i] < 0)
                    {
                        values[i] = 0;
                    }
                    else if ((int)values[i] > (control as NumericUpDown).Maximum)
                    {
                        values[i] = (control as NumericUpDown).Maximum;
                    }

                    (control as NumericUpDown).Value = (int)values[i];
                }
                else if (control.GetType() == typeof(ComboBox))
                {
                    if ((int)values[i] < 0 || (int)values[i] > (control as ComboBox).Items.Count - 1)
                    {
                        values[i] = (control as ComboBox).Items.Count - 1;
                    }

                    (control as ComboBox).SelectedIndex = (int)values[i];
                }
            }
        }

        public int GetCurrentModeSelected()
        {
            return GetSelectedTourSetting(boxSelectMode);
        }

        public int GetCurrentTourSelected()
        {
            return GetSelectedTourSetting(boxSelectTour);
        }

        public int GetCurrentDiffSelected()
        {
            return GetSelectedTourSetting(boxSelectDiff);
        }
    }
}
