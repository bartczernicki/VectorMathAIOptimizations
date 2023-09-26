using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.AI.Embeddings.VectorOperations;
using System.Runtime.InteropServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace VectorEmbeddingsSimilarityOptimizations
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Program
    {
        private float[] vectorToCompareTo;
        private float[][] testVectors;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }

        [GlobalSetup]
        public void Setup()
        {
            // Generate float vectors that match dimension size of OpenAI Embeddings
            vectorToCompareTo = GenerateFloatVector(1536);
            testVectors = GenerateFloatVectors(1000000, 1536);
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
        public void CosineSimilarityVectors()
        {
            var results = TopMatchingVectors(vectorToCompareTo, testVectors, true);
        }

        [Benchmark]
        public void DotProductVectors()
        {
            var results = TopMatchingVectors(vectorToCompareTo, testVectors, false);
        }
    }


}
