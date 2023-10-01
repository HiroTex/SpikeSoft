using System;

namespace SpikeSoft.UtilityManager
{
    public static class CommonMan
    {
        public static object GetInterfaceObject(Type iType, Type oType)
        {
            if (iType == null || oType == null)
            {
                throw new ArgumentNullException();
            }

            if (!iType.IsAssignableFrom(oType))
            {
                throw new TypeLoadException($"Class {oType} is not Interface {iType} Assignable");
            }

            try
            {
                var Interface = Activator.CreateInstance(oType);

                if (Interface == null)
                {
                    throw new Exception($"Could not Create Instance of Class {oType}");
                }

                return Interface;
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { ex.Message });
            }

            return null;
        }
    }
}
