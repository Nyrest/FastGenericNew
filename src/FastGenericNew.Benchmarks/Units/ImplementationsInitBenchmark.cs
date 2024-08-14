using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FastGenericNew.Benchmarks.Units;

#if NET6_0_OR_GREATER && FastNew_AllowUnsafeImplementation
public unsafe class ImplementationsInitBenchmark
{
    public static readonly delegate* managed<void> clrNew = typeof(ClrAllocator<DemoClass>).GetStaticCtor();

    public static readonly delegate* managed<void> ilNew = typeof(FastNew<DemoClass>).GetStaticCtor();

    public static readonly delegate* managed<object, Type, void> ctorActivatorCache =
        (delegate* managed<object, Type, void>)
        Type.GetType("System.RuntimeType")!
        .GetNestedType("ActivatorCache", BindingFlags.NonPublic)!
        .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { Type.GetType("System.RuntimeType")! }, null)!
        .MethodHandle
        .GetFunctionPointer();

    private static readonly object instActivatorCache = FormatterServices.GetUninitializedObject(Type.GetType("System.RuntimeType")!.GetNestedType("ActivatorCache", BindingFlags.NonPublic | BindingFlags.Instance)!);

    [Benchmark]
    public void InitClrNew()
    {
        clrNew();
    }

    [Benchmark(Baseline = true)]
    public void InitActivator()
    {
        ctorActivatorCache(instActivatorCache, typeof(DemoClass));
    }

    [Benchmark]
    public void InitFastNewCore()
    {
        ilNew();
    }
}
#endif
