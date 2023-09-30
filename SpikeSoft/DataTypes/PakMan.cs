using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SpikeSoft.UtilityManager;
using SpikeSoft.ZLib;

namespace SpikeSoft.DataTypes
{
    public class PakMan : IFunType
    {
        public bool ShowProgressWindow = true;
        public GUI.BWorkWindow Worker;

        public async Task InitializeHandler(string filePath)
        {
            FileManager.FunMan FUN = new FileManager.FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<int>>(Work_Handler), new object[] { filePath }, !ShowProgressWindow);
        }

        public void Work_Handler(object[] args, IProgress<int> progress)
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
                    var BPEMan = new BPE();
                    TmpMan.SetNewAssociatedPath(filePath);
                    string tmpPath = TmpMan.GetTmpFilePath(filePath);
                    byte[] zfile = BPEMan.decompress(File.ReadAllBytes(filePath));

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

        public void RecursiveUnpacking(string filePath, string[] args, IProgress<int> progress)
        {
            foreach (var arg in args)
            {
                foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), arg))
                {
                    Work_Handler(new object[] { subFile }, progress);
                }
            }
        }

        public void Unpack_Handler(Type T, string filePath, string originalPath, bool ZBPE, IProgress<int> progress)
        {
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), T) as Common.IPak);
            Package.Initialize(filePath, originalPath, ZBPE);
            Package.Unpack(progress);
        }

        public void Repack_Handler(string filePath, IProgress<int> progress)
        {
            // Recursively Check All Sub Folders for Paks to Repack
            foreach (var dir in Directory.EnumerateDirectories(Path.GetDirectoryName(filePath)))
            {
                foreach (var file in Directory.EnumerateFiles(dir, "info.idx"))
                {
                    Repack_Handler(file, progress);
                }
            }

            var idxFile = new StreamReader(filePath);
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), Type.GetType("SpikeSoft.DataTypes.Common." + idxFile.ReadLine())) as Common.IPak);
            Package.FileCount = int.Parse(idxFile.ReadLine());
            Package.ZBPE = bool.Parse(idxFile.ReadLine().ToLowerInvariant());
            idxFile.Close();

            Package.FilePath = filePath;
            Package.Repack(progress);
        }
    }
}
