using BenchmarkDotNet.Columns;
using HNSW.Net;
using ParquetSharp;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics.Tensors;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace VectorMathAIOptimizations.Util
{
    public class Vectors
    {
        private const string PARQUET_FILES_DIRECTORY = @"e:\data\dbpedia-entities-openai-1M\";
        private const string PARQUET_FILE_PATH_SUFFIX = @"*.parquet";
        private const string GRAPH_FILE_NAME = "graph_M10.hnsw";

        // Fields
        public float[]? VectorToCompareTo768Dimensions { get; set; }
        public float[][]? TestVectors768Dimensions { get; set; }
        public float[]? VectorToCompareTo1536Dimensions { get; set; }
        public float[][]? TestVectors1536Dimensions { get; set; }
        public float[][]? DbPediaVectors { get; set; }
        public IReadOnlyList<float[]>? DbPediaQuestions { get; set; }

        public SmallWorld<float[], float>? HNSWGraph { get; set; }

        public Vectors(int numberOfVectorsToCreate, bool loadRealData = false)
        {
            // Generate float vectors that match dimension size of Open-Source and OpenAI Embeddings
            // MTEB Database: https://huggingface.co/spaces/mteb/leaderboard 
            // 768 Dimensions is a popular embeddings dimension size for several leaderboard open-source models
            // 1536 Dimensions is the size of the ada-002 model for OpenAI/Azure OpenAI embeddings
            VectorToCompareTo768Dimensions = GenerateFloatVector(768);
            TestVectors768Dimensions = GenerateFloatVectors(numberOfVectorsToCreate, 768);
            VectorToCompareTo1536Dimensions = GenerateFloatVector(1536);
            TestVectors1536Dimensions = GenerateFloatVectors(numberOfVectorsToCreate, 1536);

            if (loadRealData)
            {
                this.LoadRealVectorData();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// Returns a random float normalized vector of the specified dimension size
        /// </summary>
        private float[] GenerateFloatVector(int numDimension)
        {
            // Create a random vector of floats, then normalize it
            var vector = new float[numDimension];
            DefaultRandomGenerator.Instance.NextFloats(vector);
            VectorUtils.Normalize(vector);
            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        /// <summary>
        /// Returns a an array of random float normalized vectors of the specified dimension size
        /// </summary>
        private float[][] GenerateFloatVectors(int numVectors, int numDimensions)
        {
            var result = new float[numVectors][];

            for (int i = 0; i < numVectors; i++)
            {
                result[i] = GenerateFloatVector(numDimensions);
            }

            return result;
        }

        public void LoadRealVectorData()
        {
            ConcurrentBag<DbPedia> dataSetDbPedias = new ConcurrentBag<DbPedia>();
            var recordCount = 0;

            // https://huggingface.co/datasets/KShivendu/dbpedia-entities-openai-1M 
            var parquet_files = Directory.GetFiles(PARQUET_FILES_DIRECTORY, PARQUET_FILE_PATH_SUFFIX);

            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = BenchmarkConfig.ProcessorsAvailableAt75Percent
            };

            // Load Parquet Files in parallel
            Parallel.ForEach(parquet_files, parallelOptions, parquet_file =>
            {
                using (var parquetReader = new ParquetFileReader(parquet_file))
                {
                    Console.WriteLine($"File: {parquet_file}");

                    // Read Metadata
                    // This should be the same if you are reading the same type of parquet files
                    int numColumns = parquetReader.FileMetaData.NumColumns;
                    long numRows = parquetReader.FileMetaData.NumRows;
                    int numRowGroups = parquetReader.FileMetaData.NumRowGroups;
                    IReadOnlyDictionary<string, string> metadata = parquetReader.FileMetaData.KeyValueMetadata;

                    SchemaDescriptor schema = parquetReader.FileMetaData.Schema;
                    for (int columnIndex = 0; columnIndex < schema.NumColumns; ++columnIndex)
                    {
                        ColumnDescriptor column = schema.Column(columnIndex);
                        string columnName = column.Name;
                        var dataType = column.LogicalType;
                        string dataTypeString = dataType.ToString();
                    }

                    for (int rowGroup = 0; rowGroup < parquetReader.FileMetaData.NumRowGroups; ++rowGroup)
                    {
                        using (var rowGroupReader = parquetReader.RowGroup(rowGroup))
                        {
                            var groupNumRows = (int)rowGroupReader.MetaData.NumRows;
                            recordCount += groupNumRows; //Used to match processed and records inserted into ConcurrentBag

                            var ids = rowGroupReader.Column(0).LogicalReader<string>().ReadAll(groupNumRows);
                            var titles = rowGroupReader.Column(1).LogicalReader<string>().ReadAll(groupNumRows);
                            var texts = rowGroupReader.Column(2).LogicalReader<string>().ReadAll(groupNumRows);
                            var embeddings = rowGroupReader.Column(3).LogicalReader<double?[]>().ReadAll(groupNumRows);

                            for (int i = 0; i < ids.Length; i++)
                            {
                                var item = new DbPedia
                                {
                                    Id = ids[i],
                                    Title = titles[i],
                                    Text = texts[i],
                                    Embeddings = embeddings[i].Select(x => (float) x).ToList()
                                };
                                dataSetDbPedias.Add(item);
                            }

                            Console.WriteLine($"File: {parquet_file} - Processed: {groupNumRows} rows.");
                        }
                    }

                    parquetReader.Close();
                }
            });

            // Enforce order as this is important for the graph to be built correctly
            var dataSetDbPediasOrdered = dataSetDbPedias.OrderBy(a => a.Id).ToList();

            this.loadHNSWGraph(dataSetDbPediasOrdered.Select(x => x.Embeddings.ToArray()).ToList());

            this.DbPediaVectors = dataSetDbPediasOrdered.Select(x => x.Embeddings.ToArray()).ToArray();

            //var floatVectors = new float[dataSetDbPediasOrdered.Count()][];
            //for (int i = 0; i < dataSetDbPediasOrdered.Count(); i++)
            //{
            //    var floatArray = dataSetDbPediasOrdered[i].Embeddings.ToArray();
            //    floatVectors[i] = floatArray;
            //}

            Console.WriteLine($"Total Records: {recordCount} - Total Records Ordered: {this.DbPediaVectors.Length}");
        }

        private void loadHNSWGraph(List<float[]> vectors)
        {
            var stopWatch = Stopwatch.StartNew();
            stopWatch.Start();
            var filePath = Path.Combine(PARQUET_FILES_DIRECTORY, GRAPH_FILE_NAME);

            using (var f = File.OpenRead(filePath))
            {
                this.HNSWGraph = SmallWorld<float[], float>.DeserializeGraph(vectors, DotProductDistance.DotProductOptimized, DefaultRandomGenerator.Instance, f, true);
            }

            stopWatch.Stop();
            Console.WriteLine($"Time Taken to load ANN (HNSW) Graph: {stopWatch.ElapsedMilliseconds} ms.");
        }

        private void loadRealQuestionEmbeddings()
        {
            // load actual question
            var questionData = File.ReadAllText(@"question.json");
            var questions = System.Text.Json.JsonSerializer.Deserialize<List<Question>>(questionData);
            this.DbPediaQuestions = questions?.Select(a => a.Embeddings.ToArray()).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<VectorScore> TopMatchingVectors(ReadOnlySpan<float> vectorToCompareTo, ReadOnlySpan<float[]> vectors,
            bool useCosineSimilarity, bool multiThreaded, string avxType)
        {
            var results = new List<VectorScore>(vectors.Length);
            var numOfVectors = vectors.Length;

            // Console.WriteLine($"Vector Count: {numOfVectors}");

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

                     var similarityScore = useCosineSimilarity ? BenchmarkCosineSimilarity(vectorToCompareToArray, singleVector, useDotNetAvx, avxType) :
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

                    var similarityScore = useCosineSimilarity ? BenchmarkCosineSimilarity(vectorToCompareTo, singleVector, useDotNetAvx, avxType) :
                        TensorPrimitives.Dot(vectorToCompareTo, singleVector);

                    results.Add(new VectorScore { VectorIndex = i, SimilarityScore = similarityScore });
                }

                return results.OrderByDescending(a => a.SimilarityScore).Take(50);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetSupportedAVXTypes()
        {
            var supportedAVXTypes = new List<string> { "NonHardware" };

            if (Vector512.IsHardwareAccelerated)
            {
                supportedAVXTypes.Add("Vector512");
            }
            else if(Vector128.IsHardwareAccelerated)
            {
                supportedAVXTypes.Add("Vector128");
            }
            else if (Vector256.IsHardwareAccelerated)
            {
                supportedAVXTypes.Add("Vector256");
            }

            return supportedAVXTypes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float BenchmarkCosineSimilarity(ReadOnlySpan<float> x, ReadOnlySpan<float> y, bool useDotNetFramework, string avxType)
        {
            if (useDotNetFramework)
            {
                return TensorPrimitives.CosineSimilarity(x, y);
            }
            else
            {
                // If Non-Hardware then one of the AVX Vectors: 128, 256, 512 is being used
                if (avxType != "NonHardware")
                {
                    return TensorPrimitives.CosineSimilarity(x, y);
                }
                else // use non-hardware path non-accelerated
                {
                    if (x.IsEmpty)
                    {
                        throw new ArgumentException("X float vector is empty.");
                    }

                    if (x.Length != y.Length)
                    {
                        throw new AbandonedMutexException("X & Y vectors are not equal.");
                    }

                    float num7 = 0f;
                    float num8 = 0f;
                    float num9 = 0f;
                    for (int i = 0; i < x.Length; i++)
                    {
                        num7 = MathF.FusedMultiplyAdd(x[i], y[i], num7);
                        num8 = MathF.FusedMultiplyAdd(x[i], x[i], num8);
                        num9 = MathF.FusedMultiplyAdd(y[i], y[i], num9);
                    }

                    return num7 / (MathF.Sqrt(num8) * MathF.Sqrt(num9));
                }
            }
        }
    }
}
