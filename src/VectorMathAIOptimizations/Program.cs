﻿using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System.Runtime.Intrinsics;


namespace VectorMathAIOptimizations
{
    public class Program
    {
        static void Main(string[] args)
        {
            // How BenchmarkDotNet Looks at Projects to find your benchmark DLLs
            // https://stackoverflow.com/questions/67766289/benchmarkdotnet-unable-to-find-tests-when-it-faces-weird-solution-structure  

            Console.Title = "Benchmark - Vector Math AI Optimizations";

            var aciiArt = """

                \  /_  __|_ _ ._ |\/| _._|_|_  
                 \/(/_(_ |_(_)|  |  |(_| |_| | 

                    ___   _                               
                 /\  |   / \.__|_o._ _ o_  _._|_o _ ._  _ 
                /--\_|_  \_/|_)|_|| | ||/_(_| |_|(_)| |_> 
                            |                                                                                                                                                                                                                                                     
                """;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(aciiArt);

            ProcessingOptions selectedProcessingChoice = (ProcessingOptions)0;
            bool validInput = false;

            // Iterate until the proper input is selected by the user
            while (!validInput)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(string.Empty);
                Console.WriteLine("Select one of the options, by typing a number (1 or 2 etc.)");
                Console.WriteLine("1) Linear Benchmark - Show how vector math scales linearly with the amount of vectors.");
                Console.WriteLine("2) Vector Calculation Benchmark - Compares Cosine similarity vs Dot Product performance.");
                Console.WriteLine("3) Vector Dimensions Benchmark - Compares performance of difference dimensions sizes.");
                Console.WriteLine("4) Multithreaded Benchmark - Compare multi-core performance vs single core.");
                Console.WriteLine("5) AVX Hardware Benchmark - Compares performance of AVX hardware acceleration vs no-acceleration.");
                Console.WriteLine("6) .NET Version Benchmark - Compares Different .NET versions: 6.0 vs 8.0.");
                Console.WriteLine("7) Complete Benchmark - Compares performance of all best practices combined.");
                Console.WriteLine("... #8 is WIP, has requirements not documented yet");
                Console.WriteLine("8) Complete Real Data & ANN Benchmark - Using Real Data (1M Vectors) & ANN Graph Optimizations.");

                var insertedText = Console.ReadLine() ?? string.Empty;
                string trimmedInput = insertedText.Trim();
                
                // check if the trimmedInput is between 1 and 8 (inclusive)
                if (Enumerable.Range(1, 8).Contains(Int32.Parse(trimmedInput)) == true)
                {
                    validInput = true;
                    selectedProcessingChoice = (ProcessingOptions)Int32.Parse(trimmedInput);
                    Console.WriteLine("You selected: {0}", selectedProcessingChoice);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect selection!!!!");
                }
            }

            BenchmarkDotNet.Reports.Summary? summary = null;

            if (selectedProcessingChoice == ProcessingOptions.VectorLinear)
            {
                // Benchmark - Vector Linear Scale
                summary = BenchmarkRunner.Run<Jobs.VectorLinear.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.VectorCalculation)
            {
                // Benchmark - Vector Calculation comparison of Cosine Similarity vs Dot Product
                summary = BenchmarkRunner.Run<Jobs.VectorCalculation.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.VectorDimensions)
            {
                // Benchmark - Vector Dimensions comparison of 768 vs 1536 Dimensions
                summary = BenchmarkRunner.Run<Jobs.VectorDimensions.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.VectorMultithread)
            {
                // Benchmark - Vector calculations using Multi-Threading
                Console.WriteLine(string.Format("Using {0} CPU cores for multithreaded benchmark.", Util.BenchmarkConfig.ProcessorsAvailableAt75Percent));
                summary = BenchmarkRunner.Run<Jobs.VectorMultithread.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.DotNetVersion)
            {
                // Benchmark - .NET Version comparison
                summary = BenchmarkRunner.Run<Jobs.DotNetVersion.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.VectorAVX)
            {
                // Benchmark - Vector with hardware AVX Optimizations (SIMD Math)
#if NET8_0_OR_GREATER
                Console.WriteLine("AVX-128 available: " + Vector128.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-256 available: " + Vector256.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-512 available: " + Vector512.IsHardwareAccelerated.ToString());
#endif
                summary = BenchmarkRunner.Run<Jobs.VectorAVX.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.Complete)
            {
                // Benchmark - Vector calculations using Multi-Threading
                Console.WriteLine(string.Format("Using {0} CPU cores for multithreaded benchmark.", Util.BenchmarkConfig.ProcessorsAvailableAt75Percent));
#if NET8_0_OR_GREATER
                Console.WriteLine("AVX-128 available: " + Vector128.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-256 available: " + Vector256.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-512 available: " + Vector512.IsHardwareAccelerated.ToString());
#endif

                summary = BenchmarkRunner.Run<Jobs.Complete.Benchmark>();
            }
            else if (selectedProcessingChoice == ProcessingOptions.CompleteRealDataANN)
            {
                // Benchmark - List vector count, CPU cores and AVX hardware acceleration
                Console.WriteLine("Vector Count: 1,000,000");
                Console.WriteLine(string.Format("Using {0} CPU cores for multithreaded benchmark.", Util.BenchmarkConfig.ProcessorsAvailableAt75Percent));
#if NET8_0_OR_GREATER
                Console.WriteLine("AVX-128 available: " + Vector128.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-256 available: " + Vector256.IsHardwareAccelerated.ToString());
                Console.WriteLine("AVX-512 available: " + Vector512.IsHardwareAccelerated.ToString());
#endif

                summary = BenchmarkRunner.Run<Jobs.CompleteRealDataANN.Benchmark>();
            }


            // Print Benchmark results
            Console.WriteLine(summary?.ToString());
        }
    }
}
