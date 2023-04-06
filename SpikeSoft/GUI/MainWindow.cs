using SpikeSoft.DataTypes;
using SpikeSoft.FileManager;
using SpikeSoft.GUI;
using SpikeSoft.UserSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        private void SetSaveBtn(bool set)
        {
            quickSaveFileBtn.Enabled = set;
            toolBtnSaveAs.Enabled = set;
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
            foreach (var filepath in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (!FileMan.ValidateFilePath(filepath))
                {
                    ExceptionMan.ThrowMessage(0x1000, new string[] { filepath }); continue;
                }

                SetEditorUI(filepath);
            }
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

            // Search and Execute File Specific Functionality
            FunMan Fun = new FunMan();
            if (Fun.ExecuteFileFunction(filePath))
            {
                return;
            }

            // If File does not have a Functionality, try to get a File Editor
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
            SetSaveBtn(true);
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

        #region Packaging

        private async void toolBtnUnpackSingle_Click(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            FilterNames.Add("Package");
            FilterExt.Add("*.pak;*.zpak;*.pck");

            string FilePath = FileMan.GetFilePath("Select an appropriate Package File", FilterNames, FilterExt);
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            DataTypes.PakMan worker = new PakMan();
            await worker.InitializeHandler(FilePath);
        }

        private async void toolBtnUnpackAll_Click(object sender, EventArgs e)
        {
            string FilePath = FileMan.GetDirectoryPath("Select a Folder containing Package Files");
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            FileManager.FunMan FUN = new FileManager.FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<int>>(toolBtnUnpackAll_Click_DoWork), new object[] { FilePath }, false);
        }

        private void toolBtnUnpackAll_Click_DoWork(object[] fpath, IProgress<int> progress)
        {
            string filePath = fpath[0] as string;
            string[] args = new string[] { "*.pak", "*.zpak", "*.pck" };

            if (!Directory.Exists(filePath)) return;

            foreach (var arg in args)
            {
                int ID = 1;
                float maxValue = Directory.GetFiles(filePath, arg).Length;

                foreach (var file in Directory.EnumerateFiles(filePath, arg))
                {
                    progress.Report((int)((ID++ / maxValue) * 100));
                    DataTypes.PakMan pak = new PakMan();
                    pak.ShowProgressWindow = false;
                    var t = Task.Run(async () => pak.InitializeHandler(file));
                    t.Wait();
                }
            }
        }

        private async void toolBtnRepackSingle_Click(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            FilterNames.Add("Package Info");
            FilterExt.Add("*.idx");

            string FilePath = FileMan.GetFilePath("Select an appropriate Info.Idx File", FilterNames, FilterExt);
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            DataTypes.PakMan worker = new PakMan();
            await worker.InitializeHandler(FilePath);
        }

        private async void toolBtnRepackAll_Click(object sender, EventArgs e)
        {
            string FilePath = FileMan.GetDirectoryPath("Select a Folder with Packages");
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            FileManager.FunMan FUN = new FileManager.FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<int>>(toolBtnRepackAll_Click_DoWork), new object[] { FilePath }, false);
        }

        private void toolBtnRepackAll_Click_DoWork(object[] fpath, IProgress<int> progress)
        {
            string filePath = fpath[0] as string;
            if (!Directory.Exists(filePath)) return;

            int ID = 1;
            foreach (var dir in Directory.EnumerateDirectories(filePath))
            {
                progress.Report((int)((ID++ / (float)Directory.GetDirectories(filePath).Length) * 100));
                foreach (var file in Directory.EnumerateFiles(dir, "*.idx"))
                {
                    DataTypes.PakMan pak = new PakMan();
                    pak.ShowProgressWindow = false;
                    var t = Task.Run(async () => pak.InitializeHandler(file));
                    t.Wait();
                }
            }
        }

        #endregion
    }
}
