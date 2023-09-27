```

BenchmarkDotNet v0.13.8, Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
Apple Silicon, 6 CPU, 6 logical and 6 physical cores
.NET SDK 8.0.100-rc.1.23455.8
  [Host]     : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD
  Job-LJEFFU : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD
  Job-ECAQZQ : .NET 8.0.0 (8.0.23.41904), Arm64 RyuJIT AdvSIMD

RunStrategy=Throughput  WarmupCount=1  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | MultiThreaded | Mean        | Error     | StdDev    | Ratio    | RatioSD | Allocated   | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |-------------- |------------:|----------:|----------:|---------:|--------:|------------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | False         |   0.2355 ms | 0.0008 ms | 0.0007 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | False         |   0.2244 ms | 0.0012 ms | 0.0011 ms |      -5% |    0.5% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | False         |   0.1461 ms | 0.0006 ms | 0.0006 ms |      +0% |    0.5% |    39.27 KB |         +0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | False         |   0.1460 ms | 0.0004 ms | 0.0004 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | False         |   0.4408 ms | 0.0014 ms | 0.0013 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | False         |   0.4381 ms | 0.0022 ms | 0.0021 ms |      -1% |    0.4% |    39.27 KB |         -0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | False         |   0.3321 ms | 0.0016 ms | 0.0014 ms | baseline |         |    39.27 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | False         |   0.3156 ms | 0.0017 ms | 0.0016 ms |      -5% |    0.7% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | True          |   0.1057 ms | 0.0004 ms | 0.0003 ms |      +2% |    0.8% |    71.92 KB |         -3% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | True          |   0.1037 ms | 0.0008 ms | 0.0008 ms | baseline |         |    74.46 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | True          |   0.0878 ms | 0.0007 ms | 0.0006 ms |      +2% |    0.9% |    72.06 KB |         -4% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | True          |   0.0861 ms | 0.0008 ms | 0.0007 ms | baseline |         |    75.23 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | True          |   0.1641 ms | 0.0006 ms | 0.0006 ms |      +0% |    1.0% |    74.57 KB |         -2% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | True          |   0.1638 ms | 0.0016 ms | 0.0015 ms | baseline |         |    75.79 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | True          |   0.1330 ms | 0.0009 ms | 0.0008 ms | baseline |         |    76.71 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | True          |   0.1321 ms | 0.0003 ms | 0.0003 ms |      -1% |    0.8% |    75.63 KB |         -1% |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | False         | 292.3124 ms | 5.6425 ms | 6.0374 ms |     +10% |    2.6% |  39062.9 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | False         | 266.9450 ms | 2.6328 ms | 2.4627 ms | baseline |         | 39063.02 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | False         | 199.5093 ms | 3.6220 ms | 3.3881 ms |      +8% |    2.0% | 39062.83 KB |         -0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | False         | 184.5838 ms | 1.3463 ms | 1.1935 ms | baseline |         | 39063.39 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | False         | 487.2323 ms | 1.1223 ms | 0.8762 ms |      +2% |    0.9% | 39063.09 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | False         | 478.7710 ms | 4.4492 ms | 4.1618 ms | baseline |         | 39064.89 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | False         | 362.4808 ms | 1.6766 ms | 1.3090 ms |      +5% |    0.5% | 39063.09 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | False         | 343.4845 ms | 1.3445 ms | 1.1919 ms | baseline |         | 39064.89 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | True          | 123.6587 ms | 2.3879 ms | 2.2336 ms |      +0% |    1.9% | 63265.52 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | True          | 122.9211 ms | 1.6172 ms | 1.4336 ms | baseline |         |  63265.7 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | True          | 109.8093 ms | 0.6324 ms | 0.5606 ms |      +4% |    1.7% |  63265.7 KB |         +0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | True          | 105.3831 ms | 1.8347 ms | 1.7161 ms | baseline |         | 63265.66 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | True          | 181.0131 ms | 2.1116 ms | 1.9752 ms |      +2% |    1.7% | 63268.65 KB |         -1% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | True          | 176.8727 ms | 1.7010 ms | 1.5911 ms | baseline |         | 63951.92 KB |             |
|                                       |          |                         |               |             |           |           |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | True          | 152.5003 ms | 3.0115 ms | 2.8170 ms |      +2% |    2.8% | 63268.56 KB |         -1% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | True          | 149.6067 ms | 2.2947 ms | 2.1464 ms | baseline |         | 63781.05 KB |             |
