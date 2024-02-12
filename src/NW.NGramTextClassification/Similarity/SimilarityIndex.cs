using System;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.Similarity
{
    /// <summary>The result of a similarity calculation.</summary>
    public class SimilarityIndex
    {

        #region Fields
        #endregion

        #region Properties

        public string Text { get; }
        public string Label { get; }
        public double Value { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SimilarityIndex"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SimilarityIndex(string text, string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

            Text = text;
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
                    $"{nameof(Text)}: '{Text}'",
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
    Last Update: 18.09.2022
*/