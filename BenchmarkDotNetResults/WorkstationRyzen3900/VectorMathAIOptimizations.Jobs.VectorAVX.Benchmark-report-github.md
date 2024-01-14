```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.19045.3930/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.101
  [Host]     : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2
  Job-YJQHAV : .NET 8.0.1 (8.0.123.58001), X64 RyuJIT AVX2

Runtime=.NET 8.0  RunStrategy=Throughput  Allocated=3.81 MB  

```
| Method                                | NumberOfVectorsToCreate | AVXType     | Mean      | Error    | StdDev   | 
|-------------------------------------- |------------------------ |------------ |----------:|---------:|---------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | NonHardware | 184.78 ms | 1.018 ms | 0.952 ms | 
| CosineSimilarityVectors1536Dimensions | 100000                  | Vector128   |  40.38 ms | 0.211 ms | 0.197 ms | 
