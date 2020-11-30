using NUnit.Framework;

namespace FastGenericNew.Tests
{
    public class ConstructorOfTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Reference Types
        [Test]
        public void CO_Class()
        {
            var value = ConstructorOf<TClass>.value;
            Assert.NotNull(value);
        }

        [Test]
        public void CO_Class_PrivateConstructor()
        {
            var value = ConstructorOf<TClassPrivateConstructor, int, string>.value;
            Assert.NotNull(value);
        }

        [Test]
        public void CO_Class_Parameters()
        {
            var value = ConstructorOf<TClassWithParam, int, string>.value;
            Assert.NotNull(value);
        }
        #endregion

        #region Value Types
        [Test]
        public void CO_Struct_IsNull()
        {
            var value = ConstructorOf<TStruct>.value;
            Assert.IsNull(value);
        }

        [Test]
        public void CO_Struct_PrivateConstructor()
        {
            var value = ConstructorOf<TStructPrivateConstructor, int, string>.value;
            Assert.NotNull(value);
        }

        [Test]
        public void CO_Struct_Parameters()
        {
            var value = ConstructorOf<TStructWithParam, int, string>.value;
            Assert.NotNull(value);
        }
        #endregion
    }
}