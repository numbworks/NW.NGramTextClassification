using System.Collections.Generic;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification
{
    /// <summary>The result of a text classification.</summary>
    public class TextClassifierResult
    {

        #region Fields
        #endregion

        #region Properties

        public string Label { get; }
        public List<SimilarityIndex> SimilarityIndexes { get; }
        public List<SimilarityIndexAverage> SimilarityIndexAverages { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="TextClassifierResult"/> instance.
        /// <para>When the text classification didn't return any value, <paramref name="label"/> can also be null. Therefore null is an accepted value.</para>
        /// </summary>
        public TextClassifierResult(string label, List<SimilarityIndex> indexes, List<SimilarityIndexAverage> indexAverages)
        {

            Validator.ValidateList(indexes, nameof(indexes));
            Validator.ValidateList(indexAverages, nameof(indexAverages));

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
                    $"{nameof(Label)}: '{Label ?? "null"}'",
                    $"{nameof(SimilarityIndexes)}: '{SimilarityIndexes.Count}'", // can't be null due of ValidateList()
                    $"{nameof(SimilarityIndexAverages)}: '{SimilarityIndexAverages.Count}'" // can't be null due of ValidateList()
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/