using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>A labeled example that has been tokenized.</summary>
    public class TokenizedExample
    {

        #region Fields
        #endregion

        #region Properties

        public LabeledExample LabeledExample { get; }
        public List<INGram> NGrams { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="TokenizedExample"/> instance.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>  
        public TokenizedExample(LabeledExample labeledExample, List<INGram> nGrams)
        {

            Validator.ValidateObject(labeledExample, nameof(labeledExample));
            Validator.ValidateList(nGrams, nameof(nGrams));

            LabeledExample = labeledExample;
            NGrams = nGrams;

        }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(LabeledExample.Label)}: '{LabeledExample.Label}'",
                    $"{nameof(LabeledExample.Text)}: '{LabeledExample.Text}'",
                    $"{nameof(NGrams)}: '{NGrams.Count}'"  // can't be null due of ValidateList()
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