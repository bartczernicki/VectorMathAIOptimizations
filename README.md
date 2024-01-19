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
