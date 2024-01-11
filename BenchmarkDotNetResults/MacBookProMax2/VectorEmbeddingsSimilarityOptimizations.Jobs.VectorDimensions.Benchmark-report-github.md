```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-NMBFWY : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  Allocated=39.27 KB  

```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.4216 ms | 0.0028 ms | 0.0024 ms | baseline |         | 
| CosineSimilarityVectors768Dimensions  | 1000                    | 0.1862 ms | 0.0012 ms | 0.0010 ms |     -56% |    0.7% | 
