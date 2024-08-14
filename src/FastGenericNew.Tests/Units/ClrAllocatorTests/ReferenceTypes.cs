#if AllowUnsafeImplementation && NET6_0_OR_GREATER
using System.Threading.Tasks;

namespace FastGenericNew.Tests.Units.ClrAllocatorTests;

public class ReferenceTypes
{
    [Test]
    public void Object()
    {
        if (!ClrAllocator<object>.IsSupported) Assert.Ignore("Unsupported");
        var expected = Activator.CreateInstance<object>();
        var actual = ClrAllocator<object>.CreateInstance();
        Assert.IsTrue(expected.GetType() == actual.GetType());
    }

    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
    [Parallelizable(ParallelScope.All)]
    public void CommonTypes<T>()
    {
        if (!ClrAllocator<T>.IsSupported) Assert.Ignore("Unsupported");
        var expected = Activator.CreateInstance<T>();
        var actual = ClrAllocator<T>.CreateInstance();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
    [Parallelizable(ParallelScope.All)]
    public void ParallelNew<T>()
    {
        if (!ClrAllocator<T>.IsSupported) Assert.Ignore("Unsupported");
        const int count = 512;
        T[] array = new T[count];
        Parallel.For(0, count, new ParallelOptions() { MaxDegreeOfParallelism = count }, i =>
        {
            array[i] = ClrAllocator<T>.CreateInstance();
        });
        var expected = Activator.CreateInstance<T>();
        foreach (var item in array)
        {
            Assert.AreEqual(expected, item);
        }
    }
}
#endif