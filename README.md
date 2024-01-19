**Benchmark Vector Math AI Optimizations**  
is a .NET console application that benchmarks various vector math techniques used in AI applications and their impact on performance. Implementing all of the perscribed options can improve performance of baseline applications up to 98%! Those improvements can be improved even further ~99% using Aproximate Nearest Neighbor data structures like HNSW.
Running the Application & Features:
- Required: .NET 8 SDK (.NET 6 SDK is only required to build & run .NET 6 vs 8 runtime performance benchmark) with Visual Studio 2022 (tested on 17.9 Preview 2.1)  
- Not Required: No OpenAI, Azure OpenAI nor other "AI or cloud" keys required. Benchmark was done to mimic performance using simple internal generated (mocked) vectors  
- Running the Application: Build the application in Visual Studio IDE, Select run in "Release" mode (required for benchmarking), select the benchmark number and click enter to run  
- Tweaking or Configuring: Each benchmark is a seperate DLL (for seperation concerns), which can be configured individually (i.e. amount of vectors) or optimized seperately with compiler directives. This allows for individual reports to be viewed and analyzed
- Real Data (1M vectors) located: https://huggingface.co/datasets/KShivendu/dbpedia-entities-openai-1M   
- Uses HNSW algorithm from Microsoft (fork optimized): https://github.com/bartczernicki/hnsw-sharp  
![Benchmark Process](https://github.com/bartczernicki/VectorEmbeddingsSimilarityOptimizations/blob/master/Images/BenchmarkProcess.gif)

**1) Benchmark - VectorLinear**  
Goal of this benchmark is to showcase that a simple vector math approach will scale linearly. For example, 1000 vectors will take ~10x longer to process similarity math then 100 vectors. This performance degredation is acceptable for smaller vector views/indexes, but unworkable for larger sets.
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
```
**2) Benchmark - VectorCalculation**  
Goal of this benchmark is to show that if vectors are normalized, CosineSimilarity and DotProduct calculations will return the same answer with DotProduct being faster to calculate. The DotProduct formula simply requires less math, hencse is faster.
```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1195 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
| DotProductVectors1536Dimensions       | 1000                    | 0.0947 ms | 0.0000 ms | 0.0000 ms |     -21% |    0.1% |
```
**3) Benchmark - VectorDimensions**  
Goal of this benchmark is to show that the size of the dimensions of vectors matters for performance. OpenAI's ADAv2 model uses a dimension size of 1536, but there are models that output much smaller dimension sizes (768 for example). Look at the MTEB leaderboard for other embeddings models with smaller dimension sizes, but with good similarity accuracy: https://huggingface.co/spaces/mteb/leaderboard  
```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1196 ms | 0.0004 ms | 0.0004 ms | baseline |         | 
| CosineSimilarityVectors768Dimensions  | 1000                    | 0.0648 ms | 0.0001 ms | 0.0001 ms |     -46% |    0.4% | 
```
**4) Benchmark - Multithread**  
Goal of this benchmark is to show that vector math calculations are independent, which means they are embarassingly parallelizable. Leveraging multithreading on large vector sets can improve the performance dramatically of vector search.  
```
| Method                                           | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|------------------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions            | 100000                  | 21.605 ms | 0.3985 ms | 0.3727 ms | baseline |         | 
| CosineSimilarityVectors1536DimensionsMultithread | 100000                  |  6.523 ms | 0.0808 ms | 0.0756 ms |     -70% |    1.6% | 
```
**5) Benchmark - VectorAVX**  
Goal of this benchmark is to show that AVX hardware extensions (SIMD math) with the supporting software runtime can dramatically improve the performance of vector math operations. Newer AMD & Intel CPUs include more AVX-512 hardware intrinsics. For vector bath, this basically allows to process more math over floating point numbers in less CPU instrunctions. The other important intersection is that the software runtime needs to be able to interface with the AVX hardware extensions. .NET 8 LTS has added a new TensorPrimitives library with hardware backoff and multi-targeting that offers the performance of AVX-128, AVX-256, AVX-512 (if available) with non-hardware failover.
```
| Method                                | NumberOfVectorsToCreate | AVXType     | Mean      | Error    | StdDev   | 
|-------------------------------------- |------------------------ |------------ |----------:|---------:|---------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | NonHardware | 167.29 ms | 0.231 ms | 0.216 ms | 
| CosineSimilarityVectors1536Dimensions | 100000                  | Vector512   |  20.97 ms | 0.143 ms | 0.127 ms | 
```  
**6) Benchmark - DotNetVersion**  
.NET continues to innovate with each yearly release. .NET 8 LTS includes numerous performance improvments, building upong the many .NET 6, 7 performance focused improvements. Therefore, a simple re-compile to the latest version can lead to performance improvements without any major changes. .NET 8 performance improvments: https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/  
```
| Method                                | Runtime  | NumberOfVectorsToCreate | Mean     | Error    | StdDev   | Ratio    | RatioSD | Alloc Ratio |
|-------------------------------------- |--------- |------------------------ |---------:|---------:|---------:|---------:|--------:|------------:|
| CosineSimilarityVectors1536Dimensions | .NET 6.0 | 100000                  | 22.83 ms | 0.313 ms | 0.293 ms | baseline |         |             |
| CosineSimilarityVectors1536Dimensions | .NET 8.0 | 100000                  | 21.63 ms | 0.325 ms | 0.304 ms |      -5% |    1.6% |         -0% |
```
**7) Benchmark - Complete**  
The goal of this benchmark is to curate all of these benchmarks into single performance job. These performance improvements are additive and in totality can lead to fantaastic performance improvements.  
```
| Method                                | NumberOfVectorsToCreate | Mean       | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |-----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | 169.045 ms | 0.9513 ms | 0.8898 ms | baseline |         | 
| Complete                              | 100000                  |   4.941 ms | 0.0838 ms | 0.0743 ms |     -97% |    1.6% | 
```
**8) Benchmark - CompleteRealDataANN**  
In the previous 7 benchmarks used generated vector data and used a simple list/array of floating point vectors. The goal of this benchmark is to use real OpenAI Ada-v2 embeddings at scale with 1 million vectors, while finding similarity from an actual "question" using embeddings. This benchmark uses most of the efficiencies from #7 (minus the smaller dimension size) in the baseline run. In addition to the real vector data at scale, a custom approximate nearest neighbor (ANN) graph data structure is used to dramatically improve performance.  
Azure AI Search - HNSW Implementation: https://learn.microsoft.com/en-us/azure/search/vector-search-ranking  
HNSW implementation in C#: https://github.com/bartczernicki/hnsw-sharp  

Approximate Nearest Neighbor (ANN) is fast at searching. For a 1M vector data set, it can scale to more than 1,000 searches/second! What is the tradeoff?  
1) Building an ANN graph is expensive and architectural considerations need to made for maintaining the graph/updating the records in real-time. These patterns have existed in database systems for quite some time. For example, SQL Server ColumnStore Indexes have delta rowgroups as buffers until the Columnstore index is rebuilt completely: https://learn.microsoft.com/en-us/sql/relational-databases/indexes/columnstore-indexes-overview?view=sql-server-ver16#delta-rowgroup  
2) The first word in ANN is **approximate**. This is meant to be explicit as you are sacrificing perfect recall (missing some records being retrieved) for the dramatic speed increases  
```
| Method              | Mean        | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------- |------------:|----------:|----------:|---------:|--------:|-
| Complete            | 107.2623 ms | 2.1008 ms | 4.0476 ms | baseline |         | 
| CompleteRealDataANN |   0.6250 ms | 0.0015 ms | 0.0013 ms |   -99.4% |    4.8% | 
```
