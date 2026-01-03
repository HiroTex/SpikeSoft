using SpikeSoft.UiUtils;
using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Utilities.Tools.Meteor
{
    public class BinaryPatcher : GenericToolMenuItem<BinaryPatcher>
    {
        public BinaryPatcher() : base("Patch ELF Binary") { }
        private List<string> TopTexts = new List<string>()
        {
            "Select YML Patch",
            "Select Elf",
            "Select DBZP.BIN"
        };
        private List<string> FilterNames = new List<string>()
        {
            "YML Files",
            "ELF Files",
            "Bin Files"
        };
        private List<string> FilterExts = new List<string>()
        {
            "*.yml",
            "*.78",
            "*.bin"
        };
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            var files = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                var TopText = TopTexts[i];
                var filters = new List<string>() { FilterNames[i] };
                var FilterExt = new List<string>() { FilterExts[i] };
                var result = FileMan.GetFilePath(TopText, filters, FilterExt);
                if (result == string.Empty)
                {
                    return;
                }
                files.Add(result);
            }
            if (files.Count() > 0) task(files);
        }

        public async void task(List<string> fileList)
        {
            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Patching Binary File, Please Wait", new Action<object[], IProgress<ProgressInfo>>(work), new object[] { fileList }, false);
        }

        public void work(object[] args, IProgress<ProgressInfo> progress)
        {
            var CodeList = new List<string>();
            var fileList = args[0] as List<string>;
            var pFile = fileList[0];
            using (var source = new StreamReader(pFile))
            {
                long len = source.BaseStream.Length;

                while (!source.EndOfStream)
                {
                    // Report Progress
                    progress?.Report(new ProgressInfo { Value = (int)((source.BaseStream.Position / (float)len) * 100), Message = "Collecting Patches..." });

                    // Start to Read Lines from Text to Start Parsing
                    var line = source.ReadLine();

                // Patch Name must have Semi Colon and Must not have a # in First Character
                ReadPatchName:
                    if (source.EndOfStream) break;
                    if (line.Length < 1) continue;
                    if (!line.Contains(":")) continue;
                    if (line[0] == '#') continue;

                    while (!source.EndOfStream)
                    {
                        // Code must not be commented
                        // Code must not be a Commented Patch Name
                        // Code must not be a Patch Name
                        // Code must have Code Brackets
                        line = source.ReadLine();
                        if (line.Length < 5) continue;
                        if (line.Contains("# - [")) continue;
                        if (line[0] == '#' && line.Contains(":")) goto ReadPatchName;
                        // strip comment tail fast
                        int hash = line.IndexOf('#');
                        if (hash >= 0)
                            line = line.Substring(0, hash);

                        if (line.IndexOf("- [") < 0)
                        {
                            if (line.IndexOf(':') >= 0)
                                goto ReadPatchName;

                            continue;
                        }

                        CodeList.Add(line.Trim());
                    }
                }
            }
            int total = CodeList.Count();
            int count = 0;
            using (FileStream ELFS = new FileStream(fileList[1], FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (FileStream BIFS = new FileStream(fileList[2], FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (BinaryWriter binELF = new BinaryWriter(ELFS))
            using (BinaryWriter binDZP = new BinaryWriter(BIFS))
            {
                foreach (var code in CodeList)
                {
                    progress?.Report(new ProgressInfo { Value = (int)((count++ / (float)total) * 100), Message = "Applying Patches..." });

                    try
                    {
                        int b1 = code.IndexOf('[') + 1;
                        int c1 = code.IndexOf(',', b1);
                        int c2 = code.IndexOf(',', c1 + 1);
                        int b2 = code.IndexOf(']', c2 + 1);

                        string sAddr = code.Substring(b1, c1 - b1).Trim();
                        string sVal = code.Substring(c1 + 1, c2 - c1 - 1).Trim();
                        string sLen = code.Substring(c2 + 1, b2 - c2 - 1).Trim();

                        uint address = Convert.ToUInt32(sAddr, 16);

                        byte[] patchBytes;
                        uint repeat;

                        // string patch
                        if (sVal.IndexOf('"') >= 0)
                        {
                            sVal = sVal.Replace("\"", "").Replace(" ", "");
                            patchBytes = Encoding.ASCII.GetBytes(sVal);
                            repeat = 1; // you already did this
                        }
                        else
                        {
                            uint v = Convert.ToUInt32(sVal, 16);
                            repeat = Convert.ToUInt32(sLen, 16);

                            // little-endian write (as before)
                            patchBytes = BitConverter.GetBytes(v);
                        }

                        if (address < 0x334C00)
                        {
                            long off = address - 0xFF000;
                            ELFS.Seek(off, SeekOrigin.Begin);

                            for (uint i = 0; i < repeat; i++)
                                binELF.Write(patchBytes);
                        }
                        else
                        {
                            long off = address - 0x334C00;
                            BIFS.Seek(off, SeekOrigin.Begin);

                            for (uint i = 0; i < repeat; i++)
                                binDZP.Write(patchBytes);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Problematic Code at Line: " + code);
                    }
                }
            }
        }
    }
}
