// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Loggers;

using FastGenericNew.Benchmarks;
using FastGenericNew.Benchmarks.Benchmarks;


ManualConfig config = ManualConfig.Create(DefaultConfig.Instance);
config.AddJob(
    //Job.Default.WithRuntime(ClrRuntime.Net48),
    //Job.Default.WithRuntime(CoreRuntime.Core50),
    Job.Default.WithRuntime(CoreRuntime.Core60));

config.AddDiagnoser(MemoryDiagnoser.Default);

//BenchmarkRunner.Run(Assembly.GetCallingAssembly(), config);
BenchmarkRunner.Run<ClrAllocatorBenchmark>(config);