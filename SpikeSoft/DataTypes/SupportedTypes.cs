using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes
{
    public static class SupportedTypes
    {
        // Dictionary that contains:
        // Key: Type of File
        // Value: File Extension Filter for Data Type
        public static readonly Dictionary<string, string> MainFilterList = ReadTxtDictionary("SupportedTypes.txt");

        // Dictionary that contains:
        // Key: Hardcoded File Name for specific editable File parsing
        // Value: Type of UI Editor that Edits that File
        public static readonly Dictionary<string, string> FileNameIds = ReadTxtDictionary("Libs.txt");

        // Dictionary that contains:
        // Key: Valid Editable File Extension
        // Value: Function that returns appropriate Editor for File Extension always using File Path as Parameter
        public static readonly Dictionary<string, Func<string, Type>> FileExtensions = ReadFuncDictionary("ExtType.txt");

        public static Dictionary<string, string> ReadTxtDictionary(string filePath)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            using (var sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    result.Add(line.Split(',')[0], line.Split(',')[1].Replace(" ", string.Empty));
                }
            }
            return result;
        }
        public static Dictionary<string, Func<string, Type>> ReadFuncDictionary(string filePath)
        {
            Dictionary<string, Func<string, Type>> result = new Dictionary<string, Func<string, Type>>();
            using (var sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    MethodInfo m = typeof(SupportedTypes).GetMethod(line.Split(',')[1].Replace(" ", string.Empty));
                    if ( m == null && !m.ReturnType.Equals(typeof(Type)))
                    {
                        continue;
                    }

                    Func<string, Type> func = (Func<string, Type>)Delegate.CreateDelegate(typeof(Func<string, Type>), m);

                    result.Add(line.Split(',')[0], func);
                }
            }
            return result;
        }

        public static Type Generic(string filePath)
        {
            try
            {
                foreach (var Identifier in FileNameIds)
                {
                    if (Path.GetFileName(filePath).Contains(Identifier.Key))
                    {
                        AppDomain.CurrentDomain.GetAssemblies();
                        return Type.GetType($"{Identifier.Value}.IPlugin, {Identifier.Value}");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }

            return null;
        }

        public static Type Package(string filePath)
        {
            return typeof(PakMan);
        }
    }
}
