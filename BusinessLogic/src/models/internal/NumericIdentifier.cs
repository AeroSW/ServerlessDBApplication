using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.models
{
    internal class NumericIdentifier
    {
        private static HashSet<Type> NumericTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(uint),
            typeof(double),
            typeof(decimal),
            typeof(Byte),
            typeof(SByte),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort)
        };
        internal static bool IsNumeric(Type t)
        {
            return NumericTypes.Contains(t);
        }
        internal static bool IsNullableNumeric(Type t)
        {
            return NumericTypes.Contains(t) || NumericTypes.Contains(Nullable.GetUnderlyingType(t));
        }
    }
}
