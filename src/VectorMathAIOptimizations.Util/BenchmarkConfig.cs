﻿using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Attributes;

namespace VectorMathAIOptimizations.Util
{
    // Global configuration used across all benchmarks
    public class BenchmarkConfig : ManualConfig
    {
        // Processor Count (set at 75% in code)
        // Note: Only used if multi-threading is turned on
        public static int ProcessorsAvailableAt75Percent = (int) Math.Ceiling((0.75 * Environment.ProcessorCount));

        public BenchmarkConfig()
        {
            // Add Plain Exporter
            this.AddExporter(PlainExporter.Default);

            SummaryStyle = SummaryStyle.Default
                .WithRatioStyle(RatioStyle.Percentage)
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Millisecond);
        }
    }
}
