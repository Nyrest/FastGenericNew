using NUnit.Framework;

namespace FastGenericNew.Tests
{
    public class TypeNewTests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Object
        [Test]
        public void TN_Object()
        {
            var createInstance = TypeNew.GetCreateInstance(typeof(object));
            Assert.NotNull(createInstance);
        }

        [Test]
        public void TN_Object_Generic()
        {
            var createInstance = TypeNew.GetCreateInstance<object>(typeof(object));
            Assert.NotNull(createInstance);
        }
        #endregion


        #region Reference Types
        [Test]
        public void TN_Class()
        {
            var createInstance = TypeNew.GetCreateInstance(typeof(TClass));
            Assert.NotNull(createInstance());
        }

        [Test]
        public void TN_Class_Parameters()
        {
            const int expectedNum = int.MaxValue;
            const string expectedText = "test";
            var createInstance = TypeNew.GetCreateInstance<object, int, string>(typeof(TClassWithParam), typeof(int), typeof(string));
            Assert.Multiple(() =>
            {
                var obj = createInstance(expectedNum, expectedText);
                var value = obj as TClassWithParam;
                Assert.IsInstanceOf<TClassWithParam>(obj);
                Assert.NotNull(value);
                Assert.AreEqual(expectedNum, value.i);
                Assert.AreSame(expectedText, value.text);
            });
        }

        [Test]
        public void TN_Class_Generic()
        {
            var createInstance = TypeNew.GetCreateInstance<TClass>(typeof(TClass));
            Assert.NotNull(createInstance());
        }

        [Test]
        public void TN_Class_Generic_DerivedClass()
        {
            var createInstance = TypeNew.GetCreateInstance<TAbstractClass>(typeof(TDerivedClass));
            Assert.NotNull(createInstance());
        }
        #endregion

        #region Reference Types
        [Test]
        public void TN_Struct()
        {

            var createInstance = TypeNew.GetCreateInstance(typeof(TStruct));
            Assert.NotNull(createInstance());
        }

        [Test]
        public void TN_Struct_Generic()
        {
            var createInstance = TypeNew.GetCreateInstance<TStruct>(typeof(TStruct));
            Assert.NotNull(createInstance());
        }


        #endregion
    }
}
