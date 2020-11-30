using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FastGenericNew;
using System;
#pragma warning disable CA1822 // Member does not access instance data and can be marked as static

namespace Benchmark
{
    [StopOnFirstError]
    [MemoryDiagnoser]
    [BaselineColumn]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    //[Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class FastNewBenchmark
    {
        public static Func<Example> typeNew;

        public static Func<object> typeNewBox;

        [GlobalSetup]
        public void SetUp()
        {
            typeNew = TypeNew.GetCreateInstance<Example>(typeof(Example));
            typeNewBox = TypeNew.GetCreateInstance(typeof(Example));
        }

        [Benchmark(Baseline = true)]
        public Example FastNewT() =>
    FastNew<Example>.CreateInstance();

        [Benchmark]
        public Example DirectNew() => new();

        [Benchmark]
        public Example ActivatorCreate() =>
            Activator.CreateInstance<Example>();

        [Benchmark]
        public Example TypeNewGenericResult() =>
            typeNew();

        [Benchmark]
        public object TypeNewObjectResult() =>
            typeNewBox();
    }

    public class Example { }
}