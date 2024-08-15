<div align="center">
  <a href="https://github.com/Nyrest/FastGenericNew"><img height="320" src="https://github.com/Nyrest/FastGenericNew/raw/main/Assets/FastGenericNew-Wide.svg"></a>
  
  <a href="https://github.com/Nyrest/FastGenericNew/actions/workflows/tests.yml"><img src="https://img.shields.io/github/actions/workflow/status/Nyrest/FastGenericNew/tests.yml?style=for-the-badge"></a>
  <a href="https://www.nuget.org/packages/FastGenericNew/"><img src="https://img.shields.io/nuget/vpre/FastGenericNew?style=for-the-badge&color=0065b3"></a>
  <a href="https://www.nuget.org/packages/FastGenericNew.SourceGenerator/"><img src="https://img.shields.io/nuget/vpre/FastGenericNew.SourceGenerator?label=SourceGenerator&style=for-the-badge&color=0065b3"></a>
</div>

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
dotnet add package FastGenericNew --version 3.1.0-preview1
```

```xml
<ItemGroup>
  <PackageReference Include="FastGenericNew" Version="3.1.0-preview1" />
</ItemGroup>
```

### SourceGenerator Version

```powershell
dotnet add package FastGenericNew.SourceGenerator --version 3.1.0-preview1
```

```xml
<ItemGroup>
  <PackageReference Include="FastGenericNew.SourceGenerator" Version="3.1.0-preview1" />
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

### **Environment**

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK=6.0.200-preview.22055.15
  [Host]             : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  .NET 5.0           : .NET 5.0.14 (5.0.1422.5710), X64 RyuJIT
  .NET 6.0           : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  .NET Framework 4.8 : .NET Framework 4.8 (4.8.4470.0), X64 RyuJIT
```

### Reference Types

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/Benchmark_ReferenceType.png)](https://github.com/Nyrest/FastGenericNew/blob/main/FastGenericNew.Benchmarks/Benchmarks/ReferenceTypeBenchmark.cs)

### Value Types

[![Benchmark Result of Value Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/Benchmark_ValueType.png)](https://github.com/Nyrest/FastGenericNew/blob/main/FastGenericNew.Benchmarks/Benchmarks/ValueTypeBenchmark.cs)

## 📜 License

FastGenericNew is licensed under the [MIT](LICENSE) license.
