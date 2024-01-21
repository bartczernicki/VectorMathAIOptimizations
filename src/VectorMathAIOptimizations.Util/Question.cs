using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorMathAIOptimizations.Util
{
    internal class Question
    {
        public required string Id { get; set; }
        public required string Text { get; set; }
        public required List<float> Embeddings { get; set; }
    }
}
