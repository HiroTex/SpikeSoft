using SpikeSoft.DataTypes;
using SpikeSoft.FileManager;
using SpikeSoft.GUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpikeSoft.UtilityManager;
using SpikeSoft.UiUtils;

namespace SpikeSoft
{
    public partial class MainWindow : Form
    {
        Dictionary<string, System.Reflection.MethodInfo> customMethods = new Dictionary<string, System.Reflection.MethodInfo>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            try
            {
                mainToolStrip.Renderer = new DarkRenderer();
                mainMenuStrip.Renderer = new DarkRenderer();
                SettingsMan.Instance.SetDefaultResources();
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }
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
            if (e.Effect == DragDropEffects.None)
            {
                return;
            }

            foreach (var filepath in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                if (!FileMan.ValidateFilePath(filepath))
                {
                    ExceptionMan.ThrowMessage(0x1000, new string[] { filepath }); continue;
                }

                SetEditorUI(filepath);
            }

            this.Focus();
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

            mainPanel.SuspendLayout();

            // Get Main Editor Window Size
            Size WindowSize = UI.GetEditorUISize();
            MinimumSize = WindowSize;
            Size = WindowSize;

            // Update Current Work Files
            TmpMan.InitializeMainTmpFile(filePath);

            // Set Custom Save Method
            customMethods.Clear();
            customMethods.Add("plgQuickSave", UI.GetEditorCustomMethod("plgQuickSave"));
            customMethods.Add("plgSaveAs", UI.GetEditorCustomMethod("plgSaveAs"));

            // Set Editor Screen
            mainPanel.Controls.Clear();
            mainPanel.BackgroundImage = null;
            mainPanel.Controls.Add(MainEditor);

            // Enable File Saving
            SetSaveBtn(true);
            mainPanel.ResumeLayout();
        }

        /// <summary>
        /// Executes a custom method if it exists on the Custom Method Dictionary or a default method instead.
        /// </summary>
        /// <param name="methodName">Method Name to search on Dictionary.</param>
        /// <param name="def">Default Action to perform if custom method is not found.</param>
        private void ExecuteMethodOrDefault(string methodName, Action def)
        {
            try
            {
                if (customMethods[methodName] != null)
                {
                    customMethods[methodName].Invoke(mainPanel.Controls[0], null);
                }
                else
                {
                    if (def != null)
                    {
                        def();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }
        }

        private void QuickSave(object sender, EventArgs e)
        {
            ExecuteMethodOrDefault("plgQuickSave", defaultQuickSave);
        }

        private void SaveNewFile(object sender, EventArgs e)
        {
            ExecuteMethodOrDefault("plgSaveAs", defaultNewSave);
        }

        private void defaultQuickSave()
        {
            int tmpCount = TmpMan.GetTmpPathCount();
            for (int i = 0; i < tmpCount; i++)
            {
                if (!FileMan.SaveTmpFile(i))
                {
                    throw new Exception($"Problem at file {i}");
                }
            }

            playSaveSFX(false);
        }

        private void defaultNewSave()
        {
            int tmpCount = TmpMan.GetTmpPathCount();
            for (int i = 0; i < tmpCount; i++)
            {
                string OriginalPath = TmpMan.GetWrkFile(i);
                string NewWrkFile = FileMan.SaveTmpFileAs(i, Path.GetFileNameWithoutExtension(OriginalPath), Path.GetExtension(OriginalPath));

                if (!string.IsNullOrEmpty(NewWrkFile))
                {
                    TmpMan.CleanTmpFile(OriginalPath);
                    TmpMan.SetNewAssociatedPath(NewWrkFile);
                }
            }

            playSaveSFX(true);
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }

        private void playSaveSFX(bool showMsg)
        {
            System.Media.SoundPlayer sfx = new System.Media.SoundPlayer(Properties.Resources.confirmation);
            sfx.Play();

            if (showMsg)
            {
                MessageBox.Show("File/s Saved Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

            PakMan worker = new PakMan();
            await worker.InitializeHandler(FilePath);
        }

        private async void toolBtnUnpackAll_Click(object sender, EventArgs e)
        {
            string FilePath = FileMan.GetDirectoryPath("Select a Folder containing Package Files");
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            FunMan FUN = new FunMan();
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
                    var t = Task.Run(async () => await pak.InitializeHandler(file));
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

            FunMan FUN = new FunMan();
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
                    PakMan pak = new PakMan();
                    pak.ShowProgressWindow = false;
                    var t = Task.Run(async () => await pak.InitializeHandler(file));
                }
            }
        }

        #endregion

        #region "Compression"
        private async void toolBtnBpeSingle_Click(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            FilterNames.Add("Compressed File");
            FilterExt.Add("*.z*");

            string FilePath = FileMan.GetFilePath("Select an appropriate Compressed File", FilterNames, FilterExt);
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            try
            {
                ZLib.BPE worker = new ZLib.BPE();
                await Task.Run(() => worker.decompress(File.ReadAllBytes(FilePath), null))
                    .ContinueWith(antecedent =>
                    {
                        if (antecedent.Result == null)
                        {
                            throw new Exception("Decompression Issue, Result is NULL");
                        }
                        else
                        {
                            var dir = Path.GetDirectoryName(FilePath);
                            var fname = Path.GetFileNameWithoutExtension(FilePath);
                            var ext = Path.GetExtension(FilePath).Replace("z", string.Empty);
                            File.WriteAllBytes(Path.Combine(dir, fname + ext), antecedent.Result);
                        }
                    });

                MessageBox.Show("Decompression was performed successfully", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }
        }

        private async void toolBtnZBpeSingle_Click(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            string FilePath = FileMan.GetFilePath("Select a File", FilterNames, FilterExt);
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            try
            {
                ZLib.BPE worker = new ZLib.BPE();
                await Task.Run(() => worker.compress(File.ReadAllBytes(FilePath), null))
                    .ContinueWith(antecedent =>
                    {
                        if (antecedent.Result == null)
                        {
                            throw new Exception("Compression Issue, Result is NULL");
                        }
                        else
                        {
                            var dir = Path.GetDirectoryName(FilePath);
                            var fname = Path.GetFileNameWithoutExtension(FilePath);
                            var ext = $".z{Path.GetExtension(FilePath).Replace(".", string.Empty)}";
                            File.WriteAllBytes(Path.Combine(dir, fname + ext), antecedent.Result);
                        }
                    });

                MessageBox.Show("Compression was performed successfully", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }
        }

        private async void toolBtnBpeAll_Click(object sender, EventArgs e)
        {
            string Dir = FileMan.GetDirectoryPath("Select a Folder containing Compressed Files");
            if (string.IsNullOrEmpty(Dir))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<int>>(toolBtnBpeAll_Work), new object[] { Dir }, false);
        }

        private async void toolBtnBpeAll_Work(object[] args, IProgress<int> progress)
        {
            string baseDir = args[0] as string;

            if (!Directory.Exists(baseDir)) return;

            int ID = 1;
            float maxValue = Directory.GetFiles(baseDir, "*.z*").Length;

            foreach (var file in Directory.EnumerateFiles(baseDir, "*.z*"))
            {
                progress.Report((int)((ID++ / maxValue) * 100));
                ZLib.BPE worker = new ZLib.BPE();
                await Task.Run(() => worker.decompress(File.ReadAllBytes(file), null))
                    .ContinueWith(antecedent =>
                    {
                        if (antecedent.Result == null)
                        {
                            throw new Exception("Decompression Issue, Result is NULL");
                        }
                        else
                        {
                            var dir = Path.GetDirectoryName(file);
                            var fname = Path.GetFileNameWithoutExtension(file);
                            var ext = Path.GetExtension(file).Replace("z", string.Empty);
                            File.WriteAllBytes(Path.Combine(dir, fname + ext), antecedent.Result);
                        }
                    });
            }
        }

        private async void toolBtnZBpeAll_Click(object sender, EventArgs e)
        {
            string Dir = FileMan.GetDirectoryPath("Select a Folder containing Files");
            if (string.IsNullOrEmpty(Dir))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<int>>(toolBtnZBpeAll_Work), new object[] { Dir }, false);
        }

        private async void toolBtnZBpeAll_Work(object[] args, IProgress<int> progress)
        {
            string baseDir = args[0] as string;

            if (!Directory.Exists(baseDir)) return;

            int ID = 1;
            float maxValue = Directory.GetFiles(baseDir).Length;

            foreach (var file in Directory.EnumerateFiles(baseDir))
            {
                progress.Report((int)((ID++ / maxValue) * 100));
                ZLib.BPE worker = new ZLib.BPE();
                await Task.Run(() => worker.compress(File.ReadAllBytes(file), null))
                    .ContinueWith(antecedent =>
                    {
                        if (antecedent.Result == null)
                        {
                            throw new Exception("Compression Issue, Result is NULL");
                        }
                        else
                        {
                            var dir = Path.GetDirectoryName(file);
                            var fname = Path.GetFileNameWithoutExtension(file);
                            var ext = $".z{Path.GetExtension(file).Replace(".", string.Empty)}";
                            File.WriteAllBytes(Path.Combine(dir, fname + ext), antecedent.Result);
                        }
                    });
            }
        }
        #endregion
    }
}
