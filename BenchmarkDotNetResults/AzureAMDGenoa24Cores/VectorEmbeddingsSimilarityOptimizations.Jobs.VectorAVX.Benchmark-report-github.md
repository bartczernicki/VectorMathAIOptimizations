```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-LSEZMH : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  

```
| Method                                | NumberOfVectorsToCreate | AVXType     | Mean      | Error    | StdDev   | 
|-------------------------------------- |------------------------ |------------ |----------:|---------:|---------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | NonHardware | 167.51 ms | 0.304 ms | 0.285 ms | 
| CosineSimilarityVectors1536Dimensions | 100000                  | Vector512   |  21.24 ms | 0.184 ms | 0.172 ms | 
