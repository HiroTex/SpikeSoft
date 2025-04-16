using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpikeSoft.DataTypes
{
    public static class SupportedTypes
    {
        // Dictionary that contains:
        // Key: Type of File
        // Value: File Extension Filter for Data Type
        public static readonly Dictionary<string, string> MainFilterList = ReadTxtDictionary("SupportedTypes.txt");

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

                    if (line.Split(',').Count() > 2) continue;

                    MethodInfo m = typeof(SupportedTypes).GetMethod(line.Split(',')[1].Replace(" ", string.Empty));
                    if (m == null && !m.ReturnType.Equals(typeof(Type)))
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
                string fileName = Path.GetFileName(filePath);

                var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

                foreach (var assembly in loadedAssemblies)
                {
                    // Look for type called "IPlugin" inheriting IEditor
                    var pluginType = assembly.GetTypes()
                        .FirstOrDefault(t => t.Name == "IPlugin" && typeof(IEditor).IsAssignableFrom(t));

                    if (pluginType == null)
                        continue;

                    // Check for static 'FileNamePatterns' property/field
                    var patternsField = pluginType.GetField("FileNamePatterns", BindingFlags.Public | BindingFlags.Static);
                    if (patternsField == null)
                        continue;

                    var patterns = (string[])patternsField.GetValue(null);

                    if (patterns != null && patterns.Any(pattern => fileName.Contains(pattern)))
                    {
                        return pluginType;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }

            return null;
        }
    }
}
