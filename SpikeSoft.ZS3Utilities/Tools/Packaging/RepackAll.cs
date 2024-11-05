using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;
using SpikeSoft.UiUtils;
using SpikeSoft.ZLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Packaging
{
    public class RepackAll : GenericToolMenuItem<RepackAll>
    {
        public RepackAll() : base("Repack All in Folder") { }

        // Override to implement specific action for CharacterSwap
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            string FilePath = FileMan.GetDirectoryPath("Select a Folder with Packages");
            task(FilePath);
        }

        public async void task(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<ProgressInfo>>(toolBtnRepackAll_Click_DoWork), new object[] { FilePath }, false);
        }

        private void toolBtnRepackAll_Click_DoWork(object[] fpath, IProgress<ProgressInfo> progress)
        {
            string filePath = fpath[0] as string;
            if (!Directory.Exists(filePath)) return;

            int ID = 1;
            foreach (var dir in Directory.EnumerateDirectories(filePath))
            {
                if (progress != null)
                {
                    int v = (int)(((ID++) / (float)Directory.GetDirectories(filePath).Length) * 100);
                    progress.Report(new ProgressInfo { Value = v });
                }

                foreach (var file in Directory.EnumerateFiles(dir, "*.idx"))
                {
                    PakMan pak = new PakMan();
                    pak.ShowProgressWindow = false;
                    var t = Task.Run(async () => await pak.InitializeHandler(file));
                }
            }
        }
    }
}
