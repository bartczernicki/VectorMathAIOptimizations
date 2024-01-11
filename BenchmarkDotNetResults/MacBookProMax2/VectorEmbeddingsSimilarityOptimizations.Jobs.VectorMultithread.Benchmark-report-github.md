```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-BWWQMY : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                           | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |---------:|---------:|---------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 45.58 ms | 0.211 ms | 0.176 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  | 14.57 ms | 0.206 ms | 0.182 ms |     -68% |    1.2% | 
