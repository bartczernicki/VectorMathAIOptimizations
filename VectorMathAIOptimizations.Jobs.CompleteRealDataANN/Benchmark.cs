using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;


namespace VectorMathAIOptimizations.Jobs.CompleteRealDataANN
{
    [MemoryDiagnoser(true)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80)]
    [Config(typeof(Util.BenchmarkConfig))]
    [HideColumns(Column.Gen0, Column.Gen1, Column.Allocated, Column.AllocRatio)] // Hide unnecessary columns
    public class Benchmark
    {
        private Util.Vectors? vectors;

        [GlobalSetup]
        public void Setup()
        {
            // Load the vectors
            this.vectors = new Util.Vectors(1, true);
            // Load the HNSW graph
            //this.vectors?.LoadHNSWGraph();
        }

        [Benchmark(Baseline = true)]
        public void Complete()
        {
            // Real Data with Optimizations - use dot product, use multiple threads, use AVX acceleration
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.DbPediaVectors, false, true, string.Empty);
        }

        [Benchmark]
        public void CompleteRealDataANN()
        {
            // Real Data with Optimizations - use dot product, use multiple threads, use AVX acceleration
            var vectorToSearch = vectors?.DbPediaVectors?[0];
            //Console.WriteLine($"Vector to search: {vectorToSearch}");
            var results = vectors?.HNSWGraph?.KNNSearch(vectorToSearch, 50);
            //Console.WriteLine($"Results: {results?.Count}");
        }
    }
}
