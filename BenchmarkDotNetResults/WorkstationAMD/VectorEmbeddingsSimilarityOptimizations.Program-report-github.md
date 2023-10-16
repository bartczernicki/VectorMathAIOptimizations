```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]     : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-ZHNAUU : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-TGEDCQ : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2

RunStrategy=Throughput  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | MultiThreaded | Mean        | Error     | StdDev    | Median      | Ratio    | RatioSD | Allocated   | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |-------------- |------------:|----------:|----------:|------------:|---------:|--------:|------------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | False         |   0.3063 ms | 0.0007 ms | 0.0006 ms |   0.3066 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | False         |   0.3028 ms | 0.0012 ms | 0.0012 ms |   0.3035 ms |      -1% |    0.4% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | False         |   0.1284 ms | 0.0006 ms | 0.0005 ms |   0.1282 ms | baseline |         |    39.27 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | False         |   0.1273 ms | 0.0009 ms | 0.0009 ms |   0.1278 ms |      -1% |    0.6% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | False         |   0.5913 ms | 0.0033 ms | 0.0031 ms |   0.5938 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | False         |   0.5903 ms | 0.0030 ms | 0.0028 ms |   0.5918 ms |      -0% |    0.5% |    39.27 KB |         -0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | False         |   0.2418 ms | 0.0015 ms | 0.0014 ms |   0.2408 ms | baseline |         |    39.27 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | False         |   0.2408 ms | 0.0019 ms | 0.0018 ms |   0.2418 ms |      -0% |    1.0% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | True          |   0.1207 ms | 0.0015 ms | 0.0014 ms |   0.1204 ms | baseline |         |    76.19 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | True          |   0.1116 ms | 0.0010 ms | 0.0010 ms |   0.1113 ms |      -8% |    1.2% |    75.94 KB |         -0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | True          |   0.0975 ms | 0.0009 ms | 0.0009 ms |   0.0975 ms | baseline |         |     76.1 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | True          |   0.0924 ms | 0.0018 ms | 0.0021 ms |   0.0929 ms |      -5% |    2.5% |    75.93 KB |         -0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | True          |   0.1611 ms | 0.0015 ms | 0.0014 ms |   0.1610 ms | baseline |         |    78.17 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | True          |   0.1554 ms | 0.0011 ms | 0.0011 ms |   0.1553 ms |      -4% |    1.1% |    78.75 KB |         +1% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | True          |   0.1156 ms | 0.0012 ms | 0.0011 ms |   0.1155 ms | baseline |         |    79.23 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | True          |   0.1081 ms | 0.0020 ms | 0.0019 ms |   0.1086 ms |      -7% |    1.9% |    78.65 KB |         -1% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | False         | 393.4302 ms | 2.9006 ms | 2.7133 ms | 394.6280 ms |      +5% |    1.2% | 39063.09 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | False         | 374.9427 ms | 4.1334 ms | 3.8664 ms | 374.3711 ms | baseline |         | 39063.33 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | False         | 270.6168 ms | 2.7517 ms | 2.5740 ms | 270.7545 ms | baseline |         | 39064.99 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | False         | 269.9049 ms | 3.4365 ms | 3.2146 ms | 270.8859 ms |      -0% |    1.4% |  39062.9 KB |         -0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | False         | 722.3767 ms | 6.3384 ms | 5.9289 ms | 725.5625 ms |      +3% |    1.6% | 39063.09 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | False         | 698.5192 ms | 8.4067 ms | 7.8637 ms | 696.0700 ms | baseline |         | 39063.33 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | False         | 475.2863 ms | 5.4229 ms | 5.0726 ms | 476.1487 ms |      +1% |    1.6% | 39063.09 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | False         | 469.9557 ms | 5.9025 ms | 5.5212 ms | 471.1681 ms | baseline |         | 39064.05 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | True          | 192.0192 ms | 0.9872 ms | 0.9235 ms | 192.3269 ms |      +5% |    0.5% | 67878.36 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | True          | 182.8374 ms | 0.5636 ms | 0.5272 ms | 182.7237 ms | baseline |         |  68135.9 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | True          | 191.7474 ms | 1.1707 ms | 1.0951 ms | 191.8965 ms |      +5% |    0.8% | 67025.04 KB |         +0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | True          | 182.3730 ms | 0.7873 ms | 0.7365 ms | 182.1301 ms | baseline |         | 67024.86 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | True          | 289.7238 ms | 4.3736 ms | 4.0911 ms | 291.3406 ms |      +2% |    1.4% | 67369.56 KB |         +2% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | True          | 282.8171 ms | 0.9973 ms | 0.9329 ms | 282.7378 ms | baseline |         | 66345.64 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | True          | 289.9654 ms | 3.8602 ms | 3.6108 ms | 291.3546 ms |      +2% |    1.2% | 67369.65 KB |         -1% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | True          | 283.9595 ms | 1.3076 ms | 1.2231 ms | 283.5908 ms | baseline |         |  67882.7 KB |             |