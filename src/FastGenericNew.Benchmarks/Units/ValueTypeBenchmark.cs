namespace FastGenericNew.Benchmarks.Units;

public class ValueTypeBenchmark
{
    [Benchmark]
    public DemoStruct FastNew()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoStruct>();
    }

    [Benchmark(Baseline = true)]
    public DemoStruct Activator()
    {
        return System.Activator.CreateInstance<DemoStruct>();
    }
}
