using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;
using System.Numerics.Tensors;

namespace VectorEmbeddingsSimilarityOptimizations
{
    [MemoryDiagnoser(false)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    //[SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net70, baseline: false)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80, baseline: true)]
    [Config(typeof(BenchmarkConfig))]
    public class Program
    {
        // Fields
        private float[]? vectorToCompareTo768Dimensions;
        private float[][]? testVectors768Dimensions;
        private float[]? vectorToCompareTo1536Dimensions;
        private float[][]? testVectors1536Dimensions;

        // Processor Count (set at 75% in code)
        private static int ProcessorsAvailableAt75Percent = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Vector Performance Benchmark");
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }

        [GlobalSetup]
        public void Setup()
        {
            // Generate float vectors that match dimension size of Open-Source and OpenAI Embeddings
            // MTEB Database: https://huggingface.co/spaces/mteb/leaderboard 
            // 768 Dimensions is a popular embeddings dimension size for several leaderboard open-source models
            // 1536 Dimensions is the size of the ada-002 model for OpenAI/Azure OpenAI embeddings
            vectorToCompareTo768Dimensions = GenerateFloatVector(768);
            testVectors768Dimensions = GenerateFloatVectors(NumberOfVectorsToCreate, 768);
            vectorToCompareTo1536Dimensions = GenerateFloatVector(1536);
            testVectors1536Dimensions = GenerateFloatVectors(NumberOfVectorsToCreate, 1536);

            ProcessorsAvailableAt75Percent = (int)(0.75 * Environment.ProcessorCount);
        }

        [Params(1000, 100000)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, large)
        // 1mil embeddings is roughly 700,000-1mil document pages with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        [Params(false, true)] //<-- Change this to determine if to run on a single thread or leverage 75% of available threads 
        public bool MultiThreaded { get; set; }

        private static IEnumerable<VectorScore> TopMatchingVectors(ReadOnlySpan<float> vectorToCompareTo, ReadOnlySpan<float[]> vectors,
            bool useCosineSimilarity, bool multiThreaded)
        {
            var results = new List<VectorScore>(vectors.Length);
            var numOfVectors = vectors.Length;

            if (multiThreaded)
            {
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = ProcessorsAvailableAt75Percent };
                var resultsConcurrentBag = new ConcurrentBag<VectorScore>(); // <- use concurrent collection for parallel loop
                // You can use an array copy in this simple scenario to make this faster, but most scenarios will share data

                // Need to cast these as ReadOnlyMemory to avoid ref struct error or do any unsafe pointers
                // This probably can be optimized
                var vectorToCompareToMemory = new ReadOnlyMemory<float>(vectorToCompareTo.ToArray());
                var vectorsMemory = new ReadOnlyMemory<float[]>(vectors.ToArray());

                Parallel.For(0, numOfVectors, parallelOptions,
                 i =>
                 {
                     var singleVector = vectorsMemory.Slice(i, 1).Span[0].AsSpan();
                     var vectorToCompareToArray = vectorToCompareToMemory.Span;

                     
                     var similarityScore = useCosineSimilarity ? TensorPrimitives.CosineSimilarity(vectorToCompareToArray, singleVector) :
                         TensorPrimitives.Dot(vectorToCompareToArray, singleVector);

                     resultsConcurrentBag.Add(new VectorScore { VectorIndex = i, SimilarityScore = similarityScore });
                 });

                return resultsConcurrentBag.OrderByDescending(a => a.SimilarityScore).Take(50);
            }
            else
            {
                for (var i = 0; i != numOfVectors; i++)
                {
                    ReadOnlySpan<float> singleVector = vectors.Slice(i, 1)[0];

                    var similarityScore = useCosineSimilarity ? TensorPrimitives.CosineSimilarity(vectorToCompareTo, singleVector) :
                        TensorPrimitives.Dot(vectorToCompareTo, singleVector);

                    results.Add(new VectorScore { VectorIndex = i, SimilarityScore = similarityScore });
                }

                return results.OrderByDescending(a => a.SimilarityScore).Take(50);
            }
        }

        private static float[] GenerateFloatVector(int numDimension)
        {
            double MIN_VALUE = -1.0;
            double MAX_VALUE = 1.0;

            float[] result = new float[numDimension];
            Random random = new Random();

            for (int i = 0; i < numDimension; i++)
            {
                result[i] = (float) (random.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE);
            }

            return result;
        }

        private static float[][] GenerateFloatVectors(int numVectors, int numDimensions)
        {
            var result = new float[numVectors][];
            Random random = new Random();

            for (int i = 0; i < numVectors; i++)
            {
                result[i] = GenerateFloatVector(numDimensions);
            }

            return result;
        }

        [Benchmark]
        public void CosineSimilarityVectors768Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo768Dimensions, testVectors768Dimensions, true, MultiThreaded);
        }

        [Benchmark]
        public void DotProductVectors768Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo768Dimensions, testVectors768Dimensions, false, MultiThreaded);
        }

        [Benchmark]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo1536Dimensions, testVectors1536Dimensions, true, MultiThreaded);
        }

        [Benchmark]
        public void DotProductVectors1536Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo1536Dimensions, testVectors1536Dimensions, false, MultiThreaded);
        }
    }


}
