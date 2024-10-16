using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SpikeSoft.UtilityManager;
using SpikeSoft.ZLib;
using SpikeSoft.UtilityManager.TaskProgress;

namespace SpikeSoft.DataTypes.Common
{
    public class PAK : IPak
    {
        public string FilePath { get; set; }
        public string UnpackedDir { get; set; }
        public int FileCount { get; set; }
        public List<int> FilePointers { get; set; }
        public List<string> FileNames { get; set; }
        public int EOF { get; set; }
        public bool ZBPE { get; set; }

        public virtual void Initialize(string filePath, string originalPath, bool ZBPE)
        {
            FilePath = filePath;
            UnpackedDir = Path.Combine(Path.GetDirectoryName(originalPath), Path.GetFileNameWithoutExtension(originalPath));
            this.ZBPE = ZBPE;
            InitializeSubFileCount(filePath);
            InitializeFilePointersList(filePath);
            InitializeEndOfFilePointer(filePath);
            InitializeFilenamesList(originalPath, FileCount);
        }

        public virtual bool Unpack(IProgress<ProgressInfo> progress)
        {
            #region Exceptions
            // Check for Invalid File Path
            if (!FileMan.ValidateFilePath(FilePath))
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

            // Check Enough File Names for Files
            if (FileCount > FileNames.Count)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Not Enough File Names for this File" });
                return false;
            }
            #endregion

            // Start File Unpacking.
            // Loop through all the File Pointers to Extract Each Byte Array into a Separate File.
            for (int i = 0; i < FileCount; i++)
            {
                if (progress != null)
                {
                    int v = (int)(((i + 1) / (float)FileCount) * 100);
                    progress.Report(new ProgressInfo { Value = v });
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

                byte[] tmp = BinMan.GetBytes(FilePath, fSize, FilePointers[i]);
                FileInfo exFile = new FileInfo(Path.Combine(UnpackedDir, FileNames[i]));
                exFile.Directory.Create();
                File.WriteAllBytes(exFile.FullName, tmp);
            }

            // Finally Write Package Information to be able to Repack the Folder into a File.
            WriteInfo(UnpackedDir, this.GetType().Name);

            return true;
        }

        public virtual bool Repack(IProgress<ProgressInfo> progress)
        {
            #region Exceptions
            // Check for Invalid File Path
            if (!FileMan.ValidateFilePath(FilePath))
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
            string tmpPath = TmpMan.GetTmpFilePath(NewFile_PATH);

            using (var newPak = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
            using (var newBw = new BinaryWriter(newPak))
            using (var newBr = new BinaryReader(newPak))
            {
                uint hd_length = (uint)(FileCount * 4) + 8;
                hd_length = ((hd_length % 64) == 0) ? hd_length : (hd_length + (64 - (hd_length % 64)));

                // Write Total File Count
                byte[] fcount = BitConverter.GetBytes(FileCount);
                if (UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(fcount);
                newBw.Write(fcount); // Write File Count

                // Write First File Offset (Same as Header Size)
                byte[] hsize = BitConverter.GetBytes(hd_length);
                if (UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(hsize);
                newBw.Write(hsize);

                // Write rest of header
                newBw.Write(new byte[hd_length - 8]);

                List<uint> fSizes = new List<uint>();
                int Current_FID = 0;

                // Report Progress to Progress Bar
                if (progress != null)
                {
                    string label = "Packaging Files...";
                    progress.Report(new ProgressInfo { Value = 0, Message = label });
                }

                foreach (var file in FileNames)
                {
                    // If ID is above File Count, break iteration (Only numbers above that one after this ID).
                    if (Current_FID > FileCount)
                    {
                        break;
                    }

                    // Report Progress to Progress Bar
                    if (progress != null)
                    {
                        int v = (int)(((Current_FID++) / (float)FileCount) * 100);
                        progress.Report(new ProgressInfo { Value = v });
                    }

                    // Get if File Exists
                    string fPath = Path.Combine(Path.GetDirectoryName(FilePath), file);

                    if (!new FileInfo(fPath).Exists)
                    {
                        fSizes.Add(0); // If file doesn't exists, set empty header
                        continue;
                    }

                    using (var subFile = new FileStream(fPath, FileMode.Open, FileAccess.Read))
                    {
                        uint sub_length = (uint)subFile.Length;

                        subFile.CopyTo(newPak);

                        if ((sub_length % 16) != 0)
                        {
                            newBw.Write(new byte[(16 - (sub_length % 16))]);
                            sub_length += (16 - (sub_length % 16));
                        }

                        fSizes.Add(sub_length);
                    }
                }

                // Report Progress to Progress Bar
                if (progress != null)
                {
                    string label = "Creating Header...";
                    progress.Report(new ProgressInfo { Value = 0, Message = label });
                }

                for (int i = 0; i < fSizes.Count; i++)
                {
                    // Report Progress to Progress Bar
                    if (progress != null)
                    {
                        int v = (int)(((i) / (float)fSizes.Count) * 100);
                        progress.Report(new ProgressInfo { Value = v });
                    }

                    newPak.Seek(0x4 + (i * 4), SeekOrigin.Begin);
                    byte[] fStart = newBr.ReadBytes(4);
                    if (UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(fStart);
                    uint startOffset = BitConverter.ToUInt32(fStart, 0);
                    uint endOffset = (startOffset += fSizes[i]);
                    byte[] fEnd = BitConverter.GetBytes(endOffset);
                    if (UtilityManager.Properties.Settings.Default.WIIMODE) Array.Reverse(fEnd);
                    newBw.Write(fEnd);
                }
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

            // Report Progress to Progress Bar
            if (progress != null)
            {
                string label = "Encrypting File...";
                progress.Report(new ProgressInfo { Value = 0, Message = label });
            }

            var BPEMan = new BPE();
            byte[] CompressedFile = BPEMan.compress(File.ReadAllBytes(tmpPath), progress);
            File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(NewFile_PATH), NewFile_NAME + ".zpak"), CompressedFile);
            TmpMan.CleanTmpFile(NewFile_PATH);
            return true;
        }

        public virtual void WriteInfo(string dir, string type)
        {
            var idx = new StreamWriter(Path.Combine(dir, "#info.idx"));
            idx.WriteLine(type); // Pak Type
            idx.WriteLine(FileCount); // Total File Count
            idx.WriteLine(ZBPE); // File Needs to be Re-Compressed
            foreach (var name in FileNames) idx.WriteLine(name);
            idx.Flush();
            idx.Close();
        }

        public virtual void InitializeSubFileCount(string filePath)
        {
            FileCount = BinMan.GetBinaryData<int>(filePath, 0);
        }

        public virtual void InitializeFilePointersList(string filePath)
        {
            FilePointers = new List<int>();
            int total = BinMan.GetBinaryData<int>(filePath, 0);
            for (int i = 0; i < total; i++)
            {
                FilePointers.Add(BinMan.GetBinaryData<int>(filePath, i * 4 + 4));
            }
        }

        public virtual void InitializeEndOfFilePointer(string filePath)
        {
            int total = BinMan.GetBinaryData<int>(filePath, 0);
            EOF = BinMan.GetBinaryData<int>(filePath, total * 4 + 4);
        }

        public virtual void InitializeFilenamesList(string filePath, int subFileCount)
        {
            // Initialize List to Store Matches
            var MatchList = new List<int>();
            var linePos = 0;

            // Set Base PakList File to Initialize Search
            var txt_path = Path.Combine(UtilityManager.Properties.Settings.Default.CommonTXTPath, UtilityManager.Properties.Settings.Default.CommonGAMEPath, "paklist.txt");

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
                
                // Find Matches and Create File Name List
                FileNames = FindFileListMatch(filePath, subFileCount, sr, MatchList);

                if (FileNames != null) return;

                // If no match was found, return a Default File Name List with partially identified extensions
                FileNames = GenerateDefaultFilenamesList(filePath, subFileCount);
            }
        }

        private List<string> FindFileListMatch(string filePath, int subFileCount, StreamReader sr, List<int> MatchList)
        {
            string[] id;

            List<string> fNameMatch = new List<string>();

            foreach (var match in MatchList)
            {
                // Set Stream Position to Matching File Name to Read the File Name Filter.
                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < match - 1; i++)
                {
                    sr.ReadLine();
                }

                id = sr.ReadLine().Split(' ');

                fNameMatch.Add(id[1]);
            }

            // Process Matching List to get only the ones with the most similar filenames
            string pakFname = Path.GetFileNameWithoutExtension(filePath);
            var newList = new List<int>();
            for (int i = 0; i < MatchList.Count; i++)
            {
                if (fNameMatch[i] == "*" ||
                    fNameMatch[i] == FileManager.AnalysisMan.GetMostSimilarString(pakFname, fNameMatch.ToArray()))
                {
                    newList.Add(MatchList[i]);
                }
            }

            MatchList = newList;

            // Search if any Match has the same File Count as Source File
            foreach (var match in MatchList)
            {
                // Set Stream Position to Matching File Name to Read the ID for File Count checking.
                sr.DiscardBufferedData();
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < match - 1; i++)
                {
                    sr.ReadLine();
                }

                id = sr.ReadLine().Split(' ');

                // If Sub File Count does not match, skip Matching Result.
                if (int.Parse(id[2]) != subFileCount)
                {
                    continue;
                }

                return FillNameList(sr, id);
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

                id = sr.ReadLine().Split(' ');

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
                return FillNameList(sr, id);
            }

            return null;
        }

        private List<string> FillNameList(StreamReader sr, string[] id)
        {
            var Result = new List<string>();
            for (int i = 0; i < int.Parse(id[2]); i++)
            {
                Result.Add(sr.ReadLine());
            }

            ReplaceDynamicNames(Result);

            return Result;
        }

        private List<string> ReplaceDynamicNames(List<string> input)
        {
            var fName = Path.GetFileNameWithoutExtension(FilePath);

            // Replace Dynamic Names
            for (int i = 0; i < input.Count; i++)
            {
                input[i] = input[i].Replace("{fName}", fName);
            }

            return input;
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
                int fOffset = BinMan.GetBinaryData<int>(filePath, i * 4 + 4);
                int NextOffset = BinMan.GetBinaryData<int>(filePath, i * 4 + 8);

                // File is Empty
                if ((NextOffset - fOffset) == 0)
                {
                    result.Add((i + 1).ToString(format) + "_dummy");
                    continue;
                }

                // Get File to Array and Analyse it to get FileName and Extension
                result.Add((i + 1).ToString(format) + "_" + FileManager.AnalysisMan.RunFileAnalysis(BinMan.GetBytes(filePath, NextOffset - fOffset, fOffset)));
            }

            return result;
        }
    }
}
