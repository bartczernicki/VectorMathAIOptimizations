```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3930/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-FNCYYH : .NET 6.0.26 (6.0.2623.60508), X64 RyuJIT AVX2
  Job-YNEMMA : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

RunStrategy=Throughput  Gen0=307.6923  Allocated=3.81 MB  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |---------:|---------:|---------:|---------:|--------:|------------:|
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 100000                  | 40.89 ms | 0.160 ms | 0.142 ms | baseline |         |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 100000                  | 40.68 ms | 0.170 ms | 0.159 ms |      -1% |    0.5% |         -0% |
