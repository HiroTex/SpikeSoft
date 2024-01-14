using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.UiUtils
{
    public class DarkRenderer : ToolStripProfessionalRenderer
    {
        public DarkRenderer() : base(new DarkColorTable()) { }
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

    public class DarkColorTable : ProfessionalColorTable
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
}
