```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]   : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2
  .NET 8.0 : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2
  .NET 6.0 : .NET 6.0.22 (6.0.2223.42425), X64 RyuJIT AVX2


```
| Method                                | Job      | Runtime  | Mean       | Error    | StdDev    | Ratio | RatioSD | Rank | Gen0        | Gen1        | Allocated | Alloc Ratio |
|-------------------------------------- |--------- |--------- |-----------:|---------:|----------:|------:|--------:|-----:|------------:|------------:|----------:|------------:|
| CosineSimilarityVectors768Dimensions  | .NET 8.0 | .NET 8.0 |   945.7 ms |  5.86 ms |   5.48 ms |  0.46 |    0.01 |    1 | 374000.0000 | 373000.0000 |   2.93 GB |        1.00 |
| CosineSimilarityVectors768Dimensions  | .NET 6.0 | .NET 6.0 | 2,045.2 ms | 42.51 ms |  41.75 ms |  1.00 |    0.00 |    2 | 373000.0000 | 186000.0000 |   2.93 GB |        1.00 |
|                                       |          |          |            |          |           |       |         |      |             |             |           |             |
| DotProductVectors768Dimensions        | .NET 8.0 | .NET 8.0 |   804.2 ms |  4.04 ms |   3.78 ms |  0.43 |    0.01 |    1 | 374000.0000 | 373000.0000 |   2.93 GB |        1.00 |
| DotProductVectors768Dimensions        | .NET 6.0 | .NET 6.0 | 1,958.3 ms | 38.61 ms |  73.46 ms |  1.00 |    0.00 |    2 | 373000.0000 | 186000.0000 |   2.93 GB |        1.00 |
|                                       |          |          |            |          |           |       |         |      |             |             |           |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | .NET 8.0 | 1,919.5 ms | 12.35 ms |  11.55 ms |  0.53 |    0.02 |    1 | 741000.0000 | 740000.0000 |   5.79 GB |        1.00 |
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | .NET 6.0 | 3,705.7 ms | 72.96 ms | 113.60 ms |  1.00 |    0.00 |    2 | 740000.0000 | 376000.0000 |   5.79 GB |        1.00 |
|                                       |          |          |            |          |           |       |         |      |             |             |           |             |
| DotProductVectors1536Dimensions       | .NET 8.0 | .NET 8.0 | 1,665.3 ms |  7.29 ms |   6.82 ms |  0.48 |    0.02 |    1 | 741000.0000 | 740000.0000 |   5.79 GB |        1.00 |
| DotProductVectors1536Dimensions       | .NET 6.0 | .NET 6.0 | 3,488.2 ms | 67.56 ms |  94.71 ms |  1.00 |    0.00 |    2 | 740000.0000 | 377000.0000 |   5.79 GB |        1.00 |
