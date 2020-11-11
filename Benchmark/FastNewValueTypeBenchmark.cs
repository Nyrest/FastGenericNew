using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using FastGenericNew;
#pragma warning disable CA1822 // Member does not access instance data and can be marked as static

namespace Benchmark
{
    [StopOnFirstError]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    [BaselineColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class FastNewValueTypeBenchmark
    {
        [Benchmark]
        public ExampleValueType DirectNew() =>
            new ExampleValueType();

        [Benchmark(Baseline = true)]
        public ExampleValueType FastNewT() =>
            Test<ExampleValueType>.FastNew();

        [Benchmark]
        public ExampleValueType NewT() =>
            Test<ExampleValueType>.New();

        [Benchmark]
        public Example ActivatorCreate() =>
            Test<Example>.ActivatorCreate();
    }

    public static class TestValueType<T> where T : new()
    {
        public static T FastNew() => FastNew<T>.CreateInstance();

        public static T New() => new T();

        public static T ActivatorCreate() => Activator.CreateInstance<T>();
    }

    public struct ExampleValueType { }
}