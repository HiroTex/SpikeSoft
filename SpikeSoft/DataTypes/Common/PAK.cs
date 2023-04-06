using SpikeSoft.FileManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes.Common
{
    class PAK : IPak
    {
        public string FilePath { get; set; }
        public string UnpackedDir { get; set; }
        public int FileCount { get; set; }
        public List<int> FilePointers { get; set; }
        public List<string> FileNames { get; set; }
        public int EOF { get; set; }
        public bool ZBPE { get; set; }

        public virtual bool Unpack(IProgress<int> progress)
        {
            #region Exceptions
            // Check for Invalid File Path
            if (!FileManager.FileMan.ValidateFilePath(FilePath))
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Unspecified or Invalid File Path" });
                return false;
            }

            // Create Directory for Unpacked Files
            Directory.CreateDirectory(UnpackedDir);

            // Check if Directory was Created to validate Read Write Access.
            if (!Directory.Exists(UnpackedDir))
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Unable To Create Directory for Unpacked File" });
                return false;
            }

            // Check File Count to avoid null pak files or invalid console mode.
            if (FileCount < 1)
            {
                ExceptionMan.ThrowMessage(0x2001, new string[] { "File Count is Negative" });
                return false;
            }

            // Check Matching File Count and Pointers to avoid mismatching.
            if (FileCount != FilePointers.Count)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Unmatching File Count and Pointers" });
                return false;
            }
            #endregion

            // Start File Unpacking.
            // Loop through all the File Pointers to Extract Each Byte Array into a Separate File.
            for (int i = 0; i < FileCount; i++)
            {
                if (progress != null)
                {
                    progress.Report((int)(((i + 1) / (float)FileCount) * 100));
                }

                int fSize;

                // If File is Last File, use End of File to Determine Size.
                if ((i + 1) == FilePointers.Count)
                {
                    fSize = EOF - FilePointers.Last();
                }
                else
                {
                    fSize = FilePointers[i + 1] - FilePointers[i];
                }

                // Check Invalid File Size
                if (fSize < 0)
                {
                    ExceptionMan.ThrowMessage(0x2001, new string[] { $"fSize of File is Invalid: {fSize}" });
                    return false;
                }

                // Skip Empty Files
                if (fSize == 0)
                {
                    continue;
                }

                byte[] tmp = SpikeSoft.FileManager.BinMan.GetBytes(FilePath, fSize, FilePointers[i]);
                File.WriteAllBytes(Path.Combine(UnpackedDir, FileNames[i]), tmp);
            }

            // Finally Write Package Information to be able to Repack the Folder into a File.
            WriteInfo(UnpackedDir, this.GetType().Name);

            return true;
        }

        public virtual bool Repack(IProgress<int> progress)
        {
            #region Exceptions
            // Check for Invalid File Path
            if (!FileManager.FileMan.ValidateFilePath(FilePath))
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Unspecified or Invalid File Path" });
                return false;
            }

            // Check File Count to avoid null pak files or invalid console mode.
            if (FileCount < 1)
            {
                ExceptionMan.ThrowMessage(0x2001, new string[] { "File Count is Negative" });
                return false;
            }
            #endregion

            string NewFile_NAME = Path.GetFileName(Path.GetDirectoryName(FilePath));
            string NewFile_PATH = Path.GetDirectoryName(Path.GetDirectoryName(FilePath));
            NewFile_PATH = Path.Combine(NewFile_PATH, NewFile_NAME + ".pak");
            TmpMan.SetNewAssociatedPath(NewFile_PATH);
            string tmpPath = FileManager.TmpMan.GetTmpFilePath(NewFile_PATH);

            using (var newPak = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
            {
                BinaryWriter binMan = new BinaryWriter(newPak);
                byte[] FileCountArray = BitConverter.GetBytes(FileCount);
                if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileCountArray);
                binMan.Write(FileCountArray); // Write File Count
                uint HeaderEnd = (uint)((FileCount * 4) + 8);
                if (HeaderEnd % 64 != 0) { HeaderEnd = HeaderEnd - (HeaderEnd % 64) + 64; };
                byte[] FileOffset = BitConverter.GetBytes(HeaderEnd);
                if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileOffset);
                binMan.Write(FileOffset); // Write First File Offset Always as End of Header
                binMan.BaseStream.SetLength(HeaderEnd);

                // Iterate through all files that will be packed
                uint Current_FID = 0;
                uint New_FID = 0;
                foreach (var file in Directory.EnumerateFiles(Path.GetDirectoryName(FilePath)))
                {
                    if (progress != null)
                    {
                        progress.Report((int)(((Current_FID + 1) / (float)FileCount) * 100));
                    }

                    // Skip Files without ID
                    if (Path.GetFileNameWithoutExtension(file).Split('_').Length < 2) continue;

                    // Get Sub File ID
                    try
                    {
                        New_FID = uint.Parse(Path.GetFileNameWithoutExtension(file).Split('_')[0]);
                    }
                    catch (FormatException)
                    {
                        ExceptionMan.ThrowMessage(0x2000, new string[] { $"Invalid File Name: {file}" });
                        continue;
                    }

                    // If ID is above File Count, break iteration (Only numbers above that one after this ID).
                    if (New_FID > FileCount) break;

                    New_FID -= 1;

                    // If Negative ID, skip to avoid Header Corruption
                    if (New_FID < 0) continue;

                    // If ID skipped Sub Files ID in between, Fill Header with Dummy Info
                    if (New_FID > Current_FID + 1)
                    {
                        uint EmptyFiles = New_FID - Current_FID;
                        for (int i = 0; i <= EmptyFiles; i++)
                        {
                            uint CurrentFileLength = (uint)binMan.BaseStream.Length;
                            binMan.BaseStream.Seek(0x4 + (Current_FID * 4) + (4 * i) + 0x4, SeekOrigin.Begin);

                            FileOffset = BitConverter.GetBytes(CurrentFileLength);
                            if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileOffset);
                            binMan.Write(FileOffset); // Write Current File Length for Empty Dummy Files
                        }
                    }

                    using (var SubFile = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        binMan.BaseStream.Seek(0, SeekOrigin.End);
                        SubFile.CopyTo(newPak);
                        binMan.BaseStream.Seek(0x4 + (New_FID * 4) + 0x4, SeekOrigin.Begin);
                        FileOffset = BitConverter.GetBytes(binMan.BaseStream.Length);
                        if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileOffset);
                        binMan.Write(FileOffset); // Write Current File Length for Empty Dummy Files
                    }

                    Current_FID = New_FID;
                }

                // If ID skipped Sub Files ID in between, Fill Header with Dummy Info
                if (FileCount > Current_FID + 1)
                {
                    uint EmptyFiles = (uint)(FileCount - Current_FID);
                    for (int i = 0; i <= EmptyFiles; i++)
                    {
                        uint CurrentFileLength = (uint)binMan.BaseStream.Length;
                        binMan.BaseStream.Seek(0x4 + (Current_FID * 4) + (4 * i) + 0x4, SeekOrigin.Begin);

                        FileOffset = BitConverter.GetBytes(CurrentFileLength);
                        if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileOffset);
                        binMan.Write(FileOffset); // Write Current File Length for Empty Dummy Files
                    }
                }

                binMan.BaseStream.Seek(0x4 + (FileCount * 4), SeekOrigin.Begin);
                FileOffset = BitConverter.GetBytes(binMan.BaseStream.Length);
                if (Properties.Settings.Default.WIIMODE) Array.Reverse(FileOffset);
                binMan.Write(FileOffset); // Write Current File Length for Empty Dummy Files
            }

            if (string.IsNullOrEmpty(tmpPath))
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "tmpPath is Empty!\nTemp File was not created" });
                return false;
            }

            if (!ZBPE)
            {
                File.Copy(tmpPath, NewFile_PATH, true);
                TmpMan.CleanTmpFile(NewFile_PATH);
                return true;
            }
            BPE BPEMan = new BPE();
            byte[] CompressedFile = BPEMan.compress(File.ReadAllBytes(tmpPath));
            File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(NewFile_PATH), NewFile_NAME + ".zpak"), CompressedFile);
            TmpMan.CleanTmpFile(NewFile_PATH);
            return true;
        }

        public virtual void WriteInfo(string dir, string type)
        {
            var idx = new StreamWriter(Path.Combine(dir, "info.idx"));
            idx.WriteLine(type); // Pak Type
            idx.WriteLine(FileCount); // Total File Count
            idx.WriteLine(ZBPE); // File Needs to be Re-Compressed
            idx.Flush();
            idx.Close();
        }

        public virtual void InitializeSubFileCount(string filePath)
        {
            FileCount = SpikeSoft.FileManager.BinMan.GetBinaryData_Int32(filePath, 0);
        }

        public virtual void InitializeFilePointersList(string filePath)
        {
            FilePointers = new List<int>();
            int total = FileManager.BinMan.GetBinaryData_Int32(filePath, 0);
            for (int i = 0; i < total; i++)
            {
                FilePointers.Add(FileManager.BinMan.GetBinaryData_Int32(filePath, i * 4 + 4));
            }
        }

        public virtual void InitializeEndOfFilePointer(string filePath)
        {
            int total = FileManager.BinMan.GetBinaryData_Int32(filePath, 0);
            EOF = FileManager.BinMan.GetBinaryData_Int32(filePath, total * 4 + 4);
        }

        public virtual void InitializeFilenamesList(string filePath, int subFileCount)
        {
            // Initialize List to Store Matches
            var MatchList = new List<int>();
            var linePos = 0;

            // Set Base PakList File to Initialize Search
            var txt_path = Path.Combine(Properties.Settings.Default.CommonTXTPath, Properties.Settings.Default.CommonGAMEPath, "paklist.txt");

            using (var sr = new StreamReader(txt_path))
            {
                // First Step, Find All File Name Matching Pak Lists.
                do
                {
                    // If Line is not Identifier Line, Skip.
                    if (sr.Peek() != '#')
                    {
                        sr.ReadLine();
                        linePos++;
                        continue;
                    }

                    // Read ID Line.
                    // Format:
                    // [00] # Identifier
                    // [01] Pak File Name
                    // [02] Max File Count
                    // [03] Extra File Name Identifier Words

                    var id = sr.ReadLine().Split(' ');
                    linePos++;

                    // If File name does not contain identifier word, and Idenfitier Word is not Wild Card, Skip File Name Lines.
                    if (!Path.GetFileName(filePath).ToLowerInvariant().Contains(id[1].ToLowerInvariant()) && id[1] != "*")
                    {
                        for (int i = 0; i < int.Parse(id[2]); i++)
                        {
                            sr.ReadLine();
                            linePos++;
                        }

                        continue;
                    }

                    // If Matching File Name Found, Save Position to Matching List
                    MatchList.Add(linePos);

                } while (!sr.EndOfStream);

                // Search if any Match has the same File Count as Source File
                foreach (var match in MatchList)
                {
                    // Set Stream Position to Matching File Name to Read the ID for File Count checking.
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    for(int i = 0; i < match - 1; i++)
                    {
                        sr.ReadLine();
                    }

                    var id = sr.ReadLine().Split(' ');

                    // If Sub File Count does not match, skip Matching Result.
                    if (int.Parse(id[2]) != subFileCount)
                    {
                        continue;
                    }

                    // Return the Matching File List
                    var Result = new List<string>();
                    for (int i = 0; i < subFileCount; i++)
                    {
                        Result.Add(sr.ReadLine());
                    }

                    FileNames = Result;
                    return;
                }

                // Search if any Match has a File Count above the Max File Count of the Source.
                foreach (var match in MatchList)
                {
                    // Set Stream Position to Matching File Name to Read the ID for File Count checking.
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    for (int i = 0; i < match - 1; i++)
                    {
                        sr.ReadLine();
                    }

                    var id = sr.ReadLine().Split(' ');

                    // If Sub File Count is above list max count, skip Matching Result.
                    if (int.Parse(id[2]) <= subFileCount)
                    {
                        continue;
                    }

                    // Check for Extra File Name Words Identifiers
                    if (id.Length > 3)
                    {
                        var extra = id[3].Split('|');
                        foreach (var identifier in extra)
                        {
                            if (!Path.GetFileName(filePath).Contains(identifier))
                            {
                                continue;
                            }

                            goto MatchFound;
                        }

                        continue;
                    }

                    // Return the Matching File List
                    MatchFound:
                    var Result = new List<string>();
                    for (int i = 0; i < int.Parse(id[2]); i++)
                    {
                        Result.Add(sr.ReadLine());
                    }

                    FileNames = Result;
                    return;
                }

                // If no match was found, return a Default File Name List with partially identified extensions
                FileNames = GenerateDefaultFilenamesList(filePath, subFileCount);
            }
        }

        public virtual List<string> GenerateDefaultFilenamesList(string filePath, int subFileCount)
        {
            var result = new List<string>();
            string format = "D2";

            if (subFileCount.ToString().Length > 2)
                format = "D" + subFileCount.ToString().Length;

            // Sub File Analysis

            for (var i = 0; i < subFileCount; i++)
            {
                // Get File Offsets to get File Size
                int fOffset = SpikeSoft.FileManager.BinMan.GetBinaryData_Int32(filePath, i * 4 + 4);
                int NextOffset = SpikeSoft.FileManager.BinMan.GetBinaryData_Int32(filePath, i * 4 + 8);

                // File is Empty
                if ((NextOffset - fOffset) == 0)
                {
                    result.Add((i + 1).ToString(format) + "_dummy");
                    continue;
                }

                // Get File to Array and Analyse it to get FileName and Extension
                result.Add((i + 1).ToString(format) + "_" + FileManager.AnalysisMan.RunFileAnalysis(FileManager.BinMan.GetBytes(filePath, NextOffset - fOffset, fOffset)));
            }

            return result;
        }
    }
}
