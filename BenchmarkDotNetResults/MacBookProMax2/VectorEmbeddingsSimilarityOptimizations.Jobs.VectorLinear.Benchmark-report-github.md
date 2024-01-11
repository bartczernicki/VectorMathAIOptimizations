```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-AZKIPH : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  Alloc Ratio=  

```
| Method                                | NumberOfVectorsToCreate | Mean       | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |-----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 100                     |  0.0408 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 1000                    |  0.4263 ms | 0.0076 ms | 0.0071 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 10000                   |  4.1235 ms | 0.0171 ms | 0.0160 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 100000                  | 44.8590 ms | 0.1648 ms | 0.1541 ms | baseline |         | 
