using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.GUI
{
    public interface IEditor
    {
        Control UIEditor { get; }
        Size UISize { get; set; }
        void InitializeComponent(string filePath);
    }
}
