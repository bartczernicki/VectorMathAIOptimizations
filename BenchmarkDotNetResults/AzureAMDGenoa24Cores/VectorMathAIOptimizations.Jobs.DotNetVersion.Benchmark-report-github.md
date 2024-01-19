```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-ITIBZZ : .NET 6.0.26 (6.0.2623.60508), X64 RyuJIT AVX2
  Job-RYZFGJ : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

RunStrategy=Throughput  Gen0=62.5000  Gen1=31.2500  
Allocated=3.81 MB  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |---------:|---------:|---------:|---------:|--------:|------------:|
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 100000                  | 22.83 ms | 0.313 ms | 0.293 ms | baseline |         |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 100000                  | 21.63 ms | 0.325 ms | 0.304 ms |      -5% |    1.6% |         -0% |
