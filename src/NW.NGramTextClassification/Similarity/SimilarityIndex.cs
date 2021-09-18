using System;

namespace NW.NGramTextClassification.Similarity
{
    /// <summary>The result of a similarity calculation.</summary>
    public class SimilarityIndex
    {

        #region Fields
        #endregion

        #region Properties

        public ulong Id { get; }
        public string Label { get; }
        public double Value { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SimilarityIndex"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SimilarityIndex(ulong id, string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

            Id = id;
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
                    $"{nameof(Id)}: '{Id}'",
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