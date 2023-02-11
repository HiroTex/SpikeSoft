using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SpikeSoft.FileManager
{
    public static class TmpMan
    {
        // Key: Temp File Path
        // Value: Original File Path
        private static Dictionary<string, string> TmpFilePaths;

        /// <summary>
        /// Sets Current Working File Path and Creates Temp File
        /// </summary>
        /// <param name="filePath">Complete File Path to Current Working File</param>
        public static void InitializeMainTmpFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            TmpFilePaths = new Dictionary<string, string>();
            TmpFilePaths.Add(Path.Combine(Path.GetTempPath(), "Temp.ss"), filePath);
            File.Copy(filePath, GetDefaultTmpFile(), true);
        }

        public static void SetNewAssociatedPath(string filePath)
        {
            if (!ValidateTmpNull(0))
            {
                // If Default Tmp Path has not been Initialized, set new Associated Path to Default Temp File
                InitializeMainTmpFile(filePath);
            }

            TmpFilePaths.Add(Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(filePath) + ".tmp"), filePath);
            File.Copy(filePath, Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(filePath) + ".tmp"), true);
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

                string[] TmpPaths = TmpFilePaths.Keys.ToArray();
                foreach (var p in TmpPaths)
                {
                    CleanTmpFile(Path.GetFileNameWithoutExtension(p));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // User does not have Access to erase any file on Temp Folder
                return;
            }
        }

        public static void CleanTmpFile(string fileName)
        {
            string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName) + ".tmp");

            if (!ValidateTmpNull(0) || !TmpFilePaths.Keys.Contains(tmpPath))
            {
                // No Tmp file was Initialized or all are already erased, or there is no Tmp Path that matches 
                return;
            }

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
            TmpFilePaths.Remove(tmpPath);
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

            return TmpFilePaths.Keys.ElementAt(n);
        }

        public static string GetWrkFile(int n)
        {
            if (!ValidateTmpNull(n))
            {
                // Tmp File List is Empty
                return "";
            }

            return TmpFilePaths.Values.ElementAt(n);
        }

        public static string GetTmpFilePath(string fileName)
        {
            string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName) + ".tmp");

            if (!ValidateTmpNull(0) || !TmpFilePaths.Keys.Contains(tmpPath))
            {
                // No Tmp file was Initialized, or there is no Tmp Path that matches 
                return "";
            }

            return tmpPath;
        }

        private static bool ValidateTmpNull(int n)
        {
            if (TmpFilePaths == null || TmpFilePaths.Count == 0 || n >= TmpFilePaths.Count)
            {
                return false;
            }
            return true;
        }
    }
}
