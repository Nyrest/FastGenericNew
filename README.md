# FastGenericNew ![GitHub](https://img.shields.io/github/license/BThree496/FastGenericNew?style=flat-square&logo=github) ![Nuget](https://img.shields.io/nuget/v/Boring3.FastGenericNew?style=flat-square&logo=nuget) ![Nuget](https://img.shields.io/nuget/dt/Boring3.FastGenericNew?style=flat-square&logo=nuget)

FastGenericNew is 10x times faster than `Activator.CreateInstance<T>()` / `new T()`

## Features

  - Parameters Supported
  - Non-Public Constructor Supported
  - Zero box/unbox
  - ValueType Supported

## Examples

Fast create instance of `T`:

```cs
FastNew<T>.CreateInstance();
```

Fast create instance of `T` with parameter(s):

```cs
FastNew<T, string>.CreateInstance("parameter");
FastNew<T, string, int>.CreateInstance("parameter", 0);
```

## Benchmark

```ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=5.0.100-rc.2.20479.15
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.47505, CoreFX 5.0.20.47505), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.47505, CoreFX 5.0.20.47505), X64 RyuJIT


```

|    Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Baseline |  Gen 0 | Gen 1 | Gen 2 | Allocated | Code Size |
|---------- |----------:|----------:|----------:|------:|--------:|--------- |-------:|------:|------:|----------:|----------:|
| DirectNew |  1.793 ns | 0.0206 ns | 0.0193 ns |  0.79 |    0.01 |       No | 0.0029 |     - |     - |      24 B |      25 B |
|  FastNewT |  2.262 ns | 0.0076 ns | 0.0071 ns |  1.00 |    0.00 |      Yes | 0.0029 |     - |     - |      24 B |      24 B |
|      NewT | 33.332 ns | 0.3524 ns | 0.3296 ns | 14.74 |    0.16 |       No | 0.0029 |     - |     - |      24 B |      88 B |

## How it works

Not like `Activator.CreateInstance<T>()`. FastGenericNew will dynamic compile a method that return `T`. And cache it up by generic.

You can invoke this method by a delegate with no any box/unbox.

But there's still a little problem anyway.  
.NET Runtime will not inline delegate in any case currently. So it cause bit more costs than direct new.
