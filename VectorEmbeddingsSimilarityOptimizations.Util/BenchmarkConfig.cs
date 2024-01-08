﻿using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Reports;

namespace VectorEmbeddingsSimilarityOptimizations.Util
{
    public class BenchmarkConfig : ManualConfig
    {
        // Processor Count (set at 75% in code)
        public static int ProcessorsAvailableAt75Percent = 0;

        public BenchmarkConfig()
        {
            SummaryStyle = SummaryStyle.Default
                .WithRatioStyle(RatioStyle.Percentage)
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Millisecond);
        }
    }
}
