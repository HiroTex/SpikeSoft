using SpikeSoft.UtilityManager;
using System;
using System.Collections.Generic;
using SpikeSoft.ZLib;
using SpikeSoft.UiUtils;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Packaging
{
    public class UnpackSingle : GenericToolMenuItem<UnpackSingle>
    {
        public UnpackSingle() : base("Unpack Single") { }
        
        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            List<string> FilterNames = new List<string>();
            List<string> FilterExt = new List<string>();

            FilterNames.Add("Package");
            FilterExt.Add("*.pak;*.zpak;*.pck");

            string FilePath = FileMan.GetFilePath("Select an appropriate Package File", FilterNames, FilterExt);
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
