using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Reports;

namespace VectorEmbeddingsSimilarityOptimizations
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            SummaryStyle = SummaryStyle.Default
                .WithRatioStyle(RatioStyle.Percentage)
                .WithTimeUnit(Perfolizer.Horology.TimeUnit.Second);
        }
    }
}
