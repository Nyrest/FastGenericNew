

using System.Dynamic;
using System.IO;
using System.Reflection.Emit;
using System.Xml.Serialization;

namespace FastGenericNew.Tests
{
    public class FastNewTests
    {
        [SetUp]
        public void Setup()
        {

        }

        #region CreateInstance Class

        [Test()]
        public void CreateInstanceClass()
        {
            var expected = new DemoClass();
            var actual = FastNew.CreateInstance<DemoClass>();
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void CreateInstanceClassParameter()
        {
            const int val = 99999;
            var expected = new DemoClass(val);
            var actual = FastNew.CreateInstance<DemoClass, int>(val);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void CreateInstanceClassPrivateCtor()
        {
            var actual = FastNew.CreateInstance<DemoClassPrivateCtor>();
            Assert.AreEqual(DemoClassPrivateCtor.DefaultValue, actual.value);
        }

        [Test()]
        public void CreateInstanceClassPrivateParameterCtor()
        {
            const int val = 99999;
            var actual = FastNew.CreateInstance<DemoClassPrivateCtor, int>(val);
            Assert.AreEqual(val, actual.value);
        }

        #endregion

        #region Exceptions should happen

        [Test()]
        public void ExceptionFastNewInterface()
        {
            try
            {
                FastNew.CreateInstance<IEnumerable>();
                Assert.Fail();
            }
            catch (MissingMethodException e)
            {
                Assert.IsTrue(e.Message.StartsWith("Cannot create an instance of an interface"));
            }
        }

        [Test()]
        public void ExceptionFastNewAbstract()
        {
            try
            {
                FastNew.CreateInstance<Stream>();
                Assert.Fail();
            }
            catch (MissingMethodException e)
            {
                Assert.IsTrue(e.Message.StartsWith("Cannot create an abstract class"));
            }
        }

        [Test()]
        public void ExceptionFastNewParameterless()
        {
            try
            {
                FastNew.CreateInstance<string>();
                Assert.Fail();
            }
            catch (MissingMethodException e)
            {
                Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
            }
        }

        [Test()]
        public void ExceptionFastNewWithParameter()
        {
            try
            {
                FastNew.CreateInstance<string, DBNull>(DBNull.Value);
                Assert.Fail();
            }
            catch (MissingMethodException e)
            {
                Assert.IsTrue(e.Message.StartsWith("No match constructor found in type"));
            }
        }

        #endregion

        #region ValueTypes

        [Test(Description = "Check is FastNew works on primary types")]
        [TestCaseSource(nameof(GetValueTypes))]
        [Parallelizable(ParallelScope.All)]
        public void ValueTypeDefault<T>(T defaultVal)
        {
            Assert.True(typeof(T).IsValueType, "T must be Value Type");

            var defaultValExpected = default(T);
            Assert.AreEqual(defaultValExpected, defaultVal);
            var fastNewInst = FastNew.NewOrDefault<T>();
            Assert.AreEqual(defaultVal, fastNewInst);
        }

        [Test(Description = "Check is FastNew works on primary types")]
        [TestCaseSource(nameof(GetValueTypes))]
        [Parallelizable(ParallelScope.All)]
        public void ValueTypeCreateInstance<T>(T defaultVal)
        {
            Assert.True(typeof(T).IsValueType, "T must be Value Type");

            // It seems only the first call of Activator.CreateInstance will call struct parameterless ctor on .NET Framework
            // But they are working well on .NET Core
            //Activator.CreateInstance<T>();

            var expected = Activator.CreateInstance<T>();
            var fastNewInst = FastNew.CreateInstance<T>();
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
            yield return default(DemoStructParameterless);
        }

        #endregion

        #region ParameterlessStructCtor

        [Test(Description = "Check is FastNew supports Parameterless Struct Constructor feature that added in C# 10")]
        public void ParameterlessStructCtor()
        {
            var expected = new DemoStructParameterless(); // This will call ctor
            var shouldEqual = FastNew.CreateInstance<DemoStructParameterless>(); // This should call ctor
            var shouldntEqual = FastNew.NewOrDefault<DemoStructParameterless>(); // This shouldn't call ctor
            Assert.AreEqual(expected, shouldEqual);
            Assert.AreNotEqual(expected, shouldntEqual);
        }

        #endregion
    }
}