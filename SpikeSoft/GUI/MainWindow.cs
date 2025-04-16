using SpikeSoft.DataTypes;
using SpikeSoft.FileManager;
using SpikeSoft.GUI;
using SpikeSoft.UiUtils;
using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SpikeSoft
{
    public partial class MainWindow : Form
    {
        Dictionary<string, System.Reflection.MethodInfo> customMethods = new Dictionary<string, System.Reflection.MethodInfo>();
        ToolMan toolMan;

        public MainWindow()
        {
            InitializeComponent();
            LoadExternalTools();
            InitializeToolRegistry();
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

        private void LoadExternalTools()
        {
            // Define the path to the DLL folder relative to the executable's location
            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources", "lib");

            if (!Directory.Exists(dllPath))
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "The library folder does not exist: " + dllPath });
                return;
            }

            // Iterate over each DLL in the directory
            foreach (string dllFile in Directory.GetFiles(dllPath, "*.dll"))
            {
                try
                {
                    // Load the assembly
                    Assembly assembly = Assembly.LoadFrom(dllFile);

                    // Search for the ToolHandler class
                    var toolHandlerType = assembly.GetTypes()
                                                  .FirstOrDefault(t => t.IsClass && t.Name == "ToolHandler");

                    if (toolHandlerType != null)
                    {
                        // Create an instance of ToolHandler
                        var toolHandlerInstance = Activator.CreateInstance(toolHandlerType);

                        // Find the GetTools method
                        var getToolsMethod = toolHandlerType.GetMethod("GetTools", BindingFlags.Public | BindingFlags.Instance);

                        if (getToolsMethod != null)
                        {
                            // Invoke GetTools to get the list of ToolStripMenuItems
                            var toolItems = getToolsMethod.Invoke(toolHandlerInstance, null) as List<ToolStripMenuItem>;

                            // Add each ToolStripMenuItem to the main menuStrip
                            if (toolItems != null)
                            {
                                foreach (var toolItem in toolItems)
                                {
                                    utilsToolStripMenuItem.DropDownItems.Add(toolItem);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during DLL loading or reflection
                    ExceptionMan.ThrowMessage(0x2000, new string[] { $"Error loading tools from {dllFile}: {ex.Message}" });
                }
            }
        }

        private void InitializeToolRegistry()
        {
            toolMan = new ToolMan();

            // Load external plugins from configuration file
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtType.txt");
            toolMan.LoadPlugins(configFilePath);
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

                string extension = Path.GetExtension(filepath);

                // Get the tool type for this file extension
                Type toolType = toolMan.GetToolForExtension(extension);

                if (toolType != null)
                {
                    ExecuteTask(toolType, filepath);
                    continue;
                }

                // Search and Execute File Specific Functionality
                FileManager.FunMan Fun = new FileManager.FunMan();
                if (Fun.ExecuteFileFunction(filepath))
                {
                    continue;
                }

                SetEditorUI(filepath);
            }

            this.Focus();
        }

        private void ExecuteTask(Type toolType, string filePath)
        {
            try
            {
                // Create an instance of the tool class
                var toolInstance = Activator.CreateInstance(toolType);

                // Find the 'task' method that takes a string parameter
                MethodInfo taskMethod = toolType.GetMethod("task", BindingFlags.Public | BindingFlags.Instance);

                if (taskMethod != null)
                {
                    // Invoke the 'task' method with the file path as the argument
                    taskMethod.Invoke(toolInstance, new object[] { filePath });
                }
                else
                {
                    ExceptionMan.ThrowMessage(0x2000, new string[] { $"The tool '{toolType.Name}' does not have a compatible 'task' method." });
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { $"Error executing task for {filePath}: {ex.Message}" });
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

            // If File does not have a Functionality, try to get a File Editor
            // Get Main Editor Window
            Control MainEditor = UI.GetEditorUI(filePath);
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
    }
}
