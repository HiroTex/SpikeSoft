using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpikeSoft
{
    #region "MyRenderer"
    public class MyRenderer : ToolStripProfessionalRenderer
    {
        public MyRenderer() : base(new TestColorTable()) { }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected) base.OnRenderMenuItemBackground(e);
            else
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(0x4C, 0x4A, 0x48));
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                e.Graphics.FillRectangle(brush, rc);
                e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width - 2, rc.Height - 1);
            }
        }
    }

    public class TestColorTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder
        {
            get { return Color.Black; }
        }
        public override Color MenuBorder 
        {
            get { return Color.Black; }
        }
        public override Color ToolStripBorder
        {
            get { return Color.Black; }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.Black; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return Color.Black; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Color.Black; }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return Color.Black; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return Color.Black; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return ColorTranslator.FromHtml("#4C4A48"); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return ColorTranslator.FromHtml("#5F5D5B"); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ColorTranslator.FromHtml("#4C4A48"); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return ColorTranslator.FromHtml("#5F5D5B"); }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return ColorTranslator.FromHtml("#4C4A48"); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return ColorTranslator.FromHtml("#5F5D5B"); }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return ColorTranslator.FromHtml("#4C4A48"); }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return ColorTranslator.FromHtml("#5F5D5B"); }
        }
        public override Color ButtonSelectedHighlight
        {
            get { return ColorTranslator.FromHtml("#4C4A48"); }
        }
        public override Color ButtonSelectedBorder
        {
            get { return Color.Transparent; }
        }
    }
    #endregion
    public class ImageTransparency
    {
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            if (img == null) throw new ArgumentException("null img");
            opacityvalue = (opacityvalue > 1.0f) ? 1.0f : opacityvalue;
            opacityvalue = (opacityvalue < 0.0f) ? 0.0f : opacityvalue;
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

        public static Bitmap BlendImage(Image img1, Image img2, float opacityValue)
        {
            if (img1 == null || img2 == null) throw new ArgumentException("null img");
            opacityValue = (opacityValue > 1.0f) ? 1.0f : opacityValue;
            opacityValue = (opacityValue < 0.0f) ? 0.0f : opacityValue;

            Bitmap bitmap = new Bitmap(img1.Width, img1.Height);
            float invertOpcty = ((1.0f - opacityValue) < 0.05f) ? 0.0f : (1.0f - opacityValue);
            if (invertOpcty == 0.0)
            {
                return new Bitmap(img2);
            }

            img1 = ChangeOpacity(img1, invertOpcty);
            img2 = ChangeOpacity(img2, opacityValue);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(img1, 0, 0);
                g.DrawImage(img2, 0, 0);
            }

            return bitmap;
        }
    }
}
