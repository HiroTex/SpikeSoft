using SpikeSoft.UserSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.GUI
{
    public partial class SettingsWindow : Form
    {
        private readonly Dictionary<int, SettingsProperty> BooleanSettings = new Dictionary<int, SettingsProperty>
        {
            {0, Properties.Settings.Default.Properties["UnpackDeleteFile"] },
            {1, Properties.Settings.Default.Properties["UnpackComplete"] }
        };

        public SettingsWindow()
        {
            InitializeComponent();
            InitializeSettingData();
        }

        private void InitializeSettingData()
        {
            // Set Paths
            txtBoxResourcePath.Text = Properties.Settings.Default.CommonResourcePath;
            txtBoxTXTPath.Text = Properties.Settings.Default.CommonTXTPath;
            txtBoxIMGPath.Text = Properties.Settings.Default.CommonIMGPath;
            txtBoxGAMEPath.Text = Properties.Settings.Default.CommonGAMEPath;

            // Define Game Mode
            foreach (RadioButton button in groupBoxGameMode.Controls)
            {
                if (button.Name.Contains((Properties.Settings.Default.GAMEMODE + 1).ToString()))
                {
                    button.Checked = true;
                    break;
                }
            }

            // Define Wii Mode
            btnConsoleModeSetting1.Checked = !Properties.Settings.Default.WIIMODE;
            btnConsoleModeSetting2.Checked = Properties.Settings.Default.WIIMODE;

            // Define Checked Settings
            foreach (var setting in BooleanSettings)
            {
                bool param =Convert.ToBoolean(Properties.Settings.Default.PropertyValues[setting.Value.Name].PropertyValue);
                checkListSettings.SetItemChecked(setting.Key, param);
            }
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            MessageBox.Show("Settings Saved Successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetDefaultSettings(object sender, EventArgs e)
        {
            Properties.Settings.Default.GAMEMODE = 2;
            Properties.Settings.Default.WIIMODE = false;
            Properties.Settings.Default.UnpackDeleteFile = false;
            Properties.Settings.Default.UnpackComplete = true;
            SettingsMan.Instance.ResetDefaultResources();
            InitializeSettingData();
        }

        private void SearchPath(object sender, EventArgs e)
        {
            string NewPath = FileManager.FileMan.GetDirectoryPath("Select New Folder Path");
            if (string.IsNullOrEmpty(NewPath))
            {
                return;
            }
            foreach (TextBox box in groupBoxPaths.Controls)
            {
                if (box.Name == ((sender as Button).Name).Replace("btnSearch", "txtBox"))
                {
                    (sender as Button).Text = NewPath;
                    return;
                }
            }
        }

        private void PathChanged(object sender, EventArgs e)
        {
            string SettingName = (sender as TextBox).Name.Replace("txtBox", "Common");
            UserSettings.SettingsMan.Instance.ChangeResourcePath(SettingName,(sender as TextBox).Text);
        }

        private void UpdateSettingFlags(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                Properties.Settings.Default.PropertyValues[BooleanSettings[e.Index].Name].PropertyValue = true;
            }
            else
            {
                Properties.Settings.Default.PropertyValues[BooleanSettings[e.Index].Name].PropertyValue = false;
            }
        }

        private void SetGameMode(object sender, EventArgs e)
        {
            Properties.Settings.Default.GAMEMODE = int.Parse((sender as RadioButton).Name.Replace("btnGameModeSetting", string.Empty)) - 1;
        }

        private void SetWiiMode(object sender, EventArgs e)
        {
            if (btnConsoleModeSetting2.Checked)
            {
                Properties.Settings.Default.WIIMODE = true;
            }
            else
            {
                Properties.Settings.Default.WIIMODE = false;
            }
        }
    }
}
