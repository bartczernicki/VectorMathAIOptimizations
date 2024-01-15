```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-EEPCWH : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                | NumberOfVectorsToCreate | Mean       | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |-----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | 184.217 ms | 0.3123 ms | 0.2768 ms | baseline |         | 
| Complete                              | 100000                  |   9.509 ms | 0.0840 ms | 0.0786 ms |     -95% |    0.8% | 
