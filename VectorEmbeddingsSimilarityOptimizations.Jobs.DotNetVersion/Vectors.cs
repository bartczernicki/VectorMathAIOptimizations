using System.Collections.Concurrent;
using System.Numerics.Tensors;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace VectorEmbeddingsSimilarityOptimizations.Util.DotNetVersion
{
    public class Vectors
    {
        // Fields
        public float[]? VectorToCompareTo768Dimensions { get; set; }
        public float[][]? TestVectors768Dimensions { get; set; }
        public float[]? VectorToCompareTo1536Dimensions { get; set; }
        public float[][]? TestVectors1536Dimensions { get; set; }

        public Vectors(int numberOfVectorsToCreate)
        {
            // Generate float vectors that match dimension size of Open-Source and OpenAI Embeddings
            // MTEB Database: https://huggingface.co/spaces/mteb/leaderboard 
            // 768 Dimensions is a popular embeddings dimension size for several leaderboard open-source models
            // 1536 Dimensions is the size of the ada-002 model for OpenAI/Azure OpenAI embeddings
            VectorToCompareTo768Dimensions = GenerateFloatVector(768);
            TestVectors768Dimensions = GenerateFloatVectors(numberOfVectorsToCreate, 768);
            VectorToCompareTo1536Dimensions = GenerateFloatVector(1536);
            TestVectors1536Dimensions = GenerateFloatVectors(numberOfVectorsToCreate, 1536);
        }

        private float[] GenerateFloatVector(int numDimension)
        {
            double MIN_VALUE = -1.0;
            double MAX_VALUE = 1.0;

            float[] result = new float[numDimension];
            Random random = new Random();

            for (int i = 0; i < numDimension; i++)
            {
                result[i] = (float)(random.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE);
            }

            return result;
        }

        private float[][] GenerateFloatVectors(int numVectors, int numDimensions)
        {
            var result = new float[numVectors][];
            Random random = new Random();

            for (int i = 0; i < numVectors; i++)
            {
                result[i] = GenerateFloatVector(numDimensions);
            }

            return result;
        }

        public static IEnumerable<VectorScore> TopMatchingVectors(ReadOnlySpan<float> vectorToCompareTo, ReadOnlySpan<float[]> vectors,
            bool useCosineSimilarity, bool multiThreaded, string avxType)
        {
            var results = new List<VectorScore>(vectors.Length);
            var numOfVectors = vectors.Length;
            var useDotNetAvx = (avxType == "NonHardware") ? false : true;

            if (multiThreaded)
            {
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = BenchmarkConfig.ProcessorsAvailableAt75Percent };
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
    }
}
