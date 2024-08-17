# FastGenericNew [![Tests](https://github.com/Nyerst/FastGenericNew/actions/workflows/tests.yml/badge.svg)](https://github.com/Nyerst/FastGenericNew/actions/workflows/tests.yml) [![](https://img.shields.io/nuget/vpre/FastGenericNew)](https://www.nuget.org/packages/FastGenericNew/) [![](https://img.shields.io/nuget/vpre/FastGenericNew.SourceGenerator?label=SourceGenerator)](https://www.nuget.org/packages/FastGenericNew.SourceGenerator/)

The ultimate fast and powerful alternative to `Activator.CreateInstance<T>` / `new T()`

## ✨ Features

- ✔️ **The best** `CreateInstance` ever
  - Up to 50x faster than `Activator.CreateInstance<T>`
  - Generic Parameters Support
  - Zero boxing/unboxing
  - TryGetValue-like TryFastNew API
  - Link Mode `PublishTrimmed` Support
  - Non-Public Constructor Support
  - No Generic Constraints
  - Compatible with .NET Standard 2.0
  - Multiple backend implementations
  - Heavily tested on Win/Mac/Linux

- 🪛 **Modern** Compiler Integration
  - Source Generator v2 (Incremental Generator)
  - Highly Configurable ([Props](https://github.com/Nyrest/FastGenericNew/wiki/SourceGenerator-Options))
  - Multi-threaded Generation

- 🔥 **Latest** C#/.NET Features Support
  - [C# 8 Nullable](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types) Support
  - [C# 10 Parameterless struct constructors](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-10.0/parameterless-struct-constructors) Support (Both invokes or not)
  - WebAssembly Support

## 🔧 Installation

> You should only use one of them

### Pre-Compiled Version

```powershell
dotnet add package FastGenericNew --version 3.3.1
```

```xml
<ItemGroup>
  <PackageReference Include="FastGenericNew" Version="3.3.1" />
</ItemGroup>
```

### SourceGenerator Version

```powershell
dotnet add package FastGenericNew.SourceGenerator --version 3.3.1
```

```xml
<ItemGroup>
  <PackageReference Include="FastGenericNew.SourceGenerator" Version="3.3.1" />
</ItemGroup>
```
#### SourceGeneratorV2 requires
> ***.NET Standard 2.0*** or above  
> ***C# 8.0*** or above  
> ***Roslyn 4.0.1*** or above  
> ***Modern IDE*** *(Optional)*  [VS2022, Rider, VSCode]

## 📖 Examples

```cs
using FastGenericNew;

// Simply replace 'Activator' to 'FastNew'
var obj = FastNew.CreateInstance<T>();

// With parameter(s)
var obj2 = FastNew.CreateInstance<T, string>("text");
var obj3 = FastNew.CreateInstance<T, string, int>("text", 0);

// Try pattern
// NOTE: The try pattern will only check if the constructor can be called.
//       It will not catch or handle any exceptions thrown in the constructor.
if (FastNew.TryCreateInstance<T, string>("arg0", out T result));
{
    // ...
}
```

### Notes

> **With .NET Framework**, `Activator.CreateInstance<T>()` invokes the parameterless constructor of **ValueType** if  
> the constraint is `where T : new()` but appears to **ignore the parameterless constructor if the constraint is `where T : struct`**.  
> **But `FastNew.CreateInstance<T>()` will always invoke the parameterless constructor if it's available.**  
> 
> If you don't want to invoke the parameterless constructor of **ValueType**,
> consider using `FastNew.NewOrDefault<T>()` which **will never invoke the parameterless constructor of `ValueType`**

## 🚀 Benchmark  

Check the full benchmark results here:  
https://github.com/Nyrest/FastGenericNew/blob/main/benchmark_results.md

### **Environment**

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.3958/23H2/2023Update/SunValley3)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 9.0.100-preview.7.24407.12
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  Job-GFVMQQ : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
  Job-FGYWFO : .NET 6.0.33 (6.0.3324.36610), X64 RyuJIT AVX2
  Job-LODQQQ : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  Job-NXJWMD : .NET 9.0.0 (9.0.24.40507), X64 RyuJIT AVX2
  Job-VBBRLS : .NET Framework 4.8.1 (4.8.9256.0), X64 RyuJIT VectorSize=256
```

### Reference Types

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_reference.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ReferenceTypeBenchmark.cs)

### Value Types

[![Benchmark Result of Value Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_value.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ValueTypeBenchmark.cs)

## 📜 License

FastGenericNew is licensed under the [MIT](LICENSE) license.
