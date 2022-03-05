namespace FastGenericNew.Benchmarks.Benchmarks;

public class ReferenceTypeBenchmark
{
    [Benchmark]
    public DemoClass FastNew()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoClass>();
    }

    [Benchmark]
    public DemoClass Activator()
    {
        return System.Activator.CreateInstance<DemoClass>();
    }
}
