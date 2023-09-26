using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Microsoft.SemanticKernel.AI.Embeddings.VectorOperations;

namespace VectorEmbeddingsSimilarityOptimizations
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(RuntimeMoniker.Net80, baseline: false)]
    public class Program
    {
        private float[] vectorToCompareTo768Dimensions;
        private float[][] testVectors768Dimensions;
        private float[] vectorToCompareTo1536Dimensions;
        private float[][] testVectors1536Dimensions;

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
            testVectors768Dimensions = GenerateFloatVectors(1000000, 768);
            vectorToCompareTo1536Dimensions = GenerateFloatVector(1536);
            testVectors1536Dimensions = GenerateFloatVectors(1000000, 1536);
        }

        private static IEnumerable<VectorScore> TopMatchingVectors(ReadOnlySpan<float> vectorToCompareTo, ReadOnlySpan<float[]> vectors, bool useCosineSimilarity)
        {
            var results = new List<VectorScore>();
            var numOfVectors = vectors.Length;

            for (var i = 0; i != numOfVectors; i++)
            {
                ReadOnlySpan<float> singleVector = vectors.Slice(i, 1)[0].AsSpan();
                var similarityScore = useCosineSimilarity ? CosineSimilarityOperation.CosineSimilarity(vectorToCompareTo, singleVector) :
                    DotProductOperation.DotProduct(vectorToCompareTo, singleVector);
                // convert to float
                float[] convertedArray = singleVector.ToArray();


                results.Add(new VectorScore { FloatVector = convertedArray, SimilarityScore = similarityScore });
            }

            return results.OrderByDescending(a => a.SimilarityScore);
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
            var results = TopMatchingVectors(vectorToCompareTo768Dimensions, testVectors768Dimensions, true);
        }

        [Benchmark]
        public void DotProductVectors768Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo768Dimensions, testVectors768Dimensions, false);
        }

        [Benchmark]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo1536Dimensions, testVectors1536Dimensions, true);
        }

        [Benchmark]
        public void DotProductVectors1536Dimensions()
        {
            var results = TopMatchingVectors(vectorToCompareTo1536Dimensions, testVectors1536Dimensions, false);
        }
    }


}
