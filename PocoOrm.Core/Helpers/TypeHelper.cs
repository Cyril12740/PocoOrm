using System;

namespace PocoOrm.Core.Helpers
{
    public static class TypeHelper
    {
        public static bool Is(this object obj, Type type)
        {
            Type objType = obj.GetType();

            while (objType.BaseType != null)
            {
                if (objType == type)
                {
                    return true;
                }

                objType = objType.BaseType;
            }

            return false;
        }
    }
}