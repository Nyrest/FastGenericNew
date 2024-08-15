namespace FastGenericNew.Tests.Units.GetFastNewTests;

public class ValueTypes
{
    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonValueTypes))]
    [Parallelizable(ParallelScope.All)]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = FastNew.GetCreateInstance<T>(typeof(T))();
        Assert.AreEqual(expected, actual);
    }
}
