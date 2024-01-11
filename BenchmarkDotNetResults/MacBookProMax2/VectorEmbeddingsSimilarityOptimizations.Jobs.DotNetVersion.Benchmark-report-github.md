```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-VUJCAW : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-ASZIMF : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD

RunStrategy=Throughput  Allocated=3.81 MB  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |---------:|---------:|---------:|---------:|--------:|------------:|
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 100000                  | 45.03 ms | 0.275 ms | 0.257 ms |     +15% |    0.9% |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 100000                  | 39.30 ms | 0.205 ms | 0.181 ms | baseline |         |             |
