# FastGenericNew [![GitHub](https://img.shields.io/github/license/BThree496/FastGenericNew?style=flat-square&logo=github)](https://github.com/BThree496/FastGenericNew/blob/master/LICENSE) [![Nuget](https://img.shields.io/nuget/v/Boring3.FastGenericNew?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Boring3.FastGenericNew/) [![Nuget](https://img.shields.io/nuget/dt/Boring3.FastGenericNew?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Boring3.FastGenericNew/)
<img src="./logo.png" alt="logo" width="90" height="90"/>

FastGenericNew is 10x times faster than `Activator.CreateInstance<T>()` / `new T()`

## Navigation
  - [Install](#install)
    - [DotNet CLI](#dotnet-cli)
    - [Package Reference](#package-reference)
    - [Package Manager](#package-manager)
  - [Features](#features)
  - [Examples](#examples)
    - [Fast create instance of `T`](#fast-create-instance-of-t)
    - [Fast create instance of `T` with parameter(s)](#fast-create-instance-of-t-with-parameters)
    - [Fast create instance by using TypeNew **(Experimental)**](#fast-create-instance-by-using-typenew-experimental)
  - [Benchmark](#benchmark)
    - [Environment](#environment)
    - [Reference Type (class)](#reference-type-class)
    - [Value Type (struct)](#value-type-struct)
    - [Legends](#legends)
  - [How it works](#how-it-works)
## Install

### DotNet CLI
```powershell
dotnet add package Boring3.FastGenericNew --version 2.0.0
```

### Package Reference
```xml
<PackageReference Include="Boring3.FastGenericNew" Version="2.0.0" />
```

### Package Manager
```powershell
Install-Package Boring3.FastGenericNew -Version 2.0.0
```

## Features

  - Parameters Supported
  - Non-Public Constructor Supported
  - Zero box/unbox
  - ValueType Supported
  - Source Generator
  - Fast Non-Generic TypeNew Support **(Experimental)**

## Examples

### Fast create instance of `T`

```cs
FastNew<T>.CreateInstance();
```

### Fast create instance of `T` with parameter(s)

```cs
FastNew<T, string>.CreateInstance("parameter");
FastNew<T, string, int>.CreateInstance("parameter", 0);
```

### Fast create instance by using TypeNew **(Experimental)**

```cs
// Slower a bit than FastNew<T> because boxing/unboxing.
// But still much faster than Activator.CreateInstance(typeof(Example))
Func<object> objectResult = TypeNew.GetCreateInstance(typeof(Example));

object result = objectResult();

// Performance equals FastNew<T>. Zero box/unbox
Func<Example> genericResult = TypeNew.GetCreateInstance<Example>(typeof(Example));
Func<string, Example> withGenericParameters = TypeNew.GetCreateInstance<Example, string>(typeof(Example), typeof(string));

Example result2 = genericResult("parameter");
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
|               Method |       Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD | Baseline |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------- |----------:|----------:|----------:|------:|--------:|--------- |-------:|------:|------:|----------:|
|             FastNewT |      .NET 4.8 |  8.400 ns | 0.1189 ns | 0.1112 ns |  1.00 |    0.00 |      Yes | 0.0145 |     - |     - |      24 B |
|            DirectNew |      .NET 4.8 |  1.741 ns | 0.0902 ns | 0.1483 ns |  0.21 |    0.02 |       No | 0.0145 |     - |     - |      24 B |
|      ActivatorCreate |      .NET 4.8 | 57.979 ns | 1.1526 ns | 1.2332 ns |  6.89 |    0.19 |       No | 0.0144 |     - |     - |      24 B |
| TypeNewGenericResult |      .NET 4.8 |  8.389 ns | 0.0864 ns | 0.0722 ns |  1.00 |    0.02 |       No | 0.0145 |     - |     - |      24 B |
|  TypeNewObjectResult |      .NET 4.8 |  8.093 ns | 0.0420 ns | 0.0393 ns |  0.96 |    0.01 |       No | 0.0145 |     - |     - |      24 B |
|                      |               |           |           |           |       |         |          |        |       |       |           |
|             FastNewT | .NET Core 5.0 |  2.234 ns | 0.0108 ns | 0.0090 ns |  1.00 |    0.00 |      Yes | 0.0029 |     - |     - |      24 B |
|            DirectNew | .NET Core 5.0 |  2.103 ns | 0.0452 ns | 0.0377 ns |  0.94 |    0.02 |       No | 0.0029 |     - |     - |      24 B |
|      ActivatorCreate | .NET Core 5.0 | 32.277 ns | 0.5308 ns | 0.4965 ns | 14.43 |    0.22 |       No | 0.0029 |     - |     - |      24 B |
| TypeNewGenericResult | .NET Core 5.0 |  2.309 ns | 0.0388 ns | 0.0344 ns |  1.03 |    0.02 |       No | 0.0029 |     - |     - |      24 B |
|  TypeNewObjectResult | .NET Core 5.0 |  2.495 ns | 0.0271 ns | 0.0253 ns |  1.12 |    0.01 |       No | 0.0029 |     - |     - |      24 B |

### **Value Type** (struct)
|               Method |       Runtime |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD | Baseline |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------- |-------------- |-----------:|----------:|----------:|-----------:|------:|--------:|--------- |-------:|------:|------:|----------:|
|             FastNewT |      .NET 4.8 |  0.5039 ns | 0.0087 ns | 0.0082 ns |  0.5002 ns |  1.00 |    0.00 |      Yes |      - |     - |     - |         - |
|            DirectNew |      .NET 4.8 |  0.1765 ns | 0.0035 ns | 0.0029 ns |  0.1767 ns |  0.35 |    0.01 |       No |      - |     - |     - |         - |
|      ActivatorCreate |      .NET 4.8 | 49.7076 ns | 0.5928 ns | 0.5545 ns | 49.8084 ns | 98.68 |    1.99 |       No | 0.0145 |     - |     - |      24 B |
| TypeNewGenericResult |      .NET 4.8 |  0.6239 ns | 0.0048 ns | 0.0038 ns |  0.6226 ns |  1.23 |    0.02 |       No |      - |     - |     - |         - |
|  TypeNewObjectResult |      .NET 4.8 |  8.4254 ns | 0.0709 ns | 0.0629 ns |  8.4143 ns | 16.71 |    0.29 |       No | 0.0145 |     - |     - |      24 B |
|                      |               |            |           |           |            |       |         |          |        |       |       |           |
|             FastNewT | .NET Core 5.0 |  0.4657 ns | 0.0012 ns | 0.0011 ns |  0.4652 ns | 1.000 |    0.00 |      Yes |      - |     - |     - |         - |
|            DirectNew | .NET Core 5.0 |  0.0023 ns | 0.0040 ns | 0.0037 ns |  0.0004 ns | 0.005 |    0.01 |       No |      - |     - |     - |         - |
|      ActivatorCreate | .NET Core 5.0 |  0.0123 ns | 0.0031 ns | 0.0028 ns |  0.0120 ns | 0.026 |    0.01 |       No |      - |     - |     - |         - |
| TypeNewGenericResult | .NET Core 5.0 |  0.4571 ns | 0.0053 ns | 0.0047 ns |  0.4564 ns | 0.982 |    0.01 |       No |      - |     - |     - |         - |
|  TypeNewObjectResult | .NET Core 5.0 |  2.2395 ns | 0.0115 ns | 0.0102 ns |  2.2374 ns | 4.811 |    0.02 |       No | 0.0029 |     - |     - |      24 B |

### Notes
> `new T()` will compile to `Activator.CreateInstance<T>()`

> JIT have two solutions for `Activator.CreateInstance<T>()` compilation.  
> For Reference Types. `Activator.CreateInstance<T>()` slow as everyone knows.  
> For Value Types. `Activator.CreateInstance<T>()` will allocate it inline. So it fast than  `FastNew` that unable to be inlined.

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

Not like `Activator.CreateInstance<T>()`. FastGenericNew will dynamically compile a method that return a real `new T()`. And cache it up by generic.

You can invoke this method by a delegate with no any box/unbox.

But there's still a little problem anyway.  
.NET Runtime will not inline delegate in any case currently.  
So it causes bit more costs than direct new.

```cs
public static class FastNew<T>
{
	  public static readonly Expression<Func<T>> SourceExpression =
        !typeof(T).IsValueType
        ? Expression.Lambda<Func<T>>(Expression.New(ConstructorOf<T>.value), Array.Empty<ParameterExpression>()) 
        : Expression.Lambda<Func<T>>(Expression.New(typeof(T)), Array.Empty<ParameterExpression>());

	  public static readonly Func<T> CreateInstance = SourceExpression.Compile();
}
```

```cs
public static class TypeNew
{
    public static Func<T> GetCreateInstance<T>(Type type) => 
        !type.IsValueType
        ? (Func<T>)
          typeof(FastNew<>)
          .MakeGenericType(type)
          .GetField("CreateInstance")
          .GetValue(null)
        : () => default(T);

    public static Func<object> GetCreateInstance(Type type)
    {
        if(!type.IsValueType)
        {
            return (Func<object>)
                typeof(FastNew<>)
                .MakeGenericType(type)
                .GetField("CreateInstance")
                .GetValue(null);
        }
        if (createInstanceCaches.TryGetValue(type, out var result))
            return result;
        createInstanceCaches.Add(type, 
            result = Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(type), typeof(object))).Compile());
        return result;
    }
}
```
