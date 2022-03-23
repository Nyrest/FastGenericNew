using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FastGenericNew.Tests.Units.TypeNewTests
{
    public class ReferenceTypes
    {
        [Test]
        public void Object()
        {
            var expected = Activator.CreateInstance<object>();
            var actual = FastNew.GetCreateInstance<object>(typeof(object)).Invoke();
            Assert.IsTrue(expected.GetType() == actual.GetType());
        }

        [Test()]
        public void WithParameters1()
        {
            const int val = 99999;
            var expected = new DemoClass(val);
            var actual = FastNew.GetCreateInstance<object, int>(typeof(DemoClass), typeof(int)).Invoke(99999);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void WithParameters2()
        {
            const int val = 99999;
            const int val2 = 99999;
            var expected = new DemoClass(val, val2);
            var actual = FastNew.GetCreateInstance<DemoClass, int, int>(typeof(DemoClass), typeof(int), typeof(int)).Invoke(val, val2);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void WithParametersMany()
        {
            const int val = 11111;
            var expected = new DemoClass(val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val);
            var actual = FastNew.GetCreateInstance<DemoClass, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>
                  (typeof(DemoClass), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int))
                  (val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void PrivateCtor()
        {
            var expected = DemoClassPrivateCtor.Create();
            var actual = FastNew.GetCreateInstance<object>(typeof(DemoClassPrivateCtor)).Invoke();
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void PrivateCtorWithParameter()
        {
            const int val = 99999;

            var expected = DemoClassPrivateCtor.Create(val);
            var actual = FastNew.GetCreateInstance<object, int>(typeof(DemoClassPrivateCtor), typeof(int)).Invoke(val);
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
        [Parallelizable(ParallelScope.All)]
        public void CommonTypes(Type type)
        {
            var expected = Activator.CreateInstance(type);
            var actual = FastNew.GetCreateInstance<object>(type).Invoke();
            Assert.AreEqual(expected, actual);
        }
    }
}
