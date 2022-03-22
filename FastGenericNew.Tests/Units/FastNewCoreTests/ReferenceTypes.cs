namespace FastGenericNew.Tests.Units.FastNewCoreTests;

public class ReferenceTypes
{
    [Test]
    public void Object()
    {
        var expected = Activator.CreateInstance<object>();
        var actual = FastNew<object>.CompiledDelegate();
        Assert.IsTrue(expected.GetType() == actual.GetType());
    }

    [TestCaseSourceGenericAttribute(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = FastNew<T>.CompiledDelegate();
        Assert.AreEqual(expected, actual);
    }
}