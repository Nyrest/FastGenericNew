﻿// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;

ManualConfig config = ManualConfig.Create(DefaultConfig.Instance);
config.AddJob(
    Job.Default.WithRuntime(MonoRuntime.Default),
    Job.Default.WithRuntime(ClrRuntime.Net48),
    Job.Default.WithRuntime(CoreRuntime.Core50),
    Job.Default.WithRuntime(CoreRuntime.Core60));

config.AddDiagnoser(MemoryDiagnoser.Default);

BenchmarkRunner.Run(Assembly.GetCallingAssembly(), config);