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

**Benchmark - VectorLinear**  
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
**Benchmark - VectorCalculation**  
Goal of this benchmark is to show that if vectors are normalized, CosineSimilarity and DotProduct calculations will return the same answer with DotProduct being faster to calculate. The DotProduct formula simply requires less math, hencse is faster.
```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1195 ms | 0.0001 ms | 0.0001 ms | baseline |         | 
| DotProductVectors1536Dimensions       | 1000                    | 0.0947 ms | 0.0000 ms | 0.0000 ms |     -21% |    0.1% |
```
**Benchmark - VectorDimensions**  
Goal of this benchmark is to show that the size of the dimensions of vectors matters for performance. OpenAI's ADAv2 model uses a dimension size of 1536, but there are models that output much smaller dimension sizes (768 for example). Look at the MTEB leaderboard for other embeddings models with smaller dimension sizes, but with good similarity accuracy: https://huggingface.co/spaces/mteb/leaderboard  
```
| Method                                | NumberOfVectorsToCreate | Mean      | Error     | StdDev    | Ratio    | RatioSD | 
|-------------------------------------- |------------------------ |----------:|----------:|----------:|---------:|--------:|-
| CosineSimilarityVectors1536Dimensions | 1000                    | 0.1196 ms | 0.0004 ms | 0.0004 ms | baseline |         | 
| CosineSimilarityVectors768Dimensions  | 1000                    | 0.0648 ms | 0.0001 ms | 0.0001 ms |     -46% |    0.4% | 
```
