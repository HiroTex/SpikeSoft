using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;
using SpikeSoft.ZLib;
using SpikeSoft.UiUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Packaging
{
    public class UnpackAll : GenericToolMenuItem<UnpackAll>
    {
        public UnpackAll() : base("Unpack All in Folder") { }
        
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            string FilePath = FileMan.GetDirectoryPath("Select a Folder containing Package Files");
            task(FilePath);
        }

        public async void task(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<ProgressInfo>>(toolBtnUnpackAll_Click_DoWork), new object[] { FilePath }, false);
        }

        private void toolBtnUnpackAll_Click_DoWork(object[] fpath, IProgress<ProgressInfo> progress)
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
                    if (progress != null)
                    {
                        int v = (int)(((ID++) / (float)maxValue) * 100);
                        progress.Report(new ProgressInfo { Value = v });
                    }

                    PakMan pak = new PakMan();
                    pak.ShowProgressWindow = false;
                    var t = Task.Run(async () => await pak.InitializeHandler(file));
                }
            }
        }
    }
}
