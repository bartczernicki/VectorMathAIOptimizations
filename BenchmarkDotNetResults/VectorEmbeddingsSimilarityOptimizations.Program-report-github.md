```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]     : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-CLJCUA : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  Job-JXNYSG : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2

RunStrategy=Throughput  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | MultiThreaded | Mean          | Error      | StdDev      | Median        | Ratio    | RatioSD | Allocated     | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |-------------- |--------------:|-----------:|------------:|--------------:|---------:|--------:|--------------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | False         |     0.5528 ms |  0.0027 ms |   0.0025 ms |     0.5518 ms | baseline |         |    3062.69 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | False         |     0.4674 ms |  0.0017 ms |   0.0036 ms |     0.4669 ms |     -15% |    0.5% |    3062.69 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | False         |     0.3603 ms |  0.0033 ms |   0.0027 ms |     0.3600 ms | baseline |         |    3062.69 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | False         |     0.2892 ms |  0.0021 ms |   0.0045 ms |     0.2888 ms |     -20% |    1.0% |    3062.69 KB |         +0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | False         |     1.3816 ms |  0.0053 ms |   0.0047 ms |     1.3829 ms | baseline |         |    6062.69 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | False         |     1.3612 ms |  0.0046 ms |   0.0041 ms |     1.3623 ms |      -1% |    0.4% |    6062.69 KB |         +0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | False         |     1.0705 ms |  0.0034 ms |   0.0030 ms |     1.0704 ms | baseline |         |    6062.69 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | False         |     1.0548 ms |  0.0056 ms |   0.0053 ms |     1.0538 ms |      -1% |    0.5% |    6062.69 KB |         +0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | True          |     0.1235 ms |  0.0016 ms |   0.0015 ms |     0.1231 ms | baseline |         |      76.13 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | True          |     0.1205 ms |  0.0010 ms |   0.0009 ms |     0.1207 ms |      -2% |    1.3% |      75.71 KB |         -1% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | True          |     0.1004 ms |  0.0016 ms |   0.0015 ms |     0.1006 ms | baseline |         |      76.08 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | True          |     0.0908 ms |  0.0007 ms |   0.0006 ms |     0.0909 ms |      -9% |    1.6% |      76.03 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | True          |     0.1661 ms |  0.0016 ms |   0.0015 ms |     0.1658 ms | baseline |         |      78.24 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | True          |     0.1636 ms |  0.0032 ms |   0.0031 ms |     0.1647 ms |      -2% |    2.3% |      78.63 KB |         +0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | True          |     0.1205 ms |  0.0010 ms |   0.0010 ms |     0.1208 ms | baseline |         |      79.33 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | True          |     0.1159 ms |  0.0020 ms |   0.0019 ms |     0.1159 ms |      -4% |    1.8% |      78.77 KB |         -1% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | False         | 2,015.8360 ms | 39.7259 ms |  54.3773 ms | 2,032.1744 ms | baseline |         | 3062500.81 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | False         |   936.8554 ms |  5.2404 ms |   4.9019 ms |   938.9386 ms |     -53% |    3.4% | 3062500.58 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | False         | 1,868.5756 ms | 37.0778 ms |  51.9779 ms | 1,884.6432 ms | baseline |         | 3062500.81 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | False         |   788.1315 ms |  3.7800 ms |   3.5358 ms |   787.6049 ms |     -57% |    3.8% | 3062500.58 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | False         | 3,851.6263 ms | 76.0654 ms | 177.8003 ms | 3,935.0944 ms | baseline |         | 6062500.81 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | False         | 1,885.5339 ms | 10.6724 ms |   9.4608 ms | 1,886.7943 ms |     -47% |    3.9% | 6062500.58 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | False         | 3,616.9977 ms | 71.8515 ms | 141.8279 ms | 3,637.8882 ms | baseline |         | 6062500.81 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | False         | 1,618.9111 ms |  7.6547 ms |   6.7857 ms | 1,619.1923 ms |     -53% |    3.3% | 6062500.58 KB |         -0% |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | True          |   195.9720 ms |  0.6936 ms |   0.6149 ms |   196.1004 ms |      +6% |    0.5% |   68646.41 KB |         +2% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | True          |   184.4644 ms |  0.8382 ms |   0.7431 ms |   184.4654 ms | baseline |         |   67367.39 KB |             |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | True          |   196.3446 ms |  0.5448 ms |   0.5096 ms |   196.2320 ms |      +6% |    0.4% |    66769.1 KB |         -1% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | True          |   185.0679 ms |  0.6136 ms |   0.5439 ms |   185.1226 ms | baseline |         |    67366.8 KB |             |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | True          |   292.9724 ms |  4.4237 ms |   4.1379 ms |   294.7284 ms |      +3% |    1.4% |   67369.54 KB |         +2% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | True          |   284.7661 ms |  0.9821 ms |   0.9187 ms |   284.6882 ms | baseline |         |   66090.27 KB |             |
|                                       |          |                         |               |               |            |             |               |          |         |               |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | True          |   293.5293 ms |  4.6564 ms |   4.3556 ms |   295.5763 ms |      +3% |    1.6% |   66857.63 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | True          |   284.8714 ms |  0.7499 ms |   0.7015 ms |   285.0104 ms | baseline |         |   66859.14 KB |             |
