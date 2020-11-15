# FastGenericNew [![GitHub](https://img.shields.io/github/license/BThree496/FastGenericNew?style=flat-square&logo=github)](https://github.com/BThree496/FastGenericNew/blob/master/LICENSE) [![Nuget](https://img.shields.io/nuget/v/Boring3.FastGenericNew?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Boring3.FastGenericNew/) [![Nuget](https://img.shields.io/nuget/dt/Boring3.FastGenericNew?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Boring3.FastGenericNew/)
<img src="./logo.png" alt="logo" width="90" height="90"/>

FastGenericNew is 10x times faster than `Activator.CreateInstance<T>()` / `new T()`

## Install

### DotNet CLI
```powershell
dotnet add package Boring3.FastGenericNew --version 1.1.0
```

### Package Reference
```xml
<PackageReference Include="Boring3.FastGenericNew" Version="1.1.0" />
```

### Package Manager
```powershell
Install-Package Boring3.FastGenericNew -Version 1.1.0
```

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

### **Environment**
``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
```

### **Reference Type** (class)
|          Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Baseline |  Gen 0 | Gen 1 | Gen 2 | Allocated | Code Size |
|---------------- |----------:|----------:|----------:|------:|--------:|--------- |-------:|------:|------:|----------:|----------:|
|       DirectNew |  1.757 ns | 0.0083 ns | 0.0074 ns |  0.79 |    0.01 |       No | 0.0029 |     - |     - |      24 B |      25 B |
|        FastNewT |  2.223 ns | 0.0121 ns | 0.0108 ns |  1.00 |    0.00 |      Yes | 0.0029 |     - |     - |      24 B |      24 B |
| ActivatorCreate | 32.653 ns | 0.4308 ns | 0.4030 ns | 14.69 |    0.18 |       No | 0.0029 |     - |     - |      24 B |      88 B |
|            NewT | 32.717 ns | 0.6927 ns | 0.7977 ns | 14.70 |    0.37 |       No | 0.0029 |     - |     - |      24 B |      88 B |

### **Value Type** (struct)
|          Method |       Mean |     Error |    StdDev |     Median |  Ratio | RatioSD | Baseline |  Gen 0 | Gen 1 | Gen 2 | Allocated | Code Size |
|---------------- |-----------:|----------:|----------:|-----------:|-------:|--------:|--------- |-------:|------:|------:|----------:|----------:|
|       DirectNew |  0.0014 ns | 0.0011 ns | 0.0010 ns |  0.0014 ns |  0.004 |    0.00 |       No |      - |     - |     - |         - |       3 B |
|            NewT |  0.0102 ns | 0.0130 ns | 0.0121 ns |  0.0023 ns |  0.020 |    0.03 |       No |      - |     - |     - |         - |       3 B |
|        FastNewT |  0.4629 ns | 0.0099 ns | 0.0077 ns |  0.4613 ns |  1.000 |    0.00 |      Yes |      - |     - |     - |         - |      24 B |
| ActivatorCreate | 33.6680 ns | 0.7328 ns | 0.6855 ns | 33.7002 ns | 72.346 |    1.36 |       No | 0.0029 |     - |     - |      24 B |      88 B |

> **Note:** JIT have two solutions for `new T()` compilation.  
> For Reference Types. `new T()` will equals `Activator.CreateInstance<T>()`  
> For Value Types. `new T()` will allocate it inline. So it fast than  `FastNew` that unable to be inlined.

### **Legends**

>  `Mean`      : Arithmetic mean of all measurements  
>  `Error`     : Half of 99.9% confidence interval  
>  `StdDev`    : Standard deviation of all measurements  
>  `Median`    : Value separating the higher half of all measurements (50th percentile)  
>  `Ratio`     : Mean of the ratio distribution ([Current]/[Baseline])  
> ` RatioSD`   : Standard deviation of the ratio distribution ([Current]/[Baseline])  
>  `Gen 0`     : GC Generation 0 collects per 1000 operations  
>  `Gen 1`     : GC Generation 1 collects per 1000 operations  
>  `Gen 2`     : GC Generation 2 collects per 1000 operations  
>  `Allocated` : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
>  `Code Size` : Native code size of the disassembled method(s)  
>  `1 ns`      : 1 Nanosecond (0.000000001 sec)  

## How it works

Not like `Activator.CreateInstance<T>()`. FastGenericNew will dynamically compile a method that return `T`. And cache it up by generic.

You can invoke this method by a delegate with no any box/unbox.

But there's still a little problem anyway.  
.NET Runtime will not inline delegate in any case currently.  
So it causes bit more costs than direct new.