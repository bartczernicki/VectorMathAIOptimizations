```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-IZXKBD : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                           | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 21.403 ms | 0.1642 ms | 0.1536 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  |  6.503 ms | 0.0834 ms | 0.0780 ms |     -70% |    1.6% | 
