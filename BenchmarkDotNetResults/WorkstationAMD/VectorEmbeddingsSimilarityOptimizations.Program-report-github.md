```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]     : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-XUDDTI : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-XCLJET : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2

RunStrategy=Throughput  WarmupCount=1  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | MultiThreaded | Mean        | Error     | StdDev    | Ratio    | RatioSD | Allocated   | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |-------------- |------------:|----------:|----------:|---------:|--------:|------------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | False         |   0.3065 ms | 0.0011 ms | 0.0011 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | False         |   0.3024 ms | 0.0017 ms | 0.0014 ms |      -1% |    0.5% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | False         |   0.1270 ms | 0.0009 ms | 0.0008 ms |      +0% |    0.7% |    39.27 KB |         +0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | False         |   0.1268 ms | 0.0006 ms | 0.0005 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | False         |   0.5871 ms | 0.0040 ms | 0.0036 ms |      +1% |    0.5% |    39.27 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | False         |   0.5838 ms | 0.0032 ms | 0.0027 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | False         |   0.2385 ms | 0.0022 ms | 0.0021 ms |      +1% |    0.9% |    39.27 KB |         +0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | False         |   0.2366 ms | 0.0020 ms | 0.0018 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | True          |   0.1230 ms | 0.0013 ms | 0.0012 ms | baseline |         |    76.14 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | True          |   0.1130 ms | 0.0007 ms | 0.0006 ms |      -8% |    0.9% |    76.03 KB |         -0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | True          |   0.0976 ms | 0.0010 ms | 0.0009 ms | baseline |         |    76.06 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | True          |   0.0927 ms | 0.0010 ms | 0.0010 ms |      -5% |    1.1% |    75.94 KB |         -0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | True          |   0.1603 ms | 0.0013 ms | 0.0012 ms | baseline |         |    78.16 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | True          |   0.1535 ms | 0.0012 ms | 0.0011 ms |      -4% |    1.0% |    78.75 KB |         +1% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | True          |   0.1177 ms | 0.0013 ms | 0.0012 ms | baseline |         |    79.24 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | True          |   0.1087 ms | 0.0008 ms | 0.0007 ms |      -8% |    1.0% |    78.72 KB |         -1% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | False         | 385.1567 ms | 4.8236 ms | 4.2760 ms |      +1% |    1.8% | 39063.09 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | False         | 380.2923 ms | 4.4561 ms | 4.1682 ms | baseline |         | 39064.38 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | False         | 269.8538 ms | 3.1176 ms | 2.7637 ms | baseline |         | 39063.02 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | False         | 268.0051 ms | 2.8181 ms | 2.6360 ms |      -1% |    1.4% |  39062.9 KB |         -0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | False         | 713.6307 ms | 6.8750 ms | 6.4308 ms |      +3% |    1.2% | 39063.09 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | False         | 696.2044 ms | 7.6118 ms | 7.1201 ms | baseline |         | 39064.38 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | False         | 472.8700 ms | 5.8414 ms | 5.4640 ms |      +1% |    1.6% | 39063.09 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | False         | 468.8515 ms | 5.6024 ms | 5.2405 ms | baseline |         | 39064.77 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | True          | 191.8403 ms | 1.3087 ms | 1.1601 ms |      +5% |    0.9% | 68433.01 KB |         +4% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | True          | 182.2348 ms | 0.8596 ms | 0.8040 ms | baseline |         | 65830.88 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | True          | 192.0453 ms | 1.6466 ms | 1.5402 ms |      +4% |    1.1% | 69158.41 KB |         +2% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | True          | 184.8951 ms | 1.4062 ms | 1.3153 ms | baseline |         | 68049.25 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | True          | 289.3509 ms | 3.7399 ms | 3.4983 ms |      +2% |    1.4% | 65833.45 KB |         -3% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | True          | 283.2634 ms | 0.5851 ms | 0.4568 ms | baseline |         | 67881.96 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | True          | 291.7917 ms | 3.9913 ms | 3.1162 ms |      +3% |    1.1% | 66377.56 KB |         -2% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | True          | 282.6733 ms | 0.8705 ms | 0.8143 ms | baseline |         |  67882.9 KB |             |
