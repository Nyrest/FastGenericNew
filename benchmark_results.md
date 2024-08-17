# Benchmark Results

FastGenericNew 3.3.1

## Environment

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

## Reference Types

### Parameterless

| Method    | Runtime              | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------- |--------------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| FastNew   | .NET 5.0             |  2.044 ns | 0.0787 ns | 0.0697 ns |  0.06 |    0.00 | 0.0029 |      24 B |        1.00 |
| Activator | .NET 5.0             | 35.792 ns | 0.3509 ns | 0.3283 ns |  1.00 |    0.01 | 0.0029 |      24 B |        1.00 |
|           |                      |           |           |           |       |         |        |           |             |
| FastNew   | .NET 6.0             |  2.312 ns | 0.1038 ns | 0.1153 ns |  0.18 |    0.01 | 0.0029 |      24 B |        1.00 |
| Activator | .NET 6.0             | 12.680 ns | 0.1223 ns | 0.1084 ns |  1.00 |    0.01 | 0.0029 |      24 B |        1.00 |
|           |                      |           |           |           |       |         |        |           |             |
| FastNew   | .NET 8.0             |  3.525 ns | 0.0248 ns | 0.0193 ns |  0.42 |    0.01 | 0.0029 |      24 B |        1.00 |
| Activator | .NET 8.0             |  8.425 ns | 0.1288 ns | 0.1142 ns |  1.00 |    0.02 | 0.0029 |      24 B |        1.00 |
|           |                      |           |           |           |       |         |        |           |             |
| FastNew   | .NET 9.0             |  3.629 ns | 0.0487 ns | 0.0432 ns |  0.31 |    0.00 | 0.0029 |      24 B |        1.00 |
| Activator | .NET 9.0             | 11.843 ns | 0.1229 ns | 0.1150 ns |  1.00 |    0.01 | 0.0029 |      24 B |        1.00 |
|           |                      |           |           |           |       |         |        |           |             |
| FastNew   | .NET Framework 4.8.1 | 13.143 ns | 0.0417 ns | 0.0326 ns |  0.27 |    0.00 | 0.0038 |      24 B |        1.00 |
| Activator | .NET Framework 4.8.1 | 49.409 ns | 0.2104 ns | 0.1968 ns |  1.00 |    0.01 | 0.0038 |      24 B |        1.00 |

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_reference.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ReferenceTypeBenchmark.cs)

### With parameters

| Method                | Runtime              | Mean         | Error      | StdDev     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------- |--------------------- |-------------:|-----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| FastNewWithParams1    | .NET 5.0             |     2.243 ns |  0.0764 ns |  0.0677 ns | 0.004 |    0.00 | 0.0029 |      24 B |        0.05 |
| ActivatorWithParams1  | .NET 5.0             |   522.914 ns |  3.6361 ns |  3.2233 ns | 1.000 |    0.01 | 0.0534 |     448 B |        1.00 |
| FastNewWithParams10   | .NET 5.0             |     4.868 ns |  0.0299 ns |  0.0265 ns | 0.009 |    0.00 | 0.0029 |      24 B |        0.05 |
| ActivatorWithParams10 | .NET 5.0             | 1,120.768 ns | 11.0389 ns | 10.3257 ns | 2.143 |    0.02 | 0.1163 |     984 B |        2.20 |
|                       |                      |              |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 6.0             |     2.576 ns |  0.0787 ns |  0.0736 ns | 0.007 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams1  | .NET 6.0             |   355.775 ns |  2.4181 ns |  2.2619 ns | 1.000 |    0.01 | 0.0496 |     416 B |        1.00 |
| FastNewWithParams10   | .NET 6.0             |     4.293 ns |  0.1423 ns |  0.1332 ns | 0.012 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams10 | .NET 6.0             |   912.066 ns |  7.5947 ns |  6.7325 ns | 2.564 |    0.02 | 0.1173 |     984 B |        2.37 |
|                       |                      |              |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 8.0             |     3.932 ns |  0.1058 ns |  0.0990 ns |  0.02 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams1  | .NET 8.0             |   221.870 ns |  0.8220 ns |  0.7287 ns |  1.00 |    0.00 | 0.0458 |     384 B |        1.00 |
| FastNewWithParams10   | .NET 8.0             |     5.863 ns |  0.0890 ns |  0.0789 ns |  0.03 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams10 | .NET 8.0             |   467.913 ns |  3.5657 ns |  2.9775 ns |  2.11 |    0.01 | 0.1011 |     848 B |        2.21 |
|                       |                      |              |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 9.0             |     3.787 ns |  0.0156 ns |  0.0138 ns |  0.02 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams1  | .NET 9.0             |   228.589 ns |  2.0384 ns |  1.9067 ns |  1.00 |    0.01 | 0.0458 |     384 B |        1.00 |
| FastNewWithParams10   | .NET 9.0             |     5.876 ns |  0.0407 ns |  0.0361 ns |  0.03 |    0.00 | 0.0029 |      24 B |        0.06 |
| ActivatorWithParams10 | .NET 9.0             |   471.132 ns |  2.6444 ns |  2.2082 ns |  2.06 |    0.02 | 0.1011 |     848 B |        2.21 |
|                       |                      |              |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET Framework 4.8.1 |    13.704 ns |  0.0271 ns |  0.0226 ns |  0.02 |    0.00 | 0.0038 |      24 B |        0.05 |
| ActivatorWithParams1  | .NET Framework 4.8.1 |   709.988 ns |  2.3010 ns |  2.0397 ns |  1.00 |    0.00 | 0.0725 |     457 B |        1.00 |
| FastNewWithParams10   | .NET Framework 4.8.1 |    15.957 ns |  0.0701 ns |  0.0656 ns |  0.02 |    0.00 | 0.0038 |      24 B |        0.05 |
| ActivatorWithParams10 | .NET Framework 4.8.1 | 1,548.438 ns |  3.6276 ns |  3.3933 ns |  2.18 |    0.01 | 0.1564 |     995 B |        2.18 |

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_reference_params.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ReferenceTypeWithParamsBenchmark.cs)

## Value types

### Parameterless

| Method    | Runtime              | Mean       | Error     | StdDev    | Median     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------- |--------------------- |-----------:|----------:|----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| FastNew   | .NET 5.0             |  0.0027 ns | 0.0027 ns | 0.0024 ns |  0.0017 ns |     ? |       ? |      - |         - |           ? |
| Activator | .NET 5.0             |  0.0017 ns | 0.0032 ns | 0.0030 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
|           |                      |            |           |           |            |       |         |        |           |             |
| FastNew   | .NET 6.0             |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
| Activator | .NET 6.0             |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
|           |                      |            |           |           |            |       |         |        |           |             |
| FastNew   | .NET 8.0             |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
| Activator | .NET 8.0             |  0.0000 ns | 0.0000 ns | 0.0000 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
|           |                      |            |           |           |            |       |         |        |           |             |
| FastNew   | .NET 9.0             |  0.0006 ns | 0.0014 ns | 0.0012 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
| Activator | .NET 9.0             |  0.0017 ns | 0.0035 ns | 0.0032 ns |  0.0000 ns |     ? |       ? |      - |         - |           ? |
|           |                      |            |           |           |            |       |         |        |           |             |
| FastNew   | .NET Framework 4.8.1 |  1.0006 ns | 0.0023 ns | 0.0020 ns |  0.9995 ns |  0.03 |    0.00 |      - |         - |        0.00 |
| Activator | .NET Framework 4.8.1 | 38.2332 ns | 0.2768 ns | 0.2312 ns | 38.1499 ns |  1.00 |    0.01 | 0.0038 |      24 B |        1.00 |

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_value.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ValueTypeBenchmark.cs)

### With parameters

| Method                | Runtime              | Mean          | Error      | StdDev     | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------- |--------------------- |--------------:|-----------:|-----------:|------:|--------:|-------:|----------:|------------:|
| FastNewWithParams1    | .NET 5.0             |     0.4783 ns |  0.0034 ns |  0.0032 ns | 0.001 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams1  | .NET 5.0             |   529.3377 ns |  2.1951 ns |  2.0533 ns | 1.000 |    0.01 | 0.0534 |     448 B |        1.00 |
| FastNewWithParams10   | .NET 5.0             |     2.1353 ns |  0.0060 ns |  0.0047 ns | 0.004 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams10 | .NET 5.0             | 1,104.6093 ns |  6.0086 ns |  5.3264 ns | 2.087 |    0.01 | 0.1163 |     984 B |        2.20 |
|                       |                      |               |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 6.0             |     0.7093 ns |  0.0177 ns |  0.0165 ns | 0.002 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams1  | .NET 6.0             |   367.3118 ns |  3.4310 ns |  3.2094 ns | 1.000 |    0.01 | 0.0496 |     416 B |        1.00 |
| FastNewWithParams10   | .NET 6.0             |     2.0898 ns |  0.0181 ns |  0.0169 ns | 0.006 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams10 | .NET 6.0             |   897.9462 ns |  2.5277 ns |  2.1108 ns | 2.445 |    0.02 | 0.1173 |     984 B |        2.37 |
|                       |                      |               |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 8.0             |     0.4809 ns |  0.0029 ns |  0.0026 ns | 0.002 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams1  | .NET 8.0             |   216.1378 ns |  1.7263 ns |  1.5303 ns | 1.000 |    0.01 | 0.0458 |     384 B |        1.00 |
| FastNewWithParams10   | .NET 8.0             |     2.1464 ns |  0.0096 ns |  0.0090 ns | 0.010 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams10 | .NET 8.0             |   485.1434 ns |  1.5189 ns |  1.2683 ns | 2.245 |    0.02 | 0.1011 |     848 B |        2.21 |
|                       |                      |               |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET 9.0             |     0.4804 ns |  0.0028 ns |  0.0026 ns | 0.002 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams1  | .NET 9.0             |   219.7569 ns |  2.3458 ns |  2.1943 ns | 1.000 |    0.01 | 0.0458 |     384 B |        1.00 |
| FastNewWithParams10   | .NET 9.0             |     2.0939 ns |  0.0366 ns |  0.0342 ns | 0.010 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams10 | .NET 9.0             |   453.3459 ns |  4.1613 ns |  3.6889 ns | 2.063 |    0.03 | 0.1011 |     848 B |        2.21 |
|                       |                      |               |            |            |       |         |        |           |             |
| FastNewWithParams1    | .NET Framework 4.8.1 |    12.2150 ns |  0.1013 ns |  0.0948 ns |  0.02 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams1  | .NET Framework 4.8.1 |   751.5832 ns |  3.2805 ns |  2.5612 ns |  1.00 |    0.00 | 0.0725 |     457 B |        1.00 |
| FastNewWithParams10   | .NET Framework 4.8.1 |    14.3714 ns |  0.1837 ns |  0.1718 ns |  0.02 |    0.00 |      - |         - |        0.00 |
| ActivatorWithParams10 | .NET Framework 4.8.1 | 1,582.2390 ns | 21.1942 ns | 19.8251 ns |  2.11 |    0.03 | 0.1564 |     995 B |        2.18 |

[![Benchmark Result of Reference Types](https://raw.githubusercontent.com/Nyrest/FastGenericNew/main/Assets/benchmark_value_params.png)](https://github.com/Nyrest/FastGenericNew/blob/main/src/FastGenericNew.Benchmarks/Units/ValueTypeWithParamsBenchmark.cs)
