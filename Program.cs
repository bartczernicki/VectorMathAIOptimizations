using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.SemanticKernel.AI.Embeddings.VectorOperations;
using System.Collections.Concurrent;

namespace VectorEmbeddingsSimilarityOptimizations
{
    [MemoryDiagnoser(false)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Monitoring, runtimeMoniker: RuntimeMoniker.Net60, baseline: true, warmupCount: 1)]
    [SimpleJob(runStrategy: RunStrategy.Monitoring, runtimeMoniker: RuntimeMoniker.Net80, baseline: false, warmupCount: 1)]
    [Config(typeof(BenchmarkConfig))]
    public class Program
    {
        // Fields
        private float[] vectorToCompareTo768Dimensions;
        private float[][] testVectors768Dimensions;
        private float[] vectorToCompareTo1536Dimensions;
        private float[][] testVectors1536Dimensions;
        // Processor Count (at 75%)
        private static int ProcessorsAvailableAt75Percent = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Vector Performance Benchmark");
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }

        [GlobalSetup]
        public void Setup()
        {
            // Generate float vectors that match dimension size of OpenAI Embeddings
            vectorToCompareTo768Dimensions = GenerateFloatVector(768);
            testVectors768Dimensions = GenerateFloatVectors(NumberOfVectorsToCreate, 768);
            vectorToCompareTo1536Dimensions = GenerateFloatVector(1536);
            testVectors1536Dimensions = GenerateFloatVectors(NumberOfVectorsToCreate, 1536);

            ProcessorsAvailableAt75Percent = (int)(0.75 * Environment.ProcessorCount);
        }

        [Params(1000, 1000000)] //<-- Changes this to determine the amount of vectors to "mimic" a Vector database
        public int NumberOfVectorsToCreate { get; set; }

        [Params(false, true)] //<-- Changes this to determine the amount of vectors to "mimic" a Vector database
        public bool MultiThreaded { get; set; }

        private static IEnumerable<VectorScore> TopMatchingVectors(ReadOnlySpan<float> vectorToCompareTo, ReadOnlySpan<float[]> vectors,
            bool useCosineSimilarity, bool multiThreaded)
        {
            var results = new List<VectorScore>(vectors.Length);
            var numOfVectors = vectors.Length;

            if (multiThreaded)
            {
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = ProcessorsAvailableAt75Percent };
                var vectorsArray = vectors.ToArray();
                var vectorToCompareToArray = vectorToCompareTo.ToArray();
                var resultsConcurrentBag = new ConcurrentBag<VectorScore>();

                Parallel.For(0, numOfVectors, parallelOptions,
                 i =>
                 {
                     var singleVector = vectorsArray[i];

                     var similarityScore = useCosineSimilarity ? CosineSimilarityOperation.CosineSimilarity(vectorToCompareToArray, singleVector) :
                         DotProductOperation.DotProduct(vectorToCompareToArray, singleVector);
                     // convert to float
                     float[] convertedArray = singleVector;

                     resultsConcurrentBag.Add(new VectorScore { FloatVector = convertedArray, SimilarityScore = similarityScore });
                 });

                return resultsConcurrentBag.OrderByDescending(a => a.SimilarityScore).Take(50);
            }
            else
            {
                for (var i = 0; i != numOfVectors; i++)
                {
                    ReadOnlySpan<float> singleVector = vectors.Slice(i, 1)[0].AsSpan();
                    var similarityScore = useCosineSimilarity ? CosineSimilarityOperation.CosineSimilarity(vectorToCompareTo, singleVector) :
                        DotProductOperation.DotProduct(vectorToCompareTo, singleVector);
                    // convert to float
                    float[] convertedArray = singleVector.ToArray();

                    results.Add(new VectorScore { FloatVector = convertedArray, SimilarityScore = similarityScore });
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
