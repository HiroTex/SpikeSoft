using SpikeSoft.DataTypes;
using SpikeSoft.FileManager;
using SpikeSoft.GUI;
using SpikeSoft.UserSettings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SpikeSoft
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            mainToolStrip.Renderer = new MyRenderer();
            mainMenuStrip.Renderer = new MyRenderer();
            SettingsMan.Instance.SetDefaultResources();
        }

        private void EnableSave()
        {
            quickSaveFileBtn.Enabled = true;
            toolBtnSaveAs.Enabled = true;
        }

        private void TryDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ExecuteDragDrop(object sender, DragEventArgs e)
        {
            string[] FilePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (!FileMan.ValidateFilePath(FilePath[0]))
            {
                ExceptionMan.ThrowMessage(0x1000); return;
            }

            SetEditorUI(FilePath[0]);
        }

        private void OpenFile(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>(SupportedTypes.MainFilterList.Keys);
            List<string> FilterExt = new List<string>(SupportedTypes.MainFilterList.Values);

            string FilePath = FileMan.GetFilePath("Select an appropriate File", FilterNames, FilterExt);
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            SetEditorUI(FilePath);
        }

        private void SetEditorUI(string filePath)
        {
            UIMan UI = new UIMan();
            Control MainEditor = new Control();

            // Get Main Editor Window
            MainEditor = UI.GetEditorUI(filePath);
            if (MainEditor == null)
            {
                ExceptionMan.ThrowMessage(0x1001); return;
            }

            // Get Main Editor Window Size
            Size WindowSize = UI.GetEditorUISize(MainEditor.Name);
            MinimumSize = WindowSize;

            // Update Current Work Files
            TmpMan.InitializeMainTmpFile(filePath);

            // Set Editor Screen
            mainPanel.Controls.Clear();
            mainPanel.BackgroundImage = null;
            mainPanel.Controls.Add(MainEditor);

            // Enable File Saving
            EnableSave();
        }

        private void QuickSave(object sender, EventArgs e)
        {
            string OriginalPath = TmpMan.GetDefaultWrkFile();

            if (string.IsNullOrEmpty(OriginalPath))
            {
                return;
            }

            if (FileMan.SaveDefaultTmpFile(OriginalPath))
            {
                System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(Properties.Resources.confirmation);
                sfx.Play();
            }
        }

        private void SaveNewFile(object sender, EventArgs e)
        {
            // Check if Default Work Path has been Initialized
            string OriginalPath = TmpMan.GetDefaultWrkFile();

            if (string.IsNullOrEmpty(OriginalPath))
            {
                return;
            }

            string NewDefaultWrkFile = FileMan.SaveDefaultTmpFileAs(Path.GetFileNameWithoutExtension(OriginalPath), Path.GetExtension(OriginalPath));

            if (!string.IsNullOrEmpty(NewDefaultWrkFile))
            {
                TmpMan.InitializeMainTmpFile(NewDefaultWrkFile);
                MessageBox.Show("File Saved Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }
    }
}
