using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.UiUtils
{
    public static class ImageTransparency
    {
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            if (img == null) throw new ArgumentException("null img");
            opacityvalue = CapOpacity(opacityvalue);

            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }

        public static Bitmap BlendImage(Image img1, Image img2, float opacityvalue)
        {
            if (img1 == null || img2 == null) throw new ArgumentException("null img");
            opacityvalue = CapOpacity(opacityvalue);

            Bitmap bitmap = new Bitmap(img1.Width, img1.Height);
            float invertOpcty = ((1.0f - opacityvalue) < 0.05f) ? 0.0f : (1.0f - opacityvalue);
            if (invertOpcty == 0.0)
            {
                return new Bitmap(img2);
            }

            img1 = ChangeOpacity(img1, invertOpcty);
            img2 = ChangeOpacity(img2, opacityvalue);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(img1, 0, 0);
                g.DrawImage(img2, 0, 0);
            }

            return bitmap;
        }

        private static float CapOpacity(float value)
        {
            value = (value > 1.0f) ? 1.0f : value;
            value = (value < 0.0f) ? 0.0f : value;
            return value;
        }
    }
}
