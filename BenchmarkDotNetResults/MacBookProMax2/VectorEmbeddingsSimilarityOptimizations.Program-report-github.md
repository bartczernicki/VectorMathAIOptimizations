```

BenchmarkDotNet v0.13.8, Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
Apple Silicon, 6 CPU, 6 logical and 6 physical cores
.NET SDK 8.0.100-rc.1.23455.8
  [Host]     : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD
  Job-YBEDGU : .NET 6.0.22 (6.0.2223.42425), Arm64 RyuJIT AdvSIMD
  Job-GELIDF : .NET 8.0.0 (8.0.23.41904), Arm64 RyuJIT AdvSIMD

RunStrategy=Throughput  

```
| Method                                | Runtime  | NumberOfVectorsToCreate | MultiThreaded | Mean        | Error     | StdDev    | Median      | Ratio    | RatioSD | Allocated   | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |-------------- |------------:|----------:|----------:|------------:|---------:|--------:|------------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | False         |   0.2353 ms | 0.0012 ms | 0.0011 ms |   0.2356 ms | baseline |         |    39.27 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | False         |   0.2262 ms | 0.0004 ms | 0.0004 ms |   0.2263 ms |      -4% |    0.5% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | False         |   0.1477 ms | 0.0003 ms | 0.0003 ms |   0.1476 ms | baseline |         |    39.27 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | False         |   0.1475 ms | 0.0003 ms | 0.0003 ms |   0.1476 ms |      -0% |    0.2% |    39.27 KB |         +0% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | False         |   0.4456 ms | 0.0009 ms | 0.0008 ms |   0.4454 ms |      +0% |    0.2% |    39.27 KB |         +0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | False         |   0.4438 ms | 0.0009 ms | 0.0007 ms |   0.4438 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | False         |   0.3571 ms | 0.0013 ms | 0.0013 ms |   0.3570 ms |     +12% |    0.4% |    39.27 KB |         +0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | False         |   0.3185 ms | 0.0009 ms | 0.0008 ms |   0.3184 ms | baseline |         |    39.27 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000                    | True          |   0.1048 ms | 0.0009 ms | 0.0009 ms |   0.1048 ms | baseline |         |    74.52 KB |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000                    | True          |   0.1043 ms | 0.0004 ms | 0.0003 ms |   0.1043 ms |      -0% |    0.9% |    70.95 KB |         -5% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000                    | True          |   0.0864 ms | 0.0008 ms | 0.0008 ms |   0.0865 ms | baseline |         |    74.93 KB |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000                    | True          |   0.0847 ms | 0.0004 ms | 0.0003 ms |   0.0848 ms |      -2% |    1.0% |    72.16 KB |         -4% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000                    | True          |   0.1622 ms | 0.0012 ms | 0.0011 ms |   0.1622 ms | baseline |         |    75.87 KB |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000                    | True          |   0.1620 ms | 0.0008 ms | 0.0008 ms |   0.1619 ms |      -0% |    0.8% |    74.61 KB |         -2% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000                    | True          |   0.1339 ms | 0.0008 ms | 0.0007 ms |   0.1337 ms | baseline |         |    76.91 KB |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000                    | True          |   0.1304 ms | 0.0008 ms | 0.0007 ms |   0.1306 ms |      -3% |    0.8% |    75.34 KB |         -2% |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | False         | 281.7803 ms | 3.4215 ms | 3.2004 ms | 281.4372 ms |      +8% |    1.4% |  39062.9 KB |         -0% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | False         | 262.0112 ms | 1.7921 ms | 1.5886 ms | 261.9370 ms | baseline |         | 39064.99 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | False         | 197.5768 ms | 3.2380 ms | 2.8704 ms | 197.3574 ms |      +9% |    1.4% | 39062.83 KB |         -0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | False         | 181.4985 ms | 0.7334 ms | 0.6124 ms | 181.6763 ms | baseline |         |  39063.1 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | False         | 491.1610 ms | 3.2617 ms | 2.8914 ms | 490.7992 ms |      +4% |    0.9% | 39063.09 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | False         | 473.7806 ms | 2.4490 ms | 2.2908 ms | 474.2347 ms | baseline |         | 39066.85 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | False         | 357.0805 ms | 1.2232 ms | 1.0214 ms | 356.8300 ms |      +3% |    1.5% | 39063.09 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | False         | 347.0677 ms | 4.7244 ms | 4.4192 ms | 346.6868 ms | baseline |         | 39068.65 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | 1000000                 | True          | 123.4407 ms | 1.1684 ms | 1.0929 ms | 123.9510 ms |      +0% |    1.4% | 63265.51 KB |         -1% |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | 1000000                 | True          | 123.3767 ms | 1.3949 ms | 1.3048 ms | 123.7474 ms | baseline |         |  63675.6 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors768Dimensions        | .NET 8.0 | 1000000                 | True          | 107.3904 ms | 1.6445 ms | 1.4578 ms | 107.6592 ms |     +13% |    2.3% | 63265.49 KB |         -0% |
| DotProductVectors768Dimensions        | .NET 6.0 | 1000000                 | True          |  95.2796 ms | 1.8819 ms | 2.0136 ms |  95.3884 ms | baseline |         | 63265.78 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 1000000                 | True          | 181.6991 ms | 1.5628 ms | 1.4618 ms | 181.8397 ms |      +9% |    1.4% | 63268.66 KB |         -0% |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 1000000                 | True          | 166.6190 ms | 2.0890 ms | 1.9540 ms | 167.2212 ms | baseline |         | 63270.56 KB |             |
|                                       |          |                         |               |             |           |           |             |          |         |             |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | 1000000                 | True          | 161.5478 ms | 2.7044 ms | 7.2651 ms | 158.4919 ms |      +8% |    4.2% | 63268.54 KB |         -0% |
| DotProductVectors1536Dimensions       | .NET 6.0 | 1000000                 | True          | 149.2791 ms | 1.4833 ms | 1.3875 ms | 149.0629 ms | baseline |         | 63269.41 KB |             |
