using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    public static class TmpMan
    {
        // Key: Original File Path 
        // Value: Temp File Path
        private static Dictionary<string, string> TmpFilePaths;

        /// <summary>
        /// Sets Current Working File Path and Creates Temp File
        /// </summary>
        /// <param name="filePath">Complete File Path to Current Working File</param>
        public static void InitializeMainTmpFile(string filePath)
        {
            // Skip if a Temp file has already been created for this file
            if (VerifyExistentTmp(filePath))
            {
                return;
            }

            // Reset Links
            TmpFilePaths = new Dictionary<string, string>();

            // Set new Default Working File
            SetNewAssociatedPath(filePath);
        }

        public static void SetNewAssociatedPath(string filePath)
        {
            if (VerifyExistentTmp(filePath))
            {
                return;
            }

            string tmpPath = Path.GetTempFileName();

            try
            {
                if (!ValidateTmpNull(0))
                {
                    // Initialize TmpFilePaths If Tmp Paths has not been Initialized
                    TmpFilePaths = new Dictionary<string, string>();
                }

                TmpFilePaths.Add(filePath, tmpPath);
                if (!File.Exists(filePath)) return;
                File.Copy(filePath, tmpPath, true);
            }
            catch (ArgumentException)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { $"Temp File was not cleaned!\nFile: {filePath}" });
            }
        }

        public static void CleanAllTmpFiles()
        {
            try
            {
                if (!ValidateTmpNull(0))
                {
                    // No Tmp file was Initialized or all are already erased
                    return;
                }

                string[] TmpPaths = TmpFilePaths.Values.ToArray();
                foreach (var p in TmpPaths)
                {
                    string key = TmpFilePaths.FirstOrDefault(x => x.Value == p).Key;
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    CleanTmpFile(key);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // User does not have Access to erase any file on Temp Folder
                return;
            }
        }

        public static void CleanTmpFile(string filePath)
        {
            if (!ValidateTmpNull(0) || !TmpFilePaths.Keys.Contains(filePath))
            {
                // No Tmp file was Initialized or all are already erased, or there is no Tmp Path that matches 
                return;
            }

            string tmpPath = TmpFilePaths[filePath];

            if (!File.Exists(tmpPath))
            {
                // File has already been Cleaned, erase Item in Directory
                goto CleanDirectoryItem;
            }

            try
            {
                File.Delete(tmpPath);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"User does not have rights to Delete Files on Temp Folder\nSystem Could Not Erase File: {Path.GetFileName(tmpPath)}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new UnauthorizedAccessException();
            }
            catch (IOException)
            {
                MessageBox.Show($"Temp File is Still in Use\nSystem Could Not Erase File: {Path.GetFileName(tmpPath)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CleanDirectoryItem:
            TmpFilePaths.Remove(filePath);
        }

        public static string GetDefaultTmpFile()
        {
            return GetTmpFile(0);
        }

        public static string GetDefaultWrkFile()
        {
            return GetWrkFile(0);
        }

        public static string GetTmpFile(int n)
        {
            if (!ValidateTmpNull(n))
            {
                // Tmp File List is Empty
                return "";
            }

            return TmpFilePaths.Values.ElementAt(n);
        }

        public static string GetWrkFile(int n)
        {
            if (!ValidateTmpNull(n))
            {
                // Tmp File List is Empty
                return "";
            }

            return TmpFilePaths.Keys.ElementAt(n);
        }

        public static string GetTmpFilePath(string filePath)
        {
            if (!ValidateTmpNull(0) || !TmpFilePaths.Keys.Contains(filePath))
            {
                // No Tmp file was Initialized, or there is no Tmp Path that matches 
                return "";
            }

            string tmpPath = TmpFilePaths[filePath];

            return tmpPath;
        }

        public static int GetTmpPathCount()
        {
            return TmpFilePaths.Values.Count;
        }

        private static bool ValidateTmpNull(int n)
        {
            if (TmpFilePaths == null || TmpFilePaths.Count == 0 || n >= TmpFilePaths.Count)
            {
                return false;
            }
            return true;
        }

        private static bool VerifyExistentTmp(string filePath)
        {
            if (ValidateTmpNull(0))
            {
                if (TmpFilePaths.Keys.Contains(filePath))
                {
                    // filePath already been created
                    return true;
                }
            }

            return false;
        }
    }
}
