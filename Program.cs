using BenchmarkDotNet.Running;
using System.Runtime.Intrinsics;


namespace VectorEmbeddingsSimilarityOptimizations
{
    public class Program
    {
        static void Main(string[] args)
        {
            // How BenchmarkDotNet Looks at Projects to find your benchmark DLLs
            // https://stackoverflow.com/questions/67766289/benchmarkdotnet-unable-to-find-tests-when-it-faces-weird-solution-structure  

            Console.WriteLine("Vector Performance Benchmark");

            #if NET8_0_OR_GREATER
                Console.WriteLine("AVX-128: " + Vector128.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-256: " + Vector256.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-512: " + Vector512.IsHardwareAccelerated.ToString());
            #endif

            // Benchmark - Vector Linear Scale
            var summary = BenchmarkRunner.Run<Jobs.VectorLinear.Benchmark>();

            // Benchmark - Vector calculations using Multi-Threading
            //Console.WriteLine(string.Format("Using {0} CPU cores for multithreaded benchmark.", Util.BenchmarkConfig.ProcessorsAvailableAt75Percent));
            //var summary = BenchmarkRunner.Run<Jobs.VectorMultithread.Benchmark>();

            // Benchmark - Vector Calculation comparison of Cosine Similarity vs Dot Product
            // var summary = BenchmarkRunner.Run<Jobs.VectorCalculation.Benchmark>();

            // Benchmark - Vector Dimensions comparison of 768 vs 1536 Dimensions
            //var summary = BenchmarkRunner.Run<Jobs.VectorDimensions.Benchmark>();

            // Print Benchmark results
            Console.WriteLine(summary.ToString());
        }
    }
}
