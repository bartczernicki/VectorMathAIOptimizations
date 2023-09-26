```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]     : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.41904), X64 RyuJIT AVX2


```
| Method                                | Mean       | Error   | StdDev  | Rank | Gen0        | Gen1        | Allocated |
|-------------------------------------- |-----------:|--------:|--------:|-----:|------------:|------------:|----------:|
| DotProductVectors768Dimensions        |   813.6 ms | 9.36 ms | 7.81 ms |    1 | 374000.0000 | 373000.0000 |   2.93 GB |
| CosineSimilarityVectors768Dimensions  |   946.3 ms | 4.99 ms | 4.67 ms |    2 | 374000.0000 | 373000.0000 |   2.93 GB |
| DotProductVectors1536Dimensions       | 1,648.2 ms | 6.28 ms | 5.57 ms |    3 | 741000.0000 | 740000.0000 |   5.79 GB |
| CosineSimilarityVectors1536Dimensions | 1,898.6 ms | 8.76 ms | 8.19 ms |    4 | 741000.0000 | 740000.0000 |   5.79 GB |
