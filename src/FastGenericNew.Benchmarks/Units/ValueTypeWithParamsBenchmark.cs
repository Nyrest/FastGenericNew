namespace FastGenericNew.Benchmarks.Units;

public class ValueTypeWithParamsBenchmark
{
    [Benchmark]
    public DemoStructParam FastNewWithParams1()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoStructParam, int>(1);
    }

    [Benchmark(Baseline = true)]
    public DemoStructParam ActivatorWithParams1()
    {
        return (DemoStructParam)System.Activator.CreateInstance(typeof(DemoStructParam), 1)!;
    }

    [Benchmark]
    public DemoStructParam FastNewWithParams10()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoStructParam, int, int, int, int, int, int, int, int, int, int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    }

    [Benchmark]
    public DemoStructParam ActivatorWithParams10()
    {
        return (DemoStructParam)System.Activator.CreateInstance(typeof(DemoStructParam), 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)!;
    }
}
