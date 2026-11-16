using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifications;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassificationManager
    {

        /// <summary>
        /// Performs a complete text classification process.
        /// </summary>
        /// <exception cref="Exception"/>
        ClassifyOutput Classify(ClassifyInput classifyInput);

        /// <summary>
        /// Performs a complete text classification process and returns a null if an Exception is thrown.
        /// <para>If an exception is thrown, it logs the message and returns null.</para> 
        /// </summary>
        ClassifyOutput ClassifyOrNull(ClassifyInput classifyInput);

        /// <summary>
        /// Saves example JSON files into <see cref="SettingBag.FolderPath"/>.
        /// </summary>
        /// <exception cref="Exception"/>
        void Scaffold();

        /// <summary>
        /// Saves example JSON files into <see cref="SettingBag.FolderPath"/> and sets <paramref name="success"/> equal to true.
        /// <para>If an exception is thrown, it logs the message and sets <paramref name="success"/> equal to false.</para> 
        /// </summary> 
        void Scaffold(out bool success);

    }

}