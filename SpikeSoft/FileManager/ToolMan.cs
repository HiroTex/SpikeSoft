using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SpikeSoft.FileManager
{
    public class ToolMan
    {
        private readonly Dictionary<string, Type> extensionToolMap = new Dictionary<string, Type>();

        /// <summary>
        /// Method to register a tool for a specific file extension
        /// </summary>
        /// <param name="extension">File Extension (including dot)</param>
        /// <param name="toolType">Fully qualified name of Class</param>
        public void RegisterTool(string extension, Type toolType)
        {
            if (!extensionToolMap.ContainsKey(extension))
            {
                extensionToolMap[extension.ToLower()] = toolType;
            }
        }

        /// <summary>
        /// Method to get the tool type for a given file extension
        /// </summary>
        /// <param name="extension">>File Extension (including dot)</param>
        /// <returns></returns>
        public Type GetToolForExtension(string extension)
        {
            Type toolType = null;
            extensionToolMap.TryGetValue(extension.ToLower(), out toolType);
            return toolType;
        }

        /// <summary>
        /// Method to load plugins from a configuration file
        /// </summary>
        /// <param name="configFilePath">Path to config txt file</param>
        public void LoadPlugins(string configFilePath)
        {
            if (!FileMan.ValidateFilePath(configFilePath))
            {
                ExceptionMan.ThrowMessage(0x1002, new string[] { configFilePath });
                return;
            }

            foreach (var line in File.ReadLines(configFilePath))
            {
                var parts = line.Split(',');

                if (parts.Length == 3)
                {
                    string extension = parts[0].Trim();
                    string dllName = parts[1].Trim();
                    string typeName = parts[2].Trim();

                    // Load the DLL and register the type
                    try
                    {
                        string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "lib", dllName);
                        if (File.Exists(dllPath))
                        {
                            Assembly assembly = Assembly.LoadFrom(dllPath);
                            Type toolType = assembly.GetType(typeName);

                            if (toolType != null)
                            {
                                RegisterTool(extension, toolType);
                            }
                            else
                            {
                                ExceptionMan.ThrowMessage(0x1002, new string[] { $"Type '{typeName}' in '{dllName}'." });
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
    }
}
