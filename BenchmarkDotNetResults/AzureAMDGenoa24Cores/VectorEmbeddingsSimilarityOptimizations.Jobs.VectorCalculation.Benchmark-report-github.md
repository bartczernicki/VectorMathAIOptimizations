```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-FWYGLI : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  Gen0=0.7324  
Allocated=39.27 KB  

```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1189 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
| DotProductVectors1536Dimensions       | 1000                    | 0.0938 ms | 0.0001 ms | 0.0001 ms |     -21% |    0.1% | 
