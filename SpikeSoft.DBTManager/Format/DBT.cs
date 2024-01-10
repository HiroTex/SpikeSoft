using SpikeSoft.DBTManager.DataInfo;
using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DBTManager
{
    public class DBT
    {
        private string fPath;
        private StructMan<DBTHd> hdInfo;
        private StructMan<DBTImageHd> imgInfo;

        public string FilePath
        {
            get { return fPath; }
            set { fPath = value; }
        }

        public StructMan<DBTHd> HeaderInfo
        {
            get { return hdInfo; }
        }

        public StructMan<DBTImageHd> ImageInfo
        {
            get { return imgInfo; }
        }

        /// <summary>
        /// Creates DBT Object from File
        /// </summary>
        /// <param name="filePath"></param>
        public DBT(string filePath)
        {
            fPath = filePath;
            hdInfo = new StructMan<DBTHd>(fPath, 0, 1);
            imgInfo = new StructMan<DBTImageHd>(fPath, hdInfo[0].ImageTablePtr * 4, hdInfo[0].ImageCount);
        }

        public Bitmap GetBitmapImage(int id)
        {
            // Get Image Bytes to Extract Raw Data
            byte[] bytes = File.ReadAllBytes(fPath);

            // Get Image GS TEX0 Data
            GifReg.Tex0 ImageData = new GifReg.Tex0();
            ImageData.Data = imgInfo[id].GSTEX0;

            // Get Raw Texture and Pal data with hardcoded bytes while we don't have the structs for them
            byte[] indexedTex = new byte[imgInfo[id].TexDataLength - 128];
            Array.Copy(bytes, (imgInfo[id].TexDataPtr * 4) + 96, indexedTex, 0, indexedTex.Length);
            byte[] indexedPal = new byte[imgInfo[id].PalDataLength - 128];
            Array.Copy(bytes, (imgInfo[id].PalDataPtr * 4) + 96, indexedPal, 0, indexedPal.Length);

            // Convert Raw Image to BMP
            DBT2BMP newImage = new DBT2BMP(indexedTex, indexedPal, 1 << ImageData.TW, 1 << ImageData.TH, imgInfo[id].CLUTSize);

            return newImage.GetBitmap();
        }
    }
}
