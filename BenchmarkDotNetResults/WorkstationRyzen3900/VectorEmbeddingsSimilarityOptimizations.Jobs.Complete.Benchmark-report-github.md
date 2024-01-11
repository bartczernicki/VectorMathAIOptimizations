```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3803/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-GYLYIF : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                | NumberOfVectorsToCreate | Mean      | Error    | StdDev   | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|---------:|---------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | 182.65 ms | 0.921 ms | 0.861 ms | baseline |         | 
| Complete                              | 100000                  |  17.29 ms | 0.202 ms | 0.189 ms |     -91% |    1.1% | 
