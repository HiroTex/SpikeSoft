using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpikeSoft.FileManager
{
    public class FunMan
    {
        public bool ExecuteFileFunction(string filePath)
        {
            foreach (var FileType in SpikeSoft.DataTypes.SupportedTypes.FileExtensions)
            {
                if (Path.GetExtension(filePath) != (FileType.Key) || FileType.Value == null)
                {
                    continue;
                }

                var ResultType = FileType.Value.Invoke(filePath);

                if (!typeof(DataTypes.IFunType).IsAssignableFrom(ResultType))
                {
                    continue;
                }
                
                (DataTypes.CommonMan.GetInterfaceObject(typeof(DataTypes.IFunType), ResultType) as DataTypes.IFunType).InitializeHandler(filePath);
                return true;
            }

            return false;
        }

        public async Task InitializeTask(string title, Action<object[], IProgress<int>> AsyncMethod, object[] args, bool hidden)
        {
            var Worker = new GUI.BWorkWindow();
            Worker.SetLabel(title);
            Worker.SetProgressValue(0);
            var progress = new Progress<int>(v => (Worker.Controls["pBar"] as ProgressBar).Value = v);
            if (!hidden) Worker.Show();
            Thread.Sleep(500);
            await Task.Run(() => AsyncMethod(args, progress));
            Worker.Close();
            if (!hidden) MessageBox.Show("Task Completed Successfully", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
