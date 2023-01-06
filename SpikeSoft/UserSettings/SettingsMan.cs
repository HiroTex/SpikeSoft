using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace SpikeSoft.UserSettings
{
    class SettingsMan
    {
        public static SettingsMan Instance = new SettingsMan();

        public void SetDefaultResources()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.ExePath))
            {
                Properties.Settings.Default.ExePath = AppDomain.CurrentDomain.BaseDirectory;
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.CommonResourcePath))
            {
                Properties.Settings.Default.CommonResourcePath = Path.Combine(Properties.Settings.Default.ExePath, "resources");
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.CommonTXTPath))
            {
                Properties.Settings.Default.CommonTXTPath = Path.Combine(Properties.Settings.Default.CommonResourcePath, "txt");
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.CommonIMGPath))
            {
                Properties.Settings.Default.CommonIMGPath = Path.Combine(Properties.Settings.Default.CommonResourcePath, "images");
            }
            if (string.IsNullOrEmpty(Properties.Settings.Default.CommonGAMEPath))
            {
                Properties.Settings.Default.CommonGAMEPath = "ZS" + (Properties.Settings.Default.GAMEMODE + 1).ToString();
            }

            Properties.Settings.Default.Save();
            InitializeResources();
        }
        public void ResetDefaultResources()
        {
            Properties.Settings.Default.ExePath = AppDomain.CurrentDomain.BaseDirectory;
            Properties.Settings.Default.CommonResourcePath = Path.Combine(Properties.Settings.Default.ExePath, "resources");
            Properties.Settings.Default.CommonTXTPath = Path.Combine(Properties.Settings.Default.CommonResourcePath, "txt");
            Properties.Settings.Default.CommonIMGPath = Path.Combine(Properties.Settings.Default.CommonResourcePath, "images");
            Properties.Settings.Default.CommonGAMEPath = "ZS" + (Properties.Settings.Default.GAMEMODE + 1).ToString();
            InitializeResources();
        }
        public void InitializeResources()
        {
            foreach (var resourceInitializer in SettingsResources.TXTResourceInitializers)
            {
                resourceInitializer();
            }
            foreach (var resourceInitializer in SettingsResources.IMGResourceInitializers)
            {
                resourceInitializer();
            }
        }
        public void ChangeResourcePath(string settingToChange, string newValue)
        {
            if (Properties.Settings.Default.PropertyValues[settingToChange] == null)
            {
                // Settings does not Exist
                return;
            }

            Properties.Settings.Default.PropertyValues[settingToChange].PropertyValue = newValue;
        }
    }
}
