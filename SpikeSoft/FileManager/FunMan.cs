using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.FileManager
{
    public class FunMan
    {
        public bool ExecuteFileFunction(string filePath)
        {
            foreach (var FileType in SpikeSoft.DataTypes.SupportedTypes.FileExtensions)
            {
                if (Path.GetExtension(filePath) != (FileType.Key) || FileType.Value == null)
                {
                    continue;
                }

                (DataTypes.CommonMan.GetInterfaceObject(typeof(DataTypes.IFunType), FileType.Value.Invoke(filePath)) as DataTypes.IFunType).InitializeHandler(filePath);

                return true;
            }

            return false;
        }
    }
}
