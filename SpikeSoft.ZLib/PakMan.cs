using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SpikeSoft.UtilityManager;
using System.Collections.Generic;
using SpikeSoft.UtilityManager.TaskProgress;

namespace SpikeSoft.ZLib
{
    public class PakMan : IFunType
    {
        public bool ShowProgressWindow = true;
        public BWorkWindow Worker;

        public async Task InitializeHandler(string filePath)
        {
            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<ProgressInfo>>(Work_Handler), new object[] { filePath }, !ShowProgressWindow);
        }

        public void Work_Handler(object[] args, IProgress<ProgressInfo> progress)
        {
            try
            {
                string filePath = args[0] as string;
                switch (Path.GetExtension(filePath))
                {
                    case ".idx":
                        // Create Repack Folder Handler.
                        Repack_Handler(filePath, progress);
                        return;
                    case ".zpak":
                        // Decompress BPE File to a Temporary File and replace "filePath" Variable with it.
                        if (progress != null)
                        {
                            progress.Report(new ProgressInfo { Value = 0, Message = "Decrypting File..." });
                        }

                        var BPEMan = new BPE();
                        TmpMan.SetNewAssociatedPath(filePath);
                        string tmpPath = TmpMan.GetTmpFilePath(filePath);
                        byte[] zfile = BPEMan.decompress(File.ReadAllBytes(filePath), progress);

                        if (string.IsNullOrEmpty(tmpPath))
                        {
                            ExceptionMan.ThrowMessage(0x2000, new string[] { "tmpPath is Empty!\nTemp File was not created" });
                            return;
                        }
                        if (zfile == null)
                        {
                            ExceptionMan.ThrowMessage(0x2000, new string[] { $"Decompression Error on File: {filePath}" });
                            return;
                        }

                        File.WriteAllBytes(tmpPath, zfile);

                        // Then Create PAK File Handler.
                        if (progress != null)
                        {
                            progress.Report(new ProgressInfo { Value = 0, Message = "Executing Package Work, Please Wait..." });
                        }

                        Unpack_Handler(typeof(Common.PAK), tmpPath, filePath, true, progress);

                        // Then Delete Temporary File Created.
                        TmpMan.CleanTmpFile(filePath);
                        break;
                    case ".pak":
                        // Create PAK File Handler.
                        Unpack_Handler(typeof(Common.PAK), filePath, filePath, false, progress);
                        break;
                    case ".pck":
                        // Identify Pck as EPCK file
                        if (!BinMan.GetBinaryData_String(File.ReadAllBytes(filePath), 0).Contains("EPCK"))
                        {
                            return;
                        }

                        // Create PCK File Handler.
                        Unpack_Handler(typeof(Common.PCK), filePath, filePath, false, progress);
                        break;
                }

                // If no File was Unpacked, don't do recursive Unpacking or Delete File
                if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath))))
                {
                    return;
                }

                if (SpikeSoft.UtilityManager.Properties.Settings.Default.UnpackDeleteFile == true)
                {
                    File.Delete(filePath);
                }

                if (SpikeSoft.UtilityManager.Properties.Settings.Default.UnpackComplete != true)
                {
                    return;
                }

                // Unpack sub Paks, Compressed Paks and PCKs
                RecursiveUnpacking(filePath, new string[] { "*.pak", "*.zpak", "*.pck" }, progress);
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x1000, new string[] { ex.Message });
            }
        }

        public void RecursiveUnpacking(string filePath, string[] args, IProgress<ProgressInfo> progress)
        {
            foreach (var arg in args)
            {
                foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), arg))
                {
                    Work_Handler(new object[] { subFile }, progress);
                }
            }
        }

        public void Unpack_Handler(Type T, string filePath, string originalPath, bool ZBPE, IProgress<ProgressInfo> progress)
        {
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), T) as Common.IPak);
            Package.Initialize(filePath, originalPath, ZBPE);
            Package.Unpack(progress);
        }

        public void Repack_Handler(string filePath, IProgress<ProgressInfo> progress)
        {
            // Recursively Check All Sub Folders for Paks to Repack
            foreach (var dir in Directory.EnumerateDirectories(Path.GetDirectoryName(filePath)))
            {
                foreach (var file in Directory.EnumerateFiles(dir, "*info.idx"))
                {
                    Repack_Handler(file, progress);
                }
            }

            var idxFile = new StreamReader(filePath);
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), Type.GetType("SpikeSoft.ZLib.Common." + idxFile.ReadLine())) as Common.IPak);
            Package.FileCount = int.Parse(idxFile.ReadLine());
            Package.ZBPE = bool.Parse(idxFile.ReadLine().ToLowerInvariant());

            // Fill File List
            List<string> FileList = new List<string>();
            while (!idxFile.EndOfStream)
            {
                FileList.Add(idxFile.ReadLine());
            }

            idxFile.Close();

            Package.FilePath = filePath;
            Package.FileNames = FileList;
            Package.Repack(progress);
        }
    }
}
