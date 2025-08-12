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

                    if (line.Split(',').Count() > 2)
                    {
                        continue;
                    }
                    else if (line.Split(',').Count() < 2)
                    {
                        result.Add(line.Trim(), Generic);
                        continue;
                    }

                    var parts = line.Split(',');

                    if ((parts.Length == 2) && (parts[1].Trim() != "Generic"))
                    {
                        string extension = parts[0].Trim();
                        string dllName = parts[1].Trim();

                        // Load the DLL and register the type
                        try
                        {
                            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "lib", dllName);
                            if (File.Exists(dllPath))
                            {
                                Assembly assembly = Assembly.LoadFrom(dllPath);
                                Type toolType = assembly.GetType(Path.GetFileNameWithoutExtension(dllName) + ".IPlugin");

                                if (toolType != null)
                                {
                                    Type customDelegate(string filepath) { return toolType; }
                                    result.Add(line.Split(',')[0], customDelegate);
                                }
                                else
                                {
                                    ExceptionMan.ThrowMessage(0x1002, new string[] { $"Type IPlugin in '{dllName}'." });
                                }
                            }
                            else
                            {
                                ExceptionMan.ThrowMessage(0x1002, new string[] { dllName });
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionMan.ThrowMessage(0x2000, new string[] { $"Failed to load plugin for '{extension}': {ex.Message}" });
                        }
                    }
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
                    var prop = pluginType.GetProperty("FileNamePatterns", BindingFlags.Public | BindingFlags.Instance);
                    if (prop == null)
                        continue;

                    var instance = Activator.CreateInstance(pluginType);
                    var patterns = (string[])prop.GetValue(instance);

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
