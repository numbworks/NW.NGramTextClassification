using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <inheritdoc cref="ILabeledExampleFactory"/>
    public class LabeledExampleFactory : ILabeledExampleFactory
    {

        #region Fields

        private INGramTokenizer _tokenizer;
        private uint _initialId;

        #endregion

        #region Properties

        public static uint DefaultInitialId { get; } = 1;
        public static INGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LabeledExampleFactory"/> instance.</summary>
        public LabeledExampleFactory(INGramTokenizer tokenizer, uint initialId)
        {

            Validator.ValidateObject(tokenizer, nameof(tokenizer));

            _tokenizer = tokenizer;
            _initialId = initialId;

        }

        /// <summary>Initializes a <see cref="LabeledExampleFactory"/> instance using default parameters.</summary>
        public LabeledExampleFactory()
            : this(new NGramTokenizer(), DefaultInitialId) { }

        #endregion

        #region Methods_public

        public LabeledExample TryCreateForRuleSet(ulong id, string label, string text, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<INGram> ngrams = _tokenizer.TryDoForRuleSet(text, tokenizerRuleSet);
            if (ngrams != null)
                return new LabeledExample(id, label, text, ngrams);

            return null;

        }
        public LabeledExample TryCreateForRuleSet(ulong id, string label, string text)
            => TryCreateForRuleSet(id, label, text, DefaultTokenizerRuleSet);

        public List<LabeledExample> TryCreateForRuleSet(List<(string label, string text)> tuples, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateList(tuples, nameof(tuples));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<LabeledExample> labeledExamples = new List<LabeledExample>();

            uint currentId = _initialId;
            foreach ((string label, string text) tuple in tuples)
            {

                LabeledExample labeledExample = TryCreateForRuleSet(currentId, tuple.label, tuple.text, tokenizerRuleSet);
                if (labeledExample != null)
                {

                    labeledExamples.Add(labeledExample);
                    currentId++;

                }

            }

            if (labeledExamples.Count == 0)
                return null;

            return labeledExamples;

        }
        public List<LabeledExample> TryCreateForRuleSet(List<(string label, string text)> tuples)
            => TryCreateForRuleSet(tuples, DefaultTokenizerRuleSet);

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/
