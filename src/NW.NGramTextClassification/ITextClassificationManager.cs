using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifications;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassificationManager
    {

        /// <summary>Performs a complete text classification pipeline.</summary>
        /// <exception cref="Exception"/>
        ClassifyOutput Classify(ClassifyInput classifyInput);

        /// <summary>Performs a complete text classification pipeline and returns a null if an Exception is thrown.</summary>
        ClassifyOutput ClassifyOrNull(ClassifyInput classifyInput);

        /// <summary>Saves example JSON files into <see cref="SettingBag.FolderPath"/>.</summary>
        void Scaffold();

    }

}