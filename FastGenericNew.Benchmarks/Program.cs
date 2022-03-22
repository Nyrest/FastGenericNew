// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.ConsoleArguments.ListBenchmarks;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;

using FastGenericNew;
using FastGenericNew.Benchmarks;
using FastGenericNew.Benchmarks.Units;

ManualConfig config = ManualConfig.Create(DefaultConfig.Instance);
config.AddJob(
    //Job.Default.WithRuntime(ClrRuntime.Net48),
    //Job.Default.WithRuntime(CoreRuntime.Core50),
    Job.Default.WithRuntime(CoreRuntime.Core60));

config.AddDiagnoser(MemoryDiagnoser.Default);

//BenchmarkRunner.Run(Assembly.GetCallingAssembly(), config);
#if NET6_0_OR_GREATER
BenchmarkRunner.Run<ImplementationsBenchmark>(config);

#endif