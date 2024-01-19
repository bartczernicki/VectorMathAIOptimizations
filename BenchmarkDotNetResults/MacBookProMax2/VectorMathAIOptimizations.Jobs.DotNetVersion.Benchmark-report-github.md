```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-TVODVP : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-WIIMCL : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD

RunStrategy=Throughput  Allocated=3.81 MB  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |---------:|---------:|---------:|---------:|--------:|------------:|
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 100000                  | 45.24 ms | 0.357 ms | 0.334 ms |     +17% |    0.9% |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 100000                  | 38.52 ms | 0.155 ms | 0.145 ms | baseline |         |             |
