namespace FastGenericNew.Benchmarks.Units;

public class ReferenceTypeBenchmark
{
    [Benchmark]
    public DemoClass FastNew()
    {
        return FastGenericNew.FastNew.CreateInstance<DemoClass>();
    }


    [Benchmark(Baseline = true)]
    public DemoClass Activator()
    {
        return System.Activator.CreateInstance<DemoClass>();
    }
}
