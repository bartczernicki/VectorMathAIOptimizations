```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3930/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-OKWVLJ : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                           | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |---------:|---------:|---------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 39.86 ms | 0.238 ms | 0.222 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  | 28.24 ms | 0.251 ms | 0.235 ms |     -29% |    1.0% | 
