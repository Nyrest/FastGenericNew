namespace FastGenericNew.Benchmarks.Benchmarks;

public class ValueTypeBenchmark
{
    [Benchmark]
    public DemoStruct FastNew()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoStruct>();
    }

    [Benchmark]
    public DemoStruct Activator()
    {
        return System.Activator.CreateInstance<DemoStruct>();
    }
}
