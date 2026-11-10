using NW.NGramTextClassification.TextClassifications;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassificationManager
    {

        /// <summary>Performs a complete text classification pipeline.</summary>
        /// <exception cref="Exception"/>     
        public ClassifyOutput Classify(ClassifyInput classifyInput);

    }

}