using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;
using SpikeSoft.UiUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Compression
{
    public class DecompressAll : GenericToolMenuItem<DecompressAll>
    {
        public DecompressAll() : base("Decompress All in Folder") { }
        
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            string Dir = FileMan.GetDirectoryPath("Select a Folder containing Compressed Files");
            task(Dir);
        }

        public async void task(string Dir)
        {
            if (string.IsNullOrEmpty(Dir))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<ProgressInfo>>(toolBtnBpeAll_Work), new object[] { Dir }, false);
        }

        private async void toolBtnBpeAll_Work(object[] args, IProgress<ProgressInfo> progress)
        {
            string baseDir = args[0] as string;

            if (!Directory.Exists(baseDir)) return;

            int ID = 1;
            float maxValue = Directory.GetFiles(baseDir, "*.z*").Length;

            foreach (var file in Directory.EnumerateFiles(baseDir, "*.z*"))
            {
                if (progress != null)
                {
                    int v = (int)(((ID++) / (float)maxValue) * 100);
                    progress.Report(new ProgressInfo { Value = v });
                }

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
    }
}
