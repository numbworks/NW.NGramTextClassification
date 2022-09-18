using System;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>A labeled example.</summary>
    public class LabeledExample
    {

        #region Fields
        #endregion

        #region Properties

        public string Label { get; }
        public string Text { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public LabeledExample(string label, string text)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));

            Label = label;
            Text = text;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Label)}: '{Label}'",
                    $"{nameof(Text)}: '{Text}'"
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