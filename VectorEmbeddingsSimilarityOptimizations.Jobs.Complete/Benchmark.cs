using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;


namespace VectorEmbeddingsSimilarityOptimizations.Jobs.Complete
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
            this.vectors = new Util.Vectors(NumberOfVectorsToCreate);
        }

        [Params(100000)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, medium, large)
        // 1mil embeddings is roughly 700,000-1mil document paragraphs/phrases with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        [Benchmark(Baseline = true)]
        public void CosineSimilarityVectors1536Dimensions()
        {
            // Use defaults: 1536 Dimensions, use cosine similarity, use single thread, non-hardware acceleration
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, false, "NonHardware");
        }

        [Benchmark]
        public void Complete()
        {
            // Use 768 Dimensions, use dot product, use multiple threads, use AVX acceleration
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo768Dimensions, vectors?.TestVectors768Dimensions, false, true, string.Empty);
        }
    }
}
