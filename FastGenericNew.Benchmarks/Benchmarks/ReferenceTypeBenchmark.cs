namespace FastGenericNew.Benchmarks.Benchmarks;

public class ReferenceTypeBenchmark
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
