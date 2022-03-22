#if AllowUnsafeImplementation && NET6_0_OR_GREATER
namespace FastGenericNew.Tests.Units.ClrAllocatorTests;

public class ReferenceTypes
{
    [Test]
    public void Object()
    {
        var expected = Activator.CreateInstance<object>();
        var actual = ClrAllocator<object>.CreateInstance();
        Assert.IsTrue(expected.GetType() == actual.GetType());
    }

    [TestCaseSourceGenericAttribute(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = ClrAllocator<T>.CreateInstance();
        Assert.AreEqual(expected, actual);
    }
}
#endif