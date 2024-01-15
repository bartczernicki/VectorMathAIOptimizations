```

BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-SOGLOA : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  Alloc Ratio=  

```
| Method                                | NumberOfVectorsToCreate | Mean       | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |-----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 100                     |  0.0115 ms | 0.0000 ms | 0.0000 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 1000                    |  0.1191 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 10000                   |  1.1939 ms | 0.0027 ms | 0.0025 ms | baseline |         | 
|                                       |                         |            |           |           |          |         | 
| CosineSimilarityVectors1536Dimensions | 100000                  | 21.8083 ms | 0.1558 ms | 0.1457 ms | baseline |         | 
