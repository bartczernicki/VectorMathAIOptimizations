```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-EAWRSJ : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                           | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |---------:|---------:|---------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 45.97 ms | 0.201 ms | 0.178 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  | 14.70 ms | 0.147 ms | 0.137 ms |     -68% |    1.0% | 
