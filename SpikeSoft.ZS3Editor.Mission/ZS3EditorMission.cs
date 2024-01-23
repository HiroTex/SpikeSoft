using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SpikeSoft.ZS3Editor.Mission
{
    public partial class ZS3EditorMission: UserControl
    {
        #region gVar
        public List<Mission> MissionInfo;
        public List<string> CharaList;
        public List<string> ZitemList;

        public int RandomCharaID = 161;
        public int EmptyCharaID = 164;
        public int RandomID = 998;
        public int BlankID = 999;
        #endregion

        #region Ctor
        public ZS3EditorMission()
        {
            InitializeComponent();
        }

        public ZS3EditorMission(object[] args)
        {
            InitializeComponent();

            MissionInfo = args[0] as List<Mission>;
            charaImages = args[1] as ImageList;
            mapImages = args[2] as ImageList;
            CharaList = args[3] as List<string>;
            ZitemList = args[4] as List<string>;
            MapBox.DataSource = (args[5] as List<string>);
            bgmBox.DataSource = (args[6] as List<string>);
            RefereeBox.DataSource = (args[7] as List<string>);

            foreach (var mission in MissionInfo)
            {
                MissionBox.Items.Add(mission.Title);
            }

            var MissionType = (MissionInfo[0].BattleInfo.GetType() == typeof(MissionBattleInfo));
            if (MissionType) conditionBox.DataSource = (args[8] as List<string>);

            var SurvivorType = (MissionInfo[0].BattleInfo.GetType() == typeof(SurvivalBattleInfo));
            if (SurvivorType)
            {
                for (int i = 0; i < 7; i++)
                {
                    charaSetBox.Items.Add($"Set {(i + 1).ToString("00")}");
                }
            }

            EnableAllControls();

            MissionBox.SelectedIndex = 0;
        }

        private void EnableAllControls()
        {
            foreach (Control control in ControlPanel.Controls)
            {
                EnableControl(control);
            }
        }

        private void EnableControl(Control control)
        {
            if (control is PictureBox)
            {
                control.Enabled = true;
            }
            if (control is ComboBox)
            {
                EnableComboBox(control as ComboBox);
            }
        }

        private void EnableComboBox(ComboBox control)
        {
            switch (control.Name)
            {
                case "dpBox":
                case "conditionBox":
                    control.Enabled = (MissionInfo[0].BattleInfo.GetType() == typeof(MissionBattleInfo));
                    break;
                case "charaSetBox":
                    control.Enabled = (MissionInfo[0].BattleInfo.GetType() == typeof(SurvivalBattleInfo));
                    break;
                default:
                    control.Enabled = true;
                    break;
            }
        }
        #endregion

        #region "Editor Data Update" 
        public void ExternalOpponentDataUpdate(int opponentID)
        {
            UpdateOpponentData(opponentID);
            MissionBox_SelectedIndexChanged(null, null);
        }

        private void MissionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return; 
            }

            var mission = MissionInfo[MissionBox.SelectedIndex];
            var missionType = mission.BattleInfo.GetType();
            var OpponentIDs = new int[mission.OpponentInfo.Count];

            for (int i = 0; i < OpponentIDs.Length; i++)
            {
                OpponentIDs[i] = mission.OpponentInfo[i].CharacterID;
            }

            UpdateBattleSettings((mission.BattleInfo).BattleSettings);

            if (missionType == typeof(MissionBattleInfo))
            {
                conditionBox.SelectedIndex = ((MissionBattleInfo)mission.BattleInfo).Type;
                dpBox.SelectedIndex = ((MissionBattleInfo)mission.BattleInfo).Dp;
            }
            else if (missionType == typeof(SurvivalBattleInfo))
            {
                if (charaSetBox.SelectedIndex < 0) charaSetBox.SelectedIndex = 0;
                var set = charaSetBox.SelectedIndex;

                var count = 8;
                if ((50 - (set * 8)) < 8) count = (50 - (set * 8));
                OpponentIDs = new int[count];

                for (int i = 0; i < count; i++)
                {
                    OpponentIDs[i] = mission.OpponentInfo[(set * 8) + i].CharacterID;
                }
            }

            UpdateCharaPics(OpponentIDs);

            if (sender != null) (sender as ComboBox).Focus();
        }

        private void UpdateBattleSettings(BattleSettings settings)
        {
            RefereeBox.SelectedIndex = settings.Referee;
            mapDesBox.SelectedIndex = settings.MapDestruction;
            timeBox.SelectedIndex = settings.Time;
            if (settings.Bgm == RandomID) settings.Bgm = bgmBox.Items.Count - 2;
            if (settings.Bgm == BlankID) settings.Bgm = bgmBox.Items.Count - 1;
            bgmBox.SelectedIndex = settings.Bgm;
            TransformableBox.SelectedIndex = settings.TransformableEnemies;
            if (settings.Map == RandomID) settings.Map = MapBox.Items.Count - 4;
            if (settings.Map == BlankID) settings.Map = MapBox.Items.Count - 3;
            MapBox.SelectedIndex = settings.Map;
        }

        int[] currentCharaPicID = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
        UiUtils.ImageAnimator[] CharaPicAnims = new UiUtils.ImageAnimator[8];

        private void UpdateCharaPics(int[] OpponentID)
        {
            for (int i = 0; i < OpponentID.Length; i++)
            {
                var charaBox = (PictureBox)ControlPanel.Controls.Find($"picChara{(char)('A' + i)}", true).FirstOrDefault();
                if (OpponentID[i] == RandomID) OpponentID[i] = RandomCharaID;
                if (OpponentID[i] == BlankID) OpponentID[i] = EmptyCharaID;

                if (charaBox == null)
                {
                    throw new Exception("PictureBox Control not Found");
                }

                if (currentCharaPicID[i] != -1)
                {
                    if (currentCharaPicID[i] == OpponentID[i])
                    {
                        continue;
                    }
                }

                AnimateCharaPic(i, charaBox, charaImages, currentCharaPicID[i], OpponentID[i]);
                currentCharaPicID[i] = OpponentID[i];
            }

            var remainderBlank = 8 - OpponentID.Length;
            for (int i = 0; i < remainderBlank; i++)
            {
                var charaBox = (PictureBox)ControlPanel.Controls.Find($"picChara{(char)('H' - i)}", true).FirstOrDefault();

                if (charaBox == null)
                {
                    throw new Exception("PictureBox Control not Found");
                }

                if (currentCharaPicID[7 - i] != -1)
                {
                    if (currentCharaPicID[7 - i] == EmptyCharaID)
                    {
                        continue;
                    }
                }

                AnimateCharaPic(7 - i, charaBox, charaImages, currentCharaPicID[7 - i], EmptyCharaID);
                currentCharaPicID[7 - i] = EmptyCharaID;
            }
        }

        private void AnimateCharaPic(int charaPicID, PictureBox pBox, ImageList iList, int prevIndex, int nextIndex)
        {
            if (CharaPicAnims[charaPicID] != null)
            {
                CharaPicAnims[charaPicID].ForceEnd();
            }

            CharaPicAnims[charaPicID] = new UiUtils.ImageAnimator(pBox, charaImages, prevIndex, nextIndex);
            CharaPicAnims[charaPicID].SetTimer(this, 10, CharaPicAnims[charaPicID].ImageTransition, false);
        }

        private void picChara_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return;
            }

            if (MissionBox.SelectedIndex < 0)
            {
                return;
            }

            var controlName = (sender as PictureBox).Name;
            int controlID = controlName.Remove(0, 8)[0] - 'A';

            if (charaSetBox.Enabled)
            {
                controlID += (charaSetBox.SelectedIndex * 8);
            }

            if (controlID > MissionInfo[MissionBox.SelectedIndex].OpponentInfo.Count - 1)
            {
                return;
            }

            var charaInfo = MissionInfo[MissionBox.SelectedIndex].OpponentInfo[controlID];
            var charaEditor = new CharacterSettingsWindow(this, MissionBox.SelectedIndex, controlID, charaInfo, charaImages);
            charaEditor.Show();
        }

        private void picChara_MouseEnter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return;
            }

            if (MissionBox.SelectedIndex < 0)
            {
                return;
            }

            var controlName = (sender as PictureBox).Name;
            int controlID = controlName.Remove(0, 8)[0] - 'A';

            if (charaSetBox.Enabled)
            {
                controlID += (charaSetBox.SelectedIndex * 8);
            }

            if (controlID > MissionInfo[MissionBox.SelectedIndex].OpponentInfo.Count - 1)
            {
                return;
            }

            (sender as PictureBox).Cursor = Cursors.Hand;
        }

        private void picChara_MouseLeave(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return;
            }

            (sender as PictureBox).Cursor = Cursors.Default;
        }

        UiUtils.ImageAnimator MapPicAnim = null;
        int prevMapIndex = -1;

        private void MapBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return;
            }

            if (MapPicAnim != null)
            {
                MapPicAnim.ForceEnd();
            }

            MapPicAnim = new UiUtils.ImageAnimator(picMap, mapImages, prevMapIndex, MapBox.SelectedIndex);
            MapPicAnim.SetTimer(this, 10, MapPicAnim.ImageTransition, false);
            prevMapIndex = MapBox.SelectedIndex;

            int NewMapID = MapBox.SelectedIndex;
            if (NewMapID == MapBox.Items.Count - 4) NewMapID = RandomID;
            if (NewMapID == MapBox.Items.Count - 3) NewMapID = BlankID;

            var battleSettings = MissionInfo[MissionBox.SelectedIndex].BattleSettings;
            battleSettings.Map = NewMapID;
            MissionInfo[MissionBox.SelectedIndex].BattleSettings = battleSettings;

            UpdateBattleData();
        }

        private void BattleSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (!(sender as Control).Enabled) return;
            }

            var control = (sender as ComboBox);
            var battleSettings = MissionInfo[MissionBox.SelectedIndex].BattleSettings;
            var newID = control.SelectedIndex;

            switch (control.Name)
            {
                case "conditionBox":
                    MissionInfo[MissionBox.SelectedIndex].Condition = newID;
                    break;
                case "dpBox":
                    MissionInfo[MissionBox.SelectedIndex].DpMode = newID;
                    break;
                case "RefereeBox":
                    battleSettings.Referee = newID;
                    break;
                case "mapDesBox":
                    battleSettings.MapDestruction = newID;
                    break;
                case "timeBox":
                    battleSettings.Time = newID;
                    break;
                case "bgmBox":
                    battleSettings.Bgm = newID;
                    break;
                case "TransformableBox":
                    battleSettings.TransformableEnemies = newID;
                    break;
            }

            MissionInfo[MissionBox.SelectedIndex].BattleSettings = battleSettings;

            UpdateBattleData();
        }
        #endregion

        #region FileDataUpdate
        private string GetWorkFilePath(string filter)
        {
            return (SpikeSoft.UtilityManager.TmpMan.GetDefaultWrkFile().Contains(filter)) ? SpikeSoft.UtilityManager.TmpMan.GetDefaultTmpFile() : SpikeSoft.UtilityManager.TmpMan.GetTmpFile(1);
        }

        private void UpdateBattleData()
        {
            string battleInfoPath = GetWorkFilePath("battle_info");
            var data = MissionInfo[MissionBox.SelectedIndex].BattleInfo;
            int size = Marshal.SizeOf(data);
            int offset = MissionBox.SelectedIndex * size;
            SpikeSoft.UtilityManager.DataMan.StructToFile(data, battleInfoPath, offset);
        }

        private void UpdateOpponentData(int opponentID)
        {
            string enemyInfoPath = GetWorkFilePath("opponent");
            var data = MissionInfo[MissionBox.SelectedIndex].OpponentInfo[opponentID];
            int size = Marshal.SizeOf(data);
            int offset = (MissionBox.SelectedIndex * MissionInfo[MissionBox.SelectedIndex].OpponentCount) * size;
            offset = (offset) + (opponentID * size);
            SpikeSoft.UtilityManager.DataMan.StructToFile(data, enemyInfoPath, offset);
        }
        #endregion
    }
}
