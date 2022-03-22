namespace FastGenericNew.Tests.Units.FastNewCoreTests;

public class ValueTypes
{
    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonValueTypes))]
    [Parallelizable(ParallelScope.All)]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = FastNew<T>.CompiledDelegate();
        Assert.AreEqual(expected, actual);
    }
}
