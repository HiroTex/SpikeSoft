using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.DataTypes
{
    public static class CommonMan
    {
        public static object GetInterfaceObject(Type iType, Type oType)
        {
            if (!oType.IsAssignableFrom(oType))
            {
                throw new Exception($"Class {oType} is not Interface {iType} Assignable");
            }

            var Interface = Activator.CreateInstance(oType);

            if (Interface == null)
            {
                throw new Exception($"Could not Create Instance of Class {oType}");
            }

            return Interface;
        }
    }
}
