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

    [Test()]
    public void WithParameters1()
    {
        const int val = 99999;
        var expected = new DemoClass(val);
        var actual = FastNew<DemoClass, int>.CompiledDelegate(val);
        Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void WithParameters2()
    {
        const int val = 99999;
        const int val2 = 99999;
        var expected = new DemoClass(val, val2);
        var actual = FastNew<DemoClass, int, int>.CompiledDelegate(val, val2);
        Assert.AreEqual(expected, actual);
    }

    [Test()]
    public void WithParametersMany()
    {
        const int val = 11111;
        var expected = new DemoClass(val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val);
        var actual = FastNew<DemoClass, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int>.CompiledDelegate(val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val, val);
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSourceGeneric(typeof(TestData), nameof(TestData.CommonReferenceTypesPL))]
    [Parallelizable(ParallelScope.All)]
    public void CommonTypes<T>()
    {
        var expected = Activator.CreateInstance<T>();
        var actual = FastNew<T>.CompiledDelegate();
        Assert.AreEqual(expected, actual);
    }
}