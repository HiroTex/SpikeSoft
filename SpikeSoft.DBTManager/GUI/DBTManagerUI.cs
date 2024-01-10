using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpikeSoft.UtilityManager;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SpikeSoft.DBTManager.DataInfo;

namespace SpikeSoft.DBTManager
{
    public partial class UIDBTManager: UserControl
    {
        private List<DBT> dbtList = new List<DBT>();
        private DBT currentDBT = null;

        public UIDBTManager()
        {
            InitializeComponent();
            menuStrip1.Renderer = new MyRenderer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenDbt();
        }

        private void SetBackgroundImage(object sender, EventArgs e)
        {
            var img = (sender as ToolStripMenuItem).Checked ? Properties.Resources.alpha : null;
            imgFull.BackgroundImage = img;
        }

        private void OpenDbt()
        {
            try
            {
                var fpath = GetDbtFilePath();
                var fdir = FileMan.FileToDir(fpath);
                var obj = new DBT(fpath);
                dbtList.Add(obj);
                itemDbtList.Items.Add(Path.GetFileNameWithoutExtension(obj.FilePath));
            }
            catch (Exception ex) when (ex.Message == "0001")
            {
                // No File was Selected
                return;
            }

            itemDbtList.SelectedIndex = itemDbtList.Items.Count - 1;
        }

        private string GetDbtFilePath()
        {
            var filterNames = new List<string>() { "Image Container File (*.dbt)" };
            var filterExt = new List<string>() { "*.dbt" };
            var result = FileMan.GetFilePath("Open DBT File", filterNames, filterExt);
            if (!FileMan.ValidateFilePath(result)) throw new Exception("0001");
            return result;
        }

        private void ResetImageList()
        {
            imgList.Images.Clear();
            itemImgList.Items.Clear();
            itemImgList.SmallImageList = imgList;
        }

        private void FillImageList()
        {
            int counter = 0;
            DBT obj = currentDBT;

            foreach (DBTImageHd image in obj.ImageInfo)
            {
                // Skip 32bpp images for now
                if (image.PalDataLength == 0) continue;

                // Add Image to Image List
                var newImage = obj.GetBitmapImage(counter);
                imgList.Images.Add($"{counter}", FixedSize(newImage, 64, 64));

                AddNewItemToItemList($"{counter}", $"{counter}");

                counter++;
            }
        }

        private void AddNewItemToItemList(string name, string imgKey)
        {
            ListViewItem item = new ListViewItem();
            item.Text = name;
            item.ImageKey = imgKey;
            itemImgList.Items.Add(item);
        }

        private static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Transparent);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        private void SetImageList(object sender, EventArgs e)
        {
            if (itemDbtList.SelectedIndices.Count < 1)
            {
                return;
            }

            currentDBT = dbtList[itemDbtList.SelectedIndex];
            ResetImageList();
            FillImageList();
            itemImgList.Items[0].Selected = true;
            itemImgList.Select();
        }

        private void DisplayMainImg(object sender, EventArgs e)
        {
            if (itemImgList.SelectedIndices.Count < 1)
            {
                return;
            }

            DBT obj = currentDBT;
            imgFull.Image = obj.GetBitmapImage(itemImgList.SelectedIndices[0]);
            CenterPictureBox();
            SetStatLabel();
        }

        private void CenterPictureBox()
        {
            imgFull.Location = new Point((imgFull.Parent.ClientSize.Width / 2) - (imgFull.Image.Width / 2),
                                        (imgFull.Parent.ClientSize.Height / 2) - (imgFull.Image.Height / 2));
            imgFull.Refresh();
        }

        private void SetStatLabel()
        {
            DBT obj = currentDBT;
            DBTImageHd imgHdData = obj.ImageInfo[itemImgList.SelectedIndices[0]];
            GifReg.Tex0 ImageData = new GifReg.Tex0();
            ImageData.Data = imgHdData.GSTEX0;
            statLabel.Text = $"Size: {1 << ImageData.TW}x{1 << ImageData.TH} Colors: {(imgHdData.PalDataLength - 128) / 4}";
        }
    }
}
