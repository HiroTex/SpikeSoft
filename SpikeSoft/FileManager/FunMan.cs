using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpikeSoft.UtilityManager;
using SpikeSoft.UtilityManager.TaskProgress;

namespace SpikeSoft.FileManager
{
    public class FunMan
    {
        public bool ExecuteFileFunction(string filePath)
        {
            foreach (var FileType in DataTypes.SupportedTypes.FileExtensions)
            {
                if (Path.GetExtension(filePath) != (FileType.Key) || FileType.Value == null)
                {
                    continue;
                }

                var ResultType = FileType.Value.Invoke(filePath);

                try
                {
                    IFunType Interface = (CommonMan.GetInterfaceObject(typeof(IFunType), ResultType) as IFunType);
                    Interface.InitializeHandler(filePath);
                    return true;
                }
                catch(TypeLoadException)
                {
                    continue;
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
                }
            }

            return false;
        }

        public async Task InitializeTask(string title, Action<object[], IProgress<int>> AsyncMethod, object[] args, bool hidden)
        {
            var Worker = new BWorkWindow();
            await Worker.InitializeNewTask(title, AsyncMethod, args, hidden);
        }
    }
}
