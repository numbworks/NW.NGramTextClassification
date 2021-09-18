using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>A labeled example.</summary>
    public class LabeledExample
    {

        #region Fields
        #endregion

        #region Properties

        public ulong Id { get; }
        public string Label { get; }
        public string Text { get; }
        public List<INGram> TextAsNGrams { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>  
        public LabeledExample(ulong id, string label, string text, List<INGram> textAsNGrams)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateList(textAsNGrams, nameof(textAsNGrams));

            Id = id;
            Label = label;
            Text = text;
            TextAsNGrams = textAsNGrams;

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
                    $"{nameof(Text)}: '{Text}'",
                    $"{nameof(TextAsNGrams)}: '{TextAsNGrams.Count}'"  // can't be null due of ValidateList()
                    );

            return $"[ {content} ]";

        }

        #endregion


    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/