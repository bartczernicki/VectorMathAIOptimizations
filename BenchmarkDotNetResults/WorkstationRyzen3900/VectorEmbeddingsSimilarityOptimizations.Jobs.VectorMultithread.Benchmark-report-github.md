```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3803/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-QTIQYR : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                           | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |---------:|---------:|---------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 40.88 ms | 0.194 ms | 0.182 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  | 27.41 ms | 0.062 ms | 0.055 ms |     -33% |    0.4% | 
