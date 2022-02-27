using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastGenericNew.Tests
{
    public class TypeNewTest
    {
        #region ValueTypes

        [Test(Description = "Check is FastNew works on primary types")]
        [TestCaseSource(nameof(GetValueTypes))]
        [Parallelizable(ParallelScope.All)]
        public void ValueTypeCreateInstance<T>(T defaultVal) where T : new()
        {
            Assert.True(typeof(T).IsValueType, "T must be Value Type");

            // It seems only the first call of Activator.CreateInstance will call struct parameterless ctor on .NET Framework
            // But they are working well on .NET Core
            //Activator.CreateInstance<T>();

            var expected = Activator.CreateInstance<T>();
            var fastNewInst = FastNew.GetCreateInstance<T>(typeof(T)).Invoke();
            Assert.AreEqual(expected, fastNewInst);
        }

        private static IEnumerable GetValueTypes()
        {
            #region Primary Value Types

            yield return default(char);

            yield return default(byte);
            yield return default(sbyte);
            yield return default(short);
            yield return default(ushort);
            yield return default(int);
            yield return default(uint);
            yield return default(long);
            yield return default(ulong);

            yield return default(float);
            yield return default(double);
            yield return default(decimal);

            yield return default(nint);
            yield return default(nuint);

            #endregion

            yield return default(DateTime);
            yield return default(Guid);
            yield return default(TypeCode);

            yield return default(DemoStruct);
#if !NETFRAMEWORK
            yield return default(DemoStructParameterless);
#endif
        }

        #endregion
    }
}
