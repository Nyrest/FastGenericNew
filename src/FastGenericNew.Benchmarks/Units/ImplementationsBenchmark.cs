namespace FastGenericNew.Benchmarks.Units;

#if NET6_0_OR_GREATER && FastNew_AllowUnsafeImplementation
public class ImplementationsBenchmark
{
    [Benchmark]
    public DemoClass ClrNew()
    {
        return ClrAllocator<DemoClass>.CreateInstance();
    }

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
#endif
