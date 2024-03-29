﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;
using VectorMathAIOptimizations.Util.DotNetVersion;


namespace VectorMathAIOptimizations.Jobs.DotNetVersion
{
    [MemoryDiagnoser(true)]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.SlowestToFastest)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net60, baseline: true)]
    [SimpleJob(runStrategy: RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net80)]
    [Config(typeof(Util.DotNetVersion.BenchmarkConfig))]
    [HideColumns(Column.Gen0, Column.Gen1, Column.Allocated)] // Hide unnecessary columns
    public class Benchmark
    {
        private Vectors? vectors;

        [GlobalSetup]
        public void Setup()
        {
            this.vectors = new Vectors(NumberOfVectorsToCreate);
        }

        [Params(100000)] //<-- Change this to determine the amount of vectors to "mimic" a Vector database  (very small, medium, large)
        // 1mil embeddings is roughly 700,000-1mil document paragraphs/phrases with a decent amount of text present
        public int NumberOfVectorsToCreate { get; set; }

        [Benchmark]
        public void CosineSimilarityVectors1536Dimensions()
        {
            var results = Vectors.TopMatchingVectors(vectors?.VectorToCompareTo1536Dimensions, vectors?.TestVectors1536Dimensions, true, false, string.Empty);
        }
    }
}
