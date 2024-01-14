using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.GenericItemList
{
    public class Binary
    {
        public int[] items { get; set; }
        public string filePath { get; set; }

        public Binary(int[] items, string filePath)
        {
            this.items = items;
            this.filePath = filePath;
        }

        public void Update()
        {
            if (filePath == string.Empty)
            {
                return;
            }

            // Create Binary Int Array List using each Item's ImageIndex and Save to File
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            using (var bw = new BinaryWriter(fs))
            {
                foreach (int item in items)
                {
                    bw.Write(item);
                }
            }
        }
    }
}
