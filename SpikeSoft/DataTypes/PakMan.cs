using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    // Create Repack Folder Handler.
                    return;
                case ".zpak":
                    // Decompress BPE File to a Temporary File and replace "filePath" Variable with it.
                    FileManager.TmpMan.SetNewAssociatedPath(filePath);
                    string tmpPath = FileManager.TmpMan.GetTmpFilePath(Path.GetFileNameWithoutExtension(filePath));
                    byte[] zfile = DataTypes.Common.BPE.decompress(File.ReadAllBytes(filePath));
                    File.WriteAllBytes(tmpPath, zfile);

                    // Then Create PAK File Handler.
                    PAK_Handler(tmpPath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), true);

                    // Then Delete Temporary File Created.
                    FileManager.TmpMan.CleanTmpFile(Path.GetFileNameWithoutExtension(filePath));
                    break;
                case ".pak":
                    // Create PAK File Handler.
                    PAK_Handler(filePath, Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), false);
                    break;
                case ".pck":
                    // Create PCK File Handler.
                    break;
            }

            if (Properties.Settings.Default.UnpackDeleteFile == true)
            {
                File.Delete(filePath);
            }

            if (Properties.Settings.Default.UnpackComplete != true)
            {
                return;
            }

            // Unpack sub Paks.
            foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), "*.pak"))
            {
                InitializeHandler(subFile);
            }

            // Unpack sub Compressed Paks.
            foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), "*.zpak"))
            {
                InitializeHandler(subFile);
            }

            // Unpack sub Pcks.
            foreach (var subFile in Directory.EnumerateFiles(Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath)), "*.pck"))
            {
                InitializeHandler(subFile);
            }
        }

        public void PAK_Handler(string filePath, string UnpackDir, bool ZBPE)
        {
            Common.PAK Package = new Common.PAK();
            Package.FilePath = filePath;
            Package.UnpackedDir = UnpackDir;
            Package.ZBPE = ZBPE;
            Package.InitializeSubFileCount(filePath);
            Package.InitializeFilePointersList(filePath);
            Package.InitializeEndOfFilePointer(filePath);
            Package.InitializeFilenamesList(filePath, Package.FileCount);
            Package.Unpack();
        }
    }
}
