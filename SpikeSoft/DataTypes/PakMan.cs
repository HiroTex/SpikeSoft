using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes
{
    public class PakMan : IFunType
    {
        public void InitializeHandler(string filePath)
        {
            switch (Path.GetExtension(filePath))
            {
                case ".idx":
                    // Recursively Check All Sub Folders for Paks to Repack
                    foreach (var dir in Directory.EnumerateDirectories(Path.GetDirectoryName(filePath)))
                    {
                        foreach (var file in Directory.EnumerateFiles(dir, "info.idx"))
                        {
                            InitializeHandler(file);
                        }
                    }
                    // Create Repack Folder Handler.
                    Repack_Handler(filePath);
                    return;
                case ".zpak":
                    // Decompress BPE File to a Temporary File and replace "filePath" Variable with it.
                    FileManager.TmpMan.SetNewAssociatedPath(filePath);
                    string tmpPath = FileManager.TmpMan.GetTmpFilePath(Path.GetFileNameWithoutExtension(filePath));
                    byte[] zfile = DataTypes.Common.BPE.decompress(File.ReadAllBytes(filePath));
                    File.WriteAllBytes(tmpPath, zfile);

                    // Then Create PAK File Handler.
                    Unpack_Handler(typeof(Common.PAK), tmpPath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), true);

                    // Then Delete Temporary File Created.
                    FileManager.TmpMan.CleanTmpFile(Path.GetFileNameWithoutExtension(filePath));
                    break;
                case ".pak":
                    // Create PAK File Handler.
                    Unpack_Handler(typeof(Common.PAK), filePath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), false);
                    break;
                case ".pck":
                    // Create PCK File Handler.
                    if (!FileManager.BinMan.GetBinaryData_String(File.ReadAllBytes(filePath), 0).Contains("EPCK"))
                    {
                        break;
                    }

                    Unpack_Handler(typeof(Common.PCK), filePath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), false);
                    break;
            }

            // If no File was Unpacked, don't do recursive Unpacking or Delete File
            if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath))))
            {
                return;
            }

            if (Properties.Settings.Default.UnpackDeleteFile == true)
            {
                File.Delete(filePath);
            }

            if (Properties.Settings.Default.UnpackComplete != true)
            {
                return;
            }

            // Unpack sub Paks, Compressed Paks and PCKs
            RecursiveUnpacking(filePath, new string[] { "*.pak", "*.zpak", "*.pck" });
        }

        public void Unpack_Handler(Type T, string filePath, string UnpackDir, bool ZBPE)
        {
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), T) as Common.IPak);
            Package.FilePath = filePath;
            Package.UnpackedDir = UnpackDir;
            Package.ZBPE = ZBPE;
            Package.InitializeSubFileCount(filePath);
            Package.InitializeFilePointersList(filePath);
            Package.InitializeEndOfFilePointer(filePath);
            Package.InitializeFilenamesList(filePath, Package.FileCount);
            Package.Unpack();
        }

        public void Repack_Handler(string filePath)
        {
            var idxFile = new StreamReader(filePath);
            Type PackageType = Type.GetType("SpikeSoft.DataTypes.Common." + idxFile.ReadLine());
            var Package = (CommonMan.GetInterfaceObject(typeof(Common.IPak), PackageType) as Common.IPak);
            Package.FileCount = int.Parse(idxFile.ReadLine());
            Package.ZBPE = bool.Parse(idxFile.ReadLine().ToLowerInvariant());
            idxFile.Close();

            Package.FilePath = filePath;
            Package.Repack();
        }

        public void RecursiveUnpacking(string filePath, string[] args)
        {
            foreach (var arg in args)
            {
                foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), arg))
                {
                    InitializeHandler(subFile);
                }
            }
        }
    }
}
