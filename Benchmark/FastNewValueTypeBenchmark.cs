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
    public class FastNewValueTypeBenchmark
    {
        public static Func<ExampleValueType> typeNew;

        public static Func<object> typeNewBox;

        [GlobalSetup]
        public void SetUp()
        {
            typeNew = TypeNew.GetCreateInstance<ExampleValueType>(typeof(ExampleValueType));
            typeNewBox = TypeNew.GetCreateInstance(typeof(Example));
        }

        [Benchmark(Baseline = true)]
        public ExampleValueType FastNewT() =>
    FastNew<ExampleValueType>.CreateInstance();

        [Benchmark]
        public ExampleValueType DirectNew() => new();

        [Benchmark]
        public ExampleValueType ActivatorCreate() =>
            Activator.CreateInstance<ExampleValueType>();

        [Benchmark]
        public ExampleValueType TypeNewGenericResult() =>
            typeNew();

        [Benchmark]
        public object TypeNewObjectResult() =>
            typeNewBox();
    }

    public struct ExampleValueType { }
}