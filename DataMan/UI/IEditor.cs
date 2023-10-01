using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    public interface IEditor
    {
        Control UIEditor { get; }
        Size UISize { get; }
        void Initialize(string filePath);
    }
}
