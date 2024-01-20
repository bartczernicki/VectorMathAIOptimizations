**Benchmark Vector Math AI Optimizations**  
is a .NET console application that benchmarks various vector math techniques used in AI applications and their impact on performance. Implementing all of the perscribed options can improve performance of baseline applications up to 98%! Those improvements can be improved even further ~99% using Aproximate Nearest Neighbor data structures like HNSW.
Running the Application & Features:
- Required: .NET 8 SDK (.NET 6 SDK is only required to build & run .NET 6 vs 8 runtime performance benchmark) with Visual Studio 2022 (tested on 17.9 Preview 2.1)  
- Not Required: No OpenAI, Azure OpenAI nor other "AI or cloud" keys required. Benchmark was done to mimic performance using simple internal generated (mocked) vectors  
- Running the Application: Build the application in Visual Studio IDE, Select run in "Release" mode (required for benchmarking), select the benchmark number and click enter to run  
- Tweaking or Configuring: Each benchmark is a seperate DLL (for seperation concerns), which can be configured individually (i.e. amount of vectors) or optimized seperately with compiler directives. This allows for individual reports to be viewed and analyzed
- Real Data (1M vectors) - parquet files location: https://huggingface.co/datasets/KShivendu/dbpedia-entities-openai-1M   
- Uses HNSW algorithm from Microsoft (fork optimized): https://github.com/bartczernicki/hnsw-sharp
- Uses BenchmarkDotNet for test harness: https://github.com/dotnet/BenchmarkDotNet/blob/master/README.md  
![Benchmark Process](https://github.com/bartczernicki/VectorEmbeddingsSimilarityOptimizations/blob/master/Images/BenchmarkProcess.gif)

Note: Benchmarks listed below have been run on the following specs:
```
BenchmarkDotNet v0.13.12, Windows 10 (10.0.20348.2227) (Hyper-V)
AMD EPYC 9V33X, 2 CPU, 24 logical and 24 physical cores
.NET SDK 8.0.200-preview.23624.5
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  Job-NQPSMG : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Runtime=.NET 8.0  RunStrategy=Throughput  
```

**1) Benchmark - VectorLinear**  
Goal of this benchmark is to showcase that a simple vector math approach will scale linearly or monotonically (fancy word for linear with a "bend") depending on the CPU architecture. For example, 1,000 vectors will take ~10x longer to process similarity math then 100 vectors; and 10,000 vectors will take ~10x longer than 1,000 vectors. This performance degredation is acceptable for smaller vector views/indexes, but benchmarks below will show that linear scale is unworkable for larger vector sets.
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
OpenAI Documentation appears to barely highlight this significance: https://help.openai.com/en/articles/6824809-embeddings-frequently-asked-questions  
You can pre-caculate & cache the denominator of the vectors in the search index, basically the sum((vector^2)) can be cached for the entire vector set: https://image3.slideserve.com/6563482/cosine-similarity-l.jpg  
```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1195 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
| DotProductVectors1536Dimensions       | 1000                    | 0.0947 ms | 0.0000 ms | 0.0000 ms |     -21% |    0.1% |
```
**3) Benchmark - VectorDimensions**  
Goal of this benchmark is to show that the size of the dimensions of vectors matters for performance. OpenAI's ADAv2 model uses a dimension size of 1536, but there are models that output much smaller dimension sizes (768 for example), with better performance! Look at the MTEB leaderboard for other embeddings models with smaller dimension sizes, but with potentially very good similarity accuracy: https://huggingface.co/spaces/mteb/leaderboard  
Note: These additional models can be used to "ensemble" embeddings results for improved results  
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
1) AVX Extensions: https://en.wikipedia.org/wiki/Advanced_Vector_Extensions
2) .NET 8 Tensor Primitives: https://devblogs.microsoft.com/dotnet/announcing-dotnet-8-rc2/  
```
| Method                                | NumberOfVectorsToCreate | AVXType     | Mean      | Error    | StdDev   | 
|-------------------------------------- |------------------------ |------------ |----------:|---------:|---------:|-
| CosineSimilarityVectors1536Dimensions | 100000                  | NonHardware | 167.29 ms | 0.231 ms | 0.216 ms | 
| CosineSimilarityVectors1536Dimensions | 100000                  | Vector512   |  20.97 ms | 0.143 ms | 0.127 ms | 
```  
**6) Benchmark - DotNetVersion**  
.NET continues to innovate with each yearly release. .NET 8 LTS includes numerous performance improvments, building upong the many .NET 6, 7 performance focused improvements. Therefore, a simple re-compile to the latest version can lead to performance improvements without any major changes.  
.NET 8 performance improvments: https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/  
Note: It is important to note only vector mathc is being benchmarked, in reality many common .NET 8 improvements for (AOT for example, an Azure Function cold start ~650ms -> 50ms or JSON serialization performance) all will cascade holistically into the entire AI application.
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
```
| Method              | Mean          | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------- |--------------:|----------:|----------:|---------:|--------:|-
| Linear              | 1,664.7444 ms | 0.3980 ms | 0.3723 ms | baseline |         | 
| Complete            |    94.5493 ms | 1.8721 ms | 4.0299 ms |   -94.3% |    3.5% | 
| CompleteRealDataANN |     0.6453 ms | 0.0012 ms | 0.0011 ms |  -100.0% |    0.2% | 
```
Approximate Nearest Neighbor (ANN) is fast at searching. For a 1M vector data set, it can scale to more than 1,000 searches/second! What is the tradeoff?  
1) Building an ANN graph is expensive and architectural considerations need to made for maintaining the graph/updating the records in real-time. These patterns have existed in database systems for quite some time. For example, SQL Server ColumnStore Indexes have delta rowgroups as buffers until the Columnstore index is rebuilt completely: https://learn.microsoft.com/en-us/sql/relational-databases/indexes/columnstore-indexes-overview?view=sql-server-ver16#delta-rowgroup  
2) The first word in ANN is **approximate**. This is meant to be convey that ANN is sacrificing perfect recall (missing some records being retrieved) for the dramatic speed increases. In certain scenarios, ANN cannot be used. For example, if you are doing a financial calculation extracting quantitative data from documents; this will require perfect recall for accurate calculations.

The good news is that ANN graphs can also include the performance improvements mentioned above. Below is a performance graph of:
1) Building an ANN - HNSW graph single threaded with 1M records
2) Building an ANN - HNSW graph on .NET 8, AVX hardware intrinsics, using DotProduct distance
3) Building an ANN - HNSW graph with multi-threading & partitions

```
| Method              | Mean        | Ratio       | 
|-------------------- |------------:|------------:|-
| ANN - 1 (baseline)  |    6,647 s  |    baseline |
| ANN - 2             |    4,712 s  |     -29.11% |
| ANN - 3             |    1,252 s  |     -81.16% |
```

**Conclusions**  
Vector math is expensive, but you don't need a cluster of GPUs to scale to production real-world data. You can make algorithmic improvements with frameworks like .NET 8 to build systems, applications, APIs or services that can leverage the latest AI vector innovations.  

.NET 8 (C#) over the last couple version iterations has become a very compelling framework/language for building AI applications.  
- TIOBE Index - January 2024 - C# Wins programming language of the year: https://www.techrepublic.com/article/tiobe-index-january-2024/
- Techempower .NET (C#) is fast - Balancing performance & productivity: https://www.techempower.com/benchmarks/#section=test&runid=da8306a2-d1d8-4bd3-95d4-59d8cd7047af&hw=ph&test=fortune
- Since .NET 6.x it has been included in Linux package management directly
- Stability.ai has chosen to use .NET (C#) for their products over Python - https://github.com/Stability-AI/StableSwarmUI
- .NET 8 (loading 1 billion rows with analytics, currently faster than Java/Rust fastest) - https://hotforknowledge.com/2024/01/13/1brc-in-dotnet-among-fastest-on-linux-my-optimization-journey/  
