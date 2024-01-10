**Benchmark Vector Optimizations**  
is a .NET console application that benchmarks various vector techniques used in AI applications and their impact on performance. Implementing all of the perscribed options can improve performance of baseline applications up to 98%!!    
Requirements and Running the Application:
- Required: .NET 8 SDK (.NET 6 SDK is only required to build & run .NET 6 vs 8 runtime performance benchmark) with Visual Studio 2022 (tested on 17.9 Preview 2.1)  
- Not Required: No OpenAI, Azure OpenAI nor other "AI or cloud" keys required. Benchmark was done to mimic performance using simple internal generated vectors  
- Running the Application: Build the application in Visual Studio, Select run in "Release" mode (required for benchmarking), enter the number and click enter to run  
- Tweaking or Configuring: Each benchmark is a seperate DLL (for seperation concerns), which can be configured individually (i.e. amount of vectors)  
![Benchmark Process](https://github.com/bartczernicki/VectorEmbeddingsSimilarityOptimizations/blob/master/Images/BenchmarkProcess.gif)
