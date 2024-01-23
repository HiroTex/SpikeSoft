using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private static Bitmap ResizeAndConvertToBlackAndWhite(Bitmap original)
        {
            // Resize the image to 16x16
            Bitmap resizedImage = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, 0, 0, 16, 16);
            }

            // Convert the image to black and white
            Bitmap bwImage = new Bitmap(16, 16);
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    Color originalColor = resizedImage.GetPixel(x, y);
                    int grayscaleValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                    Color newColor = Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue);
                    bwImage.SetPixel(x, y, newColor);
                }
            }

            return bwImage;
        }

        public static bool AreImagesEqual(Bitmap image1, Bitmap image2)
        {
            // Check dimensions
            if (image1.Width != image2.Width || image1.Height != image2.Height)
                return false;

            image1 = ResizeAndConvertToBlackAndWhite(image1);
            image2 = ResizeAndConvertToBlackAndWhite(image2);

            // Compare pixel by pixel
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    var px1 = image1.GetPixel(x, y);
                    var px2 = image2.GetPixel(x, y);
                    if (px1 != px2)
                        return false;
                }
            }

            // If all pixels are equal, the images are considered equal
            return true;
        }
    }
}
