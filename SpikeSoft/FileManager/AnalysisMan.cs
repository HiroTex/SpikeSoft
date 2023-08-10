using System;
using System.Collections.Generic;
using SpikeSoft.UtilityManager;

namespace SpikeSoft.FileManager
{
    static public class AnalysisMan
    {
        // List of Methods to go Through until a Match occurs.
        static List<Func<byte[], string>> AnalysisMethods = new List<Func<byte[], string>>
        {
            AnalyseMAGIC,
            AnalyseBPE,
            AnalysePAK,
            AnalyseDBT
        };

        // Dictionary for Magic Header Values.
        // Key: Magic Header Identifier.
        // Value: File Name. 
        static Dictionary<string, string> MAGICHEADER = new Dictionary<string, string>()
        {
            {"FOD", "config.fod"},
            {"V000", ".v00" },
            {"CMAn", "camera.cma"},
            {"LIPS", "cv_vic.lps"},
            {"DBES", ".dbe"},
            {"pmdl", ".pmdl"},
            {"mdl3", ".mdl3"},
            {"EPCK", ".pck" }
        };

        /// <summary>
        /// Main Binary File Analysis Method to obtain a Generic File Name and Specific Extension.
        /// </summary>
        /// <param name="source">File as Byte Array</param>
        /// <returns></returns>
        static public string RunFileAnalysis(byte[] source)
        {
            foreach (var method in AnalysisMethods)
            {
                string type = method.Invoke(source);
                if (type != string.Empty) return type;
            }

            return "unknown.bin";
        }

        static public string AnalyseMAGIC(byte[] source)
        {
            // Get MAGIC Header String for File Extension Parsing
            string MAGIC = BinMan.GetBinaryData_String(source, 0);

            // Simple Check for ASCII TXT Files.
            if (source[0] == 0xFF && source[1] == 0xFE)
            {
                return "text.txt";
            }
            
            foreach (var identifier in MAGICHEADER)
            {
                if (MAGIC.Contains(identifier.Key))
                {
                    return identifier.Value;
                }
            }

            return string.Empty;
        }

        static public string AnalyseBPE(byte[] source)
        {
            // Get ZSize to try file length comparison.
            int ZSize = BinMan.GetBinaryData_Int32(source, 4);

            // Get Padding Bytes to substract from comparison value.
            int HeaderLength = 8;
            int PaddingCount = 0;
            for (int i = source.Length; i > 1; i--)
            {
                if (source[i - 1] != 0) break;
                PaddingCount++;
            }
            
            // Compare if ZSize Does not Matches File Length without Header and Padding Bytes.
            if (ZSize != (source.Length - HeaderLength - PaddingCount))
            {
                return string.Empty;
            }

            // Compare if UZSize is Bigger than ZSize to avoid other files that might match this comparison.
            int UZSize = BinMan.GetBinaryData_Int32(source, 0);
            if (UZSize <= ZSize)
            {
                return string.Empty;  
            }

            // Decompress File and re-analyze again to determine which kind of compressed file is.
            var BPEMan = new DataTypes.Common.BPE();
            string ZType = RunFileAnalysis(BPEMan.decompress(source));
            string Result = "compressed.z";
            switch (ZType)
            {
                case ".anm": return Result + "anm";
                case ".pak": return Result + "pak";
                case ".dbt": return Result + "dbt";
            }

            return Result;
        }

        static public string AnalysePAK(byte[] source)
        {
            // Try to Match PAK Header Structure.

            // Get Sub File Count
            int fCount = BinMan.GetBinaryData_Int32(source, 0);
            if (fCount < 1 || (fCount * 4 + 4) >= source.Length || fCount >= short.MaxValue)
            {
                return string.Empty;
            }

            // Skip Header until End of File Pointer to check matching Length
            int EOFPointer = BinMan.GetBinaryData_Int32(source, fCount * 4 + 4);
            if (EOFPointer != source.Length)
            {
                return string.Empty;
            }

            return ".pak";
        }

        static public string AnalyseDBT(byte[] source)
        {
            byte[] dbtPattern = new byte[] { 0x01, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x0E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                             0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            
            var limit = source.Length - dbtPattern.Length;
            for (var i = 0; i <= limit; i++)
            {
                var k = 0;
                for (; k < dbtPattern.Length; k++)
                {
                    if (dbtPattern[k] != source[i + k]) break;
                }

                if (k == dbtPattern.Length) return "texture.dbt";
            }

            return string.Empty;
        }
    }
}
