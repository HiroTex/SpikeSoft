using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.CharaInfo
{
    public partial class ZS3EditorCharaInfo : UserControl
    {
        public ZS3EditorCharaInfo()
        {
            InitializeComponent();
        }

        private int LatestSelectedIndex;

        private void CharaListIndexChanged(object sender, EventArgs e)
        {
            if (charaList.SelectedItems.Count == 0)
            {
                return;
            }

            LatestSelectedIndex = charaList.SelectedIndices[0];

            if (ItemSelectedChanged != null)
            {
                ItemSelectedChanged(sender, e);
            }
        }

        private void UpdateHUDImage(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                this.ValueChanged(sender, e);
            }

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

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when value is updated")]
        public event EventHandler ValueChanged;

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when Selected Index in ListView Changed")]
        public event EventHandler ItemSelectedChanged;

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
