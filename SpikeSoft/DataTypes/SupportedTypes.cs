using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes
{
    public static class SupportedTypes
    {
        // Dictionary that contains:
        // Key: Type of File
        // Value: File Extension Filter for Data Type
        public static readonly Dictionary<string, string> MainFilterList = new Dictionary<string, string>
        {
            {"Binary Data", "*.dat;*.bin"},
            {"Image Data", "*.dbt;*.cdbt" },
            {"Model Data", "*.mdl" },
            {"Animation Data", "*.anm;*.canm" },
            {"Script Data", "*.gsc"},
            {"Package", "*.pak;*.zpak;*.pck;*.idx" }
        };

        // Dictionary that contains:
        // Key: Valid Editable File Extension
        // Value: Function that returns appropriate Editor for File Extension always using File Path as Parameter
        public static readonly Dictionary<string, Func<string, Type>> FileExtensions = new Dictionary<string, Func<string, Type>>
        {
            { ".dat", ParseByFileName },
            { ".pmdl", null },
            { ".mdl", null },
            { ".pck", Package},
            { ".pak", Package },
            { ".zpak", Package },
            { ".idx", Package },
            { ".dbt", null },
            { ".zdbt", null },
            { ".anm", null },
            { ".zanm", null },
            { ".gsc", null },
            { ".fod", null },
            { ".bin", null }
        };

        // Dictionary that contains:
        // Key: Hardcoded File Name for specific editable File parsing
        // Value: Type of UI Editor that Edits that File
        public static readonly Dictionary<string, Type> FileNameIds = new Dictionary<string, Type>
        {
            { "common_character_info", typeof(SpikeSoft.GUI.ZS3.CharaInfo.CharaInfoUIHandler) },
            { "unlock_item_param", typeof(SpikeSoft.GUI.ZS3.TourUnlockables.TourUnlockablesUIHandler) }
        };

        public static Type ParseByFileName(string filePath)
        {
            foreach (var Identifier in FileNameIds)
            {
                if (Path.GetFileName(filePath).Contains(Identifier.Key))
                {
                    return Identifier.Value;
                }
            }
            return null;
        }

        public static Type Package(string filePath)
        {
            return typeof(PakMan);
        }
    }
}
