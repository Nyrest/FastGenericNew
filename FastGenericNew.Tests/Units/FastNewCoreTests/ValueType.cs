namespace FastGenericNew.Tests.Units.FastNewCoreTests;

public class ValueTypes
{
    [TestCaseSourceGenericAttribute(typeof(TestData), nameof(TestData.CommonValueTypes))]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = FastNew<T>.CompiledDelegate();
        Assert.AreEqual(expected, actual);
    }
}
