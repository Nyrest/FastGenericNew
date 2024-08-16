namespace FastGenericNew.Benchmarks.Units;

public class ReferenceTypeWithParamsBenchmark
{
    [Benchmark]
    public DemoClassParam FastNewWithParams1()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoClassParam, int>(1);
    }

    [Benchmark(Baseline = true)]
    public DemoClassParam ActivatorWithParams1()
    {
        return (DemoClassParam)System.Activator.CreateInstance(typeof(DemoClassParam), 1)!;
    }

    [Benchmark]
    public DemoClassParam FastNewWithParams10()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoClassParam, int, int, int, int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    }

    [Benchmark]
    public DemoClassParam ActivatorWithParams10()
    {
        return (DemoClassParam)System.Activator.CreateInstance(typeof(DemoClassParam), 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)!;
    }
}
