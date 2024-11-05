using SpikeSoft.UtilityManager;
using SpikeSoft.ZLib;
using SpikeSoft.UiUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Packaging
{
    public class RepackSingle : GenericToolMenuItem<RepackSingle>
    {
        public RepackSingle() : base("Repack Single") { }
        
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            FilterNames.Add("Package Info");
            FilterExt.Add("*.idx");

            string FilePath = FileMan.GetFilePath("Select an appropriate Info.Idx File", FilterNames, FilterExt);
            task(FilePath);
        }

        public async void task(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }

            PakMan worker = new PakMan();
            await worker.InitializeHandler(FilePath);
        }
    }
}
