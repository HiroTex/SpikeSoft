using SpikeSoft.UtilityManager.TaskProgress;
using System;
using System.Threading.Tasks;

namespace SpikeSoft.UtilityManager
{
    public class FunMan
    {
        public async Task InitializeTask(string title, Action<object[], IProgress<ProgressInfo>> AsyncMethod, object[] args, bool hidden)
        {
            var Worker = new BWorkWindow();
            await Worker.InitializeNewTask(title, AsyncMethod, args, hidden);
        }
    }
}
