```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22621.3007/22H2/2022Update/SunValley2)
Apple Silicon, 8 CPU, 8 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  Job-XAOTFT : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Runtime=.NET 8.0  RunStrategy=Throughput  Gen0=9.2773  
Gen1=0.9766  Allocated=39.27 KB  

```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.4220 ms | 0.0009 ms | 0.0008 ms | baseline |         | 
| DotProductVectors1536Dimensions       | 1000                    | 0.3026 ms | 0.0006 ms | 0.0006 ms |     -28% |    0.3% | 
