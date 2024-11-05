using SpikeSoft.UtilityManager;
using SpikeSoft.UiUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.ZS3Utilities.Tools.Compression
{
    public class CompressSingle : GenericToolMenuItem<CompressSingle>
    {
        public CompressSingle() : base("Compress Single") { }
        
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            string FilePath = FileMan.GetFilePath("Select a File", FilterNames, FilterExt);
            task(FilePath);
        }

        public async void task(string FilePath)
        {
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
    }
}
