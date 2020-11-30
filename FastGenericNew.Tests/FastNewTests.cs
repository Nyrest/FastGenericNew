using NUnit.Framework;

namespace FastGenericNew.Tests
{
    public class FastNewTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region ReferenceType FastNew Tests
        [Test]
        public void FN_Object()
        {
            var value = FastNew<object>.CreateInstance();
            Assert.NotNull(value);
        }

        [Test]
        public void FN_Class()
        {
            var value = FastNew<TClass>.CreateInstance();
            Assert.NotNull(value);
        }

        [Test]
        public void FN_Class_Parameters()
        {
            const int expectedNum = int.MaxValue;
            const string expectedText = "test";
            var value = FastNew<TClassWithParam, int, string>.CreateInstance(expectedNum, expectedText);
            Assert.Multiple(() =>
            {
                Assert.NotNull(value);
                Assert.AreEqual(expectedNum, value.i);
                Assert.AreEqual(expectedText, value.text);
            });
        }

        [Test]
        public void FN_Class_PrivateConstructor()
        {
            const int expectedNum = int.MaxValue;
            const string expectedText = "test";
            var value = FastNew<TClassPrivateConstructor, int, string>.CreateInstance(expectedNum, expectedText);
            Assert.Multiple(() =>
            {
                Assert.NotNull(value);
                Assert.AreEqual(expectedNum, value.i);
                Assert.AreEqual(expectedText, value.text);
            });
        }
        #endregion

        #region ValueType FastNew Tests
        [Test]
        public void FN_Struct()
        {
            var value = FastNew<TStruct>.CreateInstance();
            Assert.AreEqual(default(TStruct), value);
        }

        [Test]
        public void FN_Struct_Parameters()
        {
            const int expectedNum = int.MaxValue;
            const string expectedText = "test";
            var value = FastNew<TStructWithParam, int, string>.CreateInstance(expectedNum, expectedText);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedNum, value.i);
                Assert.AreEqual(expectedText, value.text);
            });
        }

        [Test]
        public void FN_Struct_PrivateConstructor()
        {
            const int expectedNum = int.MaxValue;
            const string expectedText = "test";
            var value = FastNew<TStructPrivateConstructor, int, string>.CreateInstance(expectedNum, expectedText);
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedNum, value.i);
                Assert.AreEqual(expectedText, value.text);
            });
        }
        #endregion
    }
}