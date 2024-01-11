using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Threading;


namespace VectorEmbeddingsSimilarityOptimizations.Jobs.VectorAVX
{
    [MemoryDiagnoser(true)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80)]
    [Config(typeof(Util.BenchmarkConfig))]
    [HideColumns(Column.Gen0, Column.Gen1, Column.Allocated)] // Hide unnecessary columns
    public class Benchmark
    {
        private Util.Vectors? vectors;

        // Dynamically retrieve the supported AVX types on this hardware
        public IEnumerable<string> SupportedAVXTypes => Util.Vectors.GetSupportedAVXTypes();

        [GlobalSetup]
        public void Setup()
        {
            this.vectors = new Util.Vectors(NumberOfVectorsToCreate);
        }

        [Params(100000)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, large)
        // 1mil embeddings is roughly 700,000-1mil document paragraphs/phrases with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        // property with public setter
        [ParamsSource(nameof(SupportedAVXTypes))]
        public string AVXType { get; set; } = string.Empty;

        [Benchmark]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = Util.Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, false, AVXType);
        }
    }
}
