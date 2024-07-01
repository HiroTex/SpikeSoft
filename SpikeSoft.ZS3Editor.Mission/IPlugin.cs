using SpikeSoft.UtilityManager;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Linq;

namespace SpikeSoft.ZS3Editor.Mission
{
    public class IPlugin : SpikeSoft.UtilityManager.IEditor
    {
        public ZS3EditorMission Editor;
        public Control UIEditor { get { return Editor; } }
        public Size UISize { get { return new Size(925, 688); } set { } }

        /// <summary>
        /// Dictionary to get Path of Opponent Data of Mission Type
        /// </summary>
        Dictionary<string, string> fileTypeToEnemyDataPath = new Dictionary<string, string>
        {
            { "mission", "ub_mission_opponent_info.dat" },
            { "survivor", "ub_survivor_opponent_info.dat" },
            { "challenge", "ub_ranking_challenge_opponent_info.dat" },
            { "ranking", "ub_ranking_opponent_info.dat" },
            { "circuit", "ub_circuit_opponent_info.dat" },
            { "sim", "sim_opponent_info.dat" }
        };

        /// <summary>
        /// Dictionary to get Total Mission Count of Mission Type
        /// </summary>
        Dictionary<string, int> fileTypeToMissionCount = new Dictionary<string, int>
        {
            { "mission", 100 },
            { "survivor", 3 },
            { "challenge", 36 },
            { "ranking", 99 },
            { "circuit", 5 },
            { "sim", 7 }
        };

        public void Initialize(string filePath)
        {
            List<object> args = new List<object>();
            List<Mission> Missions = new List<Mission>();
            List<string> Conditions = new List<string>();

            string fName = Path.GetFileNameWithoutExtension(filePath);
            string fDir = Path.GetDirectoryName(filePath);
            var fontPath = Path.Combine(fDir, "font_ultimatebattle.pak");
            var enemyDatafName = string.Empty;
            string fileType = GetFileType(fName);

            if (fileType != null && fileTypeToEnemyDataPath.TryGetValue(fileType, out enemyDatafName))
            {
                var enemyDataPath = Path.Combine(fDir, enemyDatafName);

                // Initialize new Temp Files to Work with
                TmpMan.InitializeMainTmpFile(filePath);
                TmpMan.SetNewAssociatedPath(enemyDataPath);

                dynamic battleInfo = null;
                int txtID = 0;
                int missionCount = 0;
                fileTypeToMissionCount.TryGetValue(fileType, out missionCount);

                switch (fileType)
                {
                    case "mission":
                        battleInfo = new StructMan<MissionBattleInfo>(filePath, 0, missionCount);
                        txtID = 216;

                        for (int i = 0; i < 15; i++)
                        {
                            Conditions.Add(DataMan.GetUnicodeStringFromTextPak(fontPath, 328 + i));
                        }

                        break;
                    case "survivor":
                        battleInfo = new StructMan<SurvivalBattleInfo>(filePath, 0, missionCount);
                        txtID = 316;
                        break;
                    case "circuit":
                        battleInfo = new StructMan<CircuitBattleInfo>(filePath, 0, missionCount);
                        txtID = 323;
                        break;
                    case "sim":
                    case "ranking":
                    case "challenge":
                        battleInfo = new StructMan<RankingBattleInfo>(filePath, 0, missionCount);
                        break;
                }

                if (battleInfo != null)
                {
                    int battleCount = 1;

                    foreach (dynamic mission in battleInfo)
                    {
                        // Populate Mission Objects with Data

                        string missionTitle = string.Empty;
                        switch (fileType)
                        {
                            case "mission":
                            case "survivor":
                            case "circuit":
                                missionTitle = DataMan.GetUnicodeStringFromTextPak(fontPath, txtID++);
                                break;
                            case "sim":
                                missionTitle = $"Battle {battleCount++.ToString("00")}";
                                break;
                            case "ranking":
                                missionTitle = $"Rank {battleCount++.ToString("00")}";
                                break;
                            case "challenge":
                                missionTitle = $"Challenger {battleCount++.ToString("00")}";
                                break;

                        }

                        List<OpponentInfo> OpponentData = new List<OpponentInfo>();

                        for (int i = 0; i < mission.OpponentID.Length; i++)
                        {
                            var offset = mission.OpponentID[i] * Marshal.SizeOf(typeof(OpponentInfo));
                            var opponentInfo1 = new StructMan<OpponentInfo>(enemyDataPath, offset);
                            OpponentData.Add(opponentInfo1[0]);
                        }

                        var obj = new Mission(missionTitle, mission, OpponentData);
                        Missions.Add(obj);
                    }
                }
            }

            // Initialize Editor Data

            args.Add(Missions); // [0]
            args.Add(SettingsResources.CharaChip); // [1]
            args.Add(SettingsResources.MapChip); // [2]
            args.Add(SettingsResources.CharaList); // [3]
            args.Add(SettingsResources.ZitemList); // [4]
            args.Add(SettingsResources.MapList); // [5]
            args.Add(SettingsResources.BgmList); // [6]
            args.Add(SettingsResources.RefereeList); // [7]
            args.Add(Conditions); // [8]

            Editor = new ZS3EditorMission(args.ToArray());
        }

        /// <summary>
        /// Get Mission Type by parsing text of File Name
        /// </summary>
        /// <param name="fName">File name</param>
        /// <returns></returns>
        private string GetFileType(string fName)
        {
            foreach (var fileType in fileTypeToEnemyDataPath.Keys)
            {
                if (fName.Contains(fileType))
                {
                    return fileType;
                }
            }
            return null;
        }
    }
}
