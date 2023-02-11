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

                Type T = FileType.Value.Invoke(filePath);

                if (T == null)
                {
                    // No Editor Implemented for this File (Yet)
                    return false;
                }

                if (!typeof(SpikeSoft.DataTypes.IFunType).IsAssignableFrom(T))
                {
                    throw new Exception($"Class {T} is not Functionality Interface Assignable");
                }

                SpikeSoft.DataTypes.IFunType Interface = (SpikeSoft.DataTypes.IFunType)Activator.CreateInstance(T);

                if (Interface == null)
                {
                    throw new Exception($"Could not Create Instance of Class {T}");
                }

                Interface.InitializeHandler(filePath);

                return true;
            }

            return false;
        }
    }
}
