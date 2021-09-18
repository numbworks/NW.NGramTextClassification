using System;

namespace NW.NGramTextClassification.Similarity
{
    /// <summary>The average of multiple <see cref="SimilarityIndex"/>.</summary>
    public class SimilarityIndexAverage
    {

        #region Fields
        #endregion

        #region Properties

        public string Label { get; }
        public double Value { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SimilarityIndexAverage"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SimilarityIndexAverage(string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

            Label = label;
            Value = value;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Label)}: '{Label}'",
                    $"{nameof(Value)}: '{Value}'"
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