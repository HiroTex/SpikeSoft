using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.TourUnlockables
{
    public partial class ZS3EditorTourUnlockables: UserControl
    {

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when value is updated")]
        public event EventHandler ValueChanged;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Selected Index in BoxSelectMode Changed")]
        public event EventHandler ModeSelectedChanged;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Selected Index in BoxSelectTour Changed")]
        public event EventHandler TourSelectedChanged;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Selected Index in BoxSelectDiff Changed")]
        public event EventHandler DiffSelectedChanged;

        public ZS3EditorTourUnlockables()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        public ZS3EditorTourUnlockables(string[] zitemList, string[] charaList, string[] mapList, string[] bgmList)
        {
            InitializeComponent();
            InitializeBoxItemList(comboZItem, zitemList);
            InitializeBoxItemList(comboChara1, charaList);
            InitializeBoxItemList(comboChara2, charaList);
            InitializeBoxItemList(comboMap, mapList);
            InitializeBoxItemList(comboBgm, bgmList);
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
            if (TourSelectedChanged != null)
            {
                TourSelectedChanged(sender, e);
            }

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
            if (DiffSelectedChanged != null)
            {
                DiffSelectedChanged(sender, e);
            }

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
            if (ModeSelectedChanged != null)
            {
                ModeSelectedChanged(sender, e);
            }
        }

        private void UpdateValue(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
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
