using Microsoft.WindowsAPICodePack.Dialogs;
using SpikeSoft.DataTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SpikeSoft.FileManager
{
    public static class FileMan
    {
        /// <summary>
        /// Get Complete File Path from User File Search
        /// </summary>
        /// <param name="topText">Window Top Title</param>
        /// <param name="filterName">Filter Name Collection</param>
        /// <param name="filterExt">Filter Extension Collection</param>
        /// <returns></returns>
        public static string GetFilePath(string topText, List<string> filterName, List<string> filterExt)
        {
            // Build Search Filters
            StringBuilder sb = new StringBuilder();

            // Assign Default Title if not present
            if (string.IsNullOrEmpty(topText))
            {
                topText = "Please select an appropriate File";
            }

            // Assign Default Filter if not Present
            if (filterName.Count < 1 && filterExt.Count < 1)
            {
                filterName.Add("All Files");
                filterExt.Add("*.*");
            }
            // Match Filter Name and Extension Count
            else if (filterName.Count != filterExt.Count)
            {
                if (filterName.Count > filterExt.Count)
                {
                    while (filterName.Count > filterExt.Count)
                    {
                        filterExt.Add("*.*");
                    }
                }
                else if (filterExt.Count > filterName.Count)
                {
                    while (filterExt.Count > filterName.Count)
                    {
                        filterName.Add($"{filterExt[filterName.Count]}");
                    }
                }
            }

            // Fill Filters

            // Filter for All Supported Files by Extension List
            sb.Append($"All Supported Files|");

            for (var i = 0; i < filterExt.Count; i++)
            {
                sb.Append($"{filterExt[i]};");
            }

            // Filter for All Files
            sb.Append($"|All Files|*.*|");

            // Individual Extension Filters
            for (var i = 0; i < filterName.Count - 1; i++)
            {
                sb.Append($"{filterName[i]}|{filterExt[i]}|");
            }

            sb.Append($"{filterName.Last()}|{filterExt.Last()}");

            // Assign Params
            var FileSearch = new OpenFileDialog();
            FileSearch.Title = topText;
            FileSearch.Filter = sb.ToString();

            // User Search File
            if (FileSearch.ShowDialog() == DialogResult.OK)
            {
                return FileSearch.FileName;
            }

            // Return Empty if Canceled
            return string.Empty;
        }

        /// <summary>
        /// Get Complete Directory Path from User Directory Search
        /// </summary>
        /// <param name="topText">Special Title Text</param>
        /// <returns></returns>
        public static string GetDirectoryPath(string topText)
        {
            // Assign Default Title if not present
            if (string.IsNullOrEmpty(topText))
            {
                topText = "Please select a Directory";
            }

            // Assign Params
            var FolderSearch = new CommonOpenFileDialog();
            FolderSearch.Title = topText;

            // User Search File
            if (FolderSearch.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return FolderSearch.FileName;
            }

            // Return Empty if Canceled
            return string.Empty;
        }

        /// <summary>
        /// Save Temp File as New File at a new Location with the Default Extension specified if ommited.
        /// </summary>
        /// <param name="defaultName">File Default Name</param>
        /// <param name="defaultExtension">File Default Extension (if User ommited)</param>
        public static string SaveDefaultTmpFileAs(string defaultName, string defaultExtension)
        {
            // Assign Params
            var FileSave = new SaveFileDialog();
            FileSave.Title = "Please Select Directory and Filename of the New File";
            FileSave.Filter = "All Files|*.*";
            FileSave.FileName = defaultName;
            FileSave.AddExtension = true;
            FileSave.DefaultExt = defaultExtension; // No Period (ex: "txt")

            // User Save File
            if ((FileSave.ShowDialog() != DialogResult.OK) || (!SaveDefaultTmpFile(FileSave.FileName)))
            {
                return string.Empty;
            }

            return FileSave.FileName;
        }

        /// <summary>
        /// Copies Default Temporal File to Param File Path and returns if task completed successfully
        /// </summary>
        /// <param name="savePath">Destiny File Path to Copy File</param>
        public static bool SaveDefaultTmpFile(string savePath)
        {
            if (!ValidateFilePath(savePath))
            {
                ExceptionMan.ThrowMessage(0x1000); return false;
            }

            File.Copy(TmpMan.GetDefaultTmpFile(), savePath, true);
            return true;
        }

        /// <summary>
        /// Validates File Path
        /// </summary>
        /// <param name="path">Full Path to be validated</param>
        /// <returns></returns>
        public static bool ValidateFilePath(string path)
        {
            // Check for Empty Path
            if (string.IsNullOrEmpty(path)) { return false; }

            // Check for invalid file name Characters
            string regexString = "[" + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]";
            Regex containsABadCharacter = new Regex(regexString);
            if (containsABadCharacter.IsMatch(path))
            {
                return false;
            }

            // Check for invalid root drive
            string pathRoot = Path.GetPathRoot(path);
            if (!Directory.GetLogicalDrives().Contains(pathRoot))
            {
                return false;
            }

            // Final check if a File can exist on that directory
            FileInfo fi = null;

            try
            {
                fi = new FileInfo(path);
            }
            catch (ArgumentException) { }
            catch (PathTooLongException) { }
            catch (NotSupportedException) { }
            if (ReferenceEquals(fi, null))
            {
                return false;
            }

            return true;
        }
    }
}
