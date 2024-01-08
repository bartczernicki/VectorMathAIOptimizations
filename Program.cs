using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Numerics.Tensors;
using System.Runtime.Intrinsics;
using Microsoft.SemanticKernel;


namespace VectorEmbeddingsSimilarityOptimizations
{
    [MemoryDiagnoser(true)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80)]
    [Config(typeof(VectorEmbeddingsSimilarityOptimizations.Util.BenchmarkConfig))]
    public class Program
    {
        private Util.Vectors? vectors;

        // Processor Count (set at 75% in code)
        private static int ProcessorsAvailableAt75Percent = Util.BenchmarkConfig.ProcessorsAvailableAt75Percent;

        static void Main(string[] args)
        {
            Console.WriteLine("Vector Performance Benchmark");

        #if NET8_0_OR_GREATER
            Console.WriteLine("AVX-128: " + Vector128.IsHardwareAccelerated.ToString());
            Console.WriteLine("AVX-256: " + Vector256.IsHardwareAccelerated.ToString());
            Console.WriteLine("AVX-512: " + Vector512.IsHardwareAccelerated.ToString());
        #endif

            //var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            var summary = BenchmarkRunner.Run<Program>();
            // print results
            Console.WriteLine(summary.ToString());
        }

        [GlobalSetup]
        public void Setup()
        {
            // Generate float vectors that match dimension size of Open-Source and OpenAI Embeddings
            // MTEB Database: https://huggingface.co/spaces/mteb/leaderboard 
            // 768 Dimensions is a popular embeddings dimension size for several leaderboard open-source models
            // 1536 Dimensions is the size of the ada-002 model for OpenAI/Azure OpenAI embeddings
            this.vectors = new VectorEmbeddingsSimilarityOptimizations.Util.Vectors(NumberOfVectorsToCreate);

            ProcessorsAvailableAt75Percent = (int)(0.75 * Environment.ProcessorCount);
        }

        [Params(1000, 100)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, large)
        // 1mil embeddings is roughly 700,000-1mil document pages with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        [Params(false, true)] //<-- Change this to determine if to run on a single thread or leverage 75% of available threads 
        public bool MultiThreaded { get; set; }

        [Benchmark]
        public void CosineSimilarityVectors768Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, true, MultiThreaded);
        }

        [Benchmark]
        public void DotProductVectors768Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, false, MultiThreaded);
        }

        [Benchmark]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, MultiThreaded);
        }

        [Benchmark]
        public void DotProductVectors1536Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, false, MultiThreaded);
        }
    }
}
