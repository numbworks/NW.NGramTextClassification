using System;
using NW.NGramTextClassification.TextClassifications;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassificationManager
    {

        /// <summary>Performs a complete text classification pipeline.</summary>
        /// <exception cref="Exception"/>
        public ClassifyOutput Classify(ClassifyInput classifyInput);

        /// <summary>Performs a complete text classification pipeline and returns a null if an Exception is thrown.</summary>
        public ClassifyOutput ClassifyOrNull(ClassifyInput classifyInput);

    }

}