using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FastGenericNew.Tests
{
    public static class TestData
    {
        public static readonly Type[] CommonReferenceTypesPL = new Type[]
        {
            typeof(DemoClass),
        };

        public static readonly Type[] CommonValueTypes = new Type[]
        {
            #region Primary Value Types

            typeof(char),

            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),

            typeof(float),
            typeof(double),
            typeof(decimal),

            typeof(nint),
            typeof(nuint),

#if NET8_0_OR_GREATER
            typeof(Int128),
            typeof(UInt128),
#endif

            #endregion

            typeof(DateTime),
            typeof(Guid),
            typeof(TypeCode), // Enum

            typeof(DemoStruct),
#if !NETFRAMEWORK
            typeof(DemoStructParameterless),
#endif
        };
    }
}
