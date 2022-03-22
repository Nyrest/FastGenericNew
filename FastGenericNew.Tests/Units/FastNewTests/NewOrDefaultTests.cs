namespace FastGenericNew.Tests.Units.FastNewTests;

public class NewOrDefaultTests
{
    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonValueTypes))]
    [Parallelizable(ParallelScope.All)]
    public void ValueTypeDefault<T>()
    {
        Assert.True(typeof(T).IsValueType, "T must be Value Type");

        var expected = default(T);
        var actual = FastNew.NewOrDefault<T>();
        Assert.AreEqual(expected, actual);
    }
}
