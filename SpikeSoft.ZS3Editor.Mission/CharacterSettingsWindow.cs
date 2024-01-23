using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Editor.Mission
{
    public partial class CharacterSettingsWindow : Form
    {
        private OpponentInfo info;
        private readonly ZS3EditorMission editor;
        private int missionID = -1;
        private int opponentID = -1;

        public CharacterSettingsWindow(ZS3EditorMission source, int MissionID, int OpponentID, OpponentInfo Info, ImageList iList)
        {
            InitializeComponent();

            editor = source;
            missionID = MissionID;
            opponentID = OpponentID;
            info = Info;
            imgList = iList;

            InitializeControls();
            InitializeInfo();
            
        }

        private void InitializeControls()
        {
            if (editor.CharaList != null)
            {
                charaBox.DataSource = editor.CharaList;
            }

            if (editor.ZitemList != null)
            {
                if (editor.ZitemList[editor.ZitemList.Count - 1] != "Empty") editor.ZitemList.Add("Empty");
                if (editor.ZitemList[editor.ZitemList.Count - 2] != "Random") editor.ZitemList.Insert(editor.ZitemList.Count - 1, "Random");

                boxZItem1.DataSource = new List<string>(editor.ZitemList);
                boxZItem2.DataSource = new List<string>(editor.ZitemList);
                boxZItem3.DataSource = new List<string>(editor.ZitemList);
                boxZItem4.DataSource = new List<string>(editor.ZitemList);
                boxZItem5.DataSource = new List<string>(editor.ZitemList);
                boxZItem6.DataSource = new List<string>(editor.ZitemList);
                boxZItem7.DataSource = new List<string>(editor.ZitemList);
                boxZItem8.DataSource = new List<string>(editor.ZitemList);
            }

            EnableControls();
        }

        private void InitializeInfo()
        {
            if (info.CharacterID == editor.RandomID)
            {
                charaBox.SelectedIndex = editor.RandomCharaID;
            }
            else if (info.CharacterID == editor.BlankID)
            {
                charaBox.SelectedIndex = editor.EmptyCharaID;
            }
            else
            {
                charaBox.SelectedIndex = info.CharacterID;
            }
            
            costumeNumeric.Value = info.CostumeID;
            AINumeric.Value = info.AI;

            for (int i = 0; i < 8; i++)
            {
                var zBox = this.Controls.Find($"boxZItem{i+1}", true).FirstOrDefault() as ComboBox;
                if (zBox == null)
                {
                    throw new Exception("Missing zItemBox Control");
                }

                if (info.Customization[i] == editor.RandomID)
                {
                    zBox.SelectedIndex = zBox.Items.Count - 2;
                }
                else if (info.Customization[i] == editor.BlankID)
                {
                    zBox.SelectedIndex = zBox.Items.Count - 1;
                }
                else
                {
                    zBox.SelectedIndex = info.Customization[i];
                }
            }
        }
        
        private void EnableControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox || control is NumericUpDown)
                {
                    control.Enabled = true;
                }
            }
        }

        UiUtils.ImageAnimator charaPicAnimator = null;
        int prevPicIndex = -1;

        private void charaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!charaBox.Enabled) return;

            if (charaPicAnimator != null)
            {
                charaPicAnimator.ForceEnd();
            }

            charaPicAnimator = new UiUtils.ImageAnimator(charaPicBox, imgList, prevPicIndex, charaBox.SelectedIndex);
            charaPicAnimator.SetTimer(this, 10, charaPicAnimator.ImageTransition, false);
            prevPicIndex = charaBox.SelectedIndex;
            var id = charaBox.SelectedIndex;

            if (id == editor.RandomCharaID) id = editor.RandomID;
            else if (id == editor.EmptyCharaID) id = editor.BlankID;

            info.CharacterID = id;
        }

        private void Numeric_ValueChanged(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled) return;

            var control = (sender as NumericUpDown);
            switch (control.Name)
            {
                case "AINumeric":
                    info.AI = (int)(control.Value);
                    break;
                case "costumeNumeric":
                    info.CostumeID = (int)(control.Value);
                    break;
            }
        }

        private void zItemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled) return;

            var control = (sender as ComboBox);
            int slotID = int.Parse(control.Name.Remove(0, 8)) - 1;
            var itemID = control.SelectedIndex;

            if (itemID == control.Items.Count - 2) itemID = editor.RandomID;
            else if (itemID == control.Items.Count - 1) itemID = editor.BlankID;

            info.Customization[slotID] = itemID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (missionID < 0 || editor.MissionInfo == null || opponentID < 0)
            {
                this.Close();
            }

            editor.MissionInfo[missionID].OpponentInfo[opponentID] = info;
            editor.ExternalOpponentDataUpdate(opponentID);
            this.Close();
        }
    }
}
