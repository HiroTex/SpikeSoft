using Microsoft.VisualBasic.FileIO;
using SpikeSoft.UiUtils;
using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;
using SpikeSoft.ZLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SpikeSoft.ZS3Utilities.Tools.Meteor
{
    public class CharacterSwap : GenericToolMenuItem<CharacterSwap>
    {
        public CharacterSwap() : base("Unify Character Resources") { }

        protected override void OnToolBtnClick(object sender, EventArgs e)
        {
            var TopText = "Select Base File Pak";
            var FilterNames = new List<string>() { "Pak Files" };
            var FilterExt = new List<string>() { "*.pak" };
            string baseFile = FileMan.GetFilePath(TopText, FilterNames, FilterExt);
            task(baseFile);
        }

        public async void task(string baseFile)
        {
            if (!FileMan.ValidateFilePath(baseFile))
            {
                return;
            }

            FunMan FUN = new FunMan();
            await FUN.InitializeTask("Executing Package Work, Please Wait", new Action<object[], IProgress<ProgressInfo>>(work), new object[] { baseFile }, false);
        }

        public void work(object[] args, IProgress<ProgressInfo> progress)
        {
            var baseFile = args[0] as string;
            var paks = Directory.EnumerateFiles(Path.GetDirectoryName(baseFile), "*.pak");
            var list = new List<string>();
            var counter = 0;

            // Get Costume Files
            foreach (var pak in paks)
            {
                if (progress != null)
                {
                    progress.Report(new ProgressInfo { Value = (int)((counter++ / (float)paks.Count()) * 100), Message = "Get Costume Count..." });
                }

                var fCount = BinMan.GetBinaryData<int>(pak, 0);
                if (fCount != 252) continue;
                list.Add(pak);
            }

            counter = 0;

            // Unpack Files
            foreach (var pak in list)
            {
                if (progress != null)
                {
                    progress.Report(new ProgressInfo { Value = (int)((counter++ / (float)list.Count()) * 100), Message = "Unpacking..." });
                }

                var pakMan = new PakMan();
                pakMan.ShowProgressWindow = false;
                Task.Run(() => pakMan.Work_Handler(new object[] { pak }, null)).Wait();
            }

            var baseDir = Path.Combine(Path.GetDirectoryName(baseFile), Path.GetFileNameWithoutExtension(baseFile));
            counter = 0;

            // Copy and Repack Files
            foreach (var pak in list)
            {
                if (progress != null)
                {
                    progress.Report(new ProgressInfo { Value = (int)((counter++ / (float)list.Count()) * 100), Message = "Copying Files..." });
                }

                if (pak == baseFile) continue;

                var workDir = Path.Combine(Path.GetDirectoryName(baseFile), Path.GetFileNameWithoutExtension(pak));

                Directory.CreateDirectory(Path.Combine(workDir, "cv_lips_JP"));
                Directory.CreateDirectory(Path.Combine(workDir, "cv_lips_US"));
                Directory.CreateDirectory(Path.Combine(workDir, "move_list"));
                Directory.CreateDirectory(Path.Combine(workDir, "event_assets"));
                Directory.CreateDirectory(Path.Combine(workDir, "param_data"));
                Directory.CreateDirectory(Path.Combine(workDir, "model_assets"));

                FileSystem.CopyDirectory(Path.Combine(baseDir, "cv_lips_JP"), Path.Combine(workDir, "cv_lips_JP"), true);
                FileSystem.CopyDirectory(Path.Combine(baseDir, "cv_lips_US"), Path.Combine(workDir, "cv_lips_US"), true);
                FileSystem.CopyDirectory(Path.Combine(baseDir, "move_list"), Path.Combine(workDir, "move_list"), true);
                FileSystem.CopyDirectory(Path.Combine(baseDir, "event_assets"), Path.Combine(workDir, "event_assets"), true);

                var paramFiles = Directory.EnumerateFiles(Path.Combine(baseDir, "param_data"));

                foreach (var file in paramFiles)
                {
                    if (file.Contains("gimmick_info.dat")) continue;

                    File.Copy(file, Path.Combine(workDir, "param_data", Path.GetFileName(file)), true);
                }

                var hudPath = Path.Combine(baseDir, "model_assets", Path.GetFileName(baseDir) + "_hud.dbt");

                if (FileMan.ValidateFilePath(hudPath))
                {
                    File.Copy(hudPath, Path.Combine(workDir, "model_assets", Path.GetFileName(workDir) + "_hud.dbt"), true);
                }

                var pakMan = new PakMan();
                pakMan.ShowProgressWindow = false;
                Task.Run(() => pakMan.Work_Handler(new object[] { Path.Combine(workDir, "#info.idx") }, null)).Wait();
            }

            var dirList = Directory.GetDirectories(Path.GetDirectoryName(baseFile));
            for (int i = 0; i < dirList.Count(); i++)
            {
                if (progress != null)
                {
                    progress.Report(new ProgressInfo { Value = (int)((i / (float)dirList.Count()) * 100), Message = "Cleaning..." });
                }

                var pak = dirList[i];
                var repacker = new DirectoryInfo(pak).GetFiles("#info.idx").FirstOrDefault();
                if (repacker == null) continue;
                Task.Run(() => Directory.Delete(pak, true));
            }
        }
    }
}
