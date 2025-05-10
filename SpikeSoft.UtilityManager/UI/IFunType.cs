using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.UtilityManager
{
    public interface IFunType
    {
        Task InitializeHandler(string filePath);
    }
}
