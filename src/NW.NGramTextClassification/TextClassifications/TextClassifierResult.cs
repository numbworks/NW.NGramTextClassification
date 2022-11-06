using System.Collections.Generic;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification.TextClassifications
{
    /// <summary>The result of a text classification.</summary>
    public class TextClassifierResult
    {

        #region Fields
        #endregion

        #region Properties

        public TextSnippet TextSnippet { get; }
        public string Label { get; }
        public List<SimilarityIndex> SimilarityIndexes { get; }
        public List<SimilarityIndexAverage> SimilarityIndexAverages { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="TextClassifierResult"/> instance.
        /// <para>When the text classification doesn't return any value, parameters can be null.</para>
        /// </summary>
        public TextClassifierResult(TextSnippet textSnippet, string label, List<SimilarityIndex> indexes, List<SimilarityIndexAverage> indexAverages)
        {

            TextSnippet = textSnippet;
            Label = label;
            SimilarityIndexes = indexes;
            SimilarityIndexAverages = indexAverages;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(TextSnippet)}: '{TextSnippet?.ToString() ?? "null"}'",
                    $"{nameof(Label)}: '{Label ?? "null"}'",
                    $"{nameof(SimilarityIndexes)}: '{SimilarityIndexes?.Count.ToString() ?? "null"}'",
                    $"{nameof(SimilarityIndexAverages)}: '{SimilarityIndexAverages?.Count.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 04.11.2022
*/