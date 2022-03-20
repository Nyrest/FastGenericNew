namespace FastGenericNew.Benchmarks.Units;

public class ImplementationsBenchmark
{
#if NET6_0_OR_GREATER
    [Benchmark]
    public DemoClass ClrNew()
    {
        return ClrAllocator<DemoClass>.CreateInstance();
    }
#endif

    [Benchmark]
    public DemoClass FastNew()
    {
        return FastNew<DemoClass>.CompiledDelegate();
    }

    [Benchmark(Baseline = true)]
    public DemoClass Activator()
    {
        return System.Activator.CreateInstance<DemoClass>();
    }
}
