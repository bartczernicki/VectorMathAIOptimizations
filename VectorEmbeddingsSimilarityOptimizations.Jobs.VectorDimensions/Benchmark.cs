using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;


namespace VectorEmbeddingsSimilarityOptimizations.Jobs.VectorDimensions
{
    [MemoryDiagnoser(true)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80)]
    [Config(typeof(Util.BenchmarkConfig))]
    public class Benchmark
    {
        private Util.Vectors? vectors;

        [GlobalSetup]
        public void Setup()
        {
            this.vectors = new Util.Vectors(NumberOfVectorsToCreate);
        }

        [Params(1000)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, medium, large)
        // 1mil embeddings is roughly 700,000-1mil document paragraphs/phrases with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        [Benchmark(Baseline = true)]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, false, string.Empty);
        }

        [Benchmark]
        public void CosineSimilarityVectors768Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, true, false, string.Empty);
        }
    }
}
