using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <inheritdoc cref="ILabeledExampleManager"/>
    public class LabeledExampleManager : ILabeledExampleManager
    {

        #region Fields

        private NGramTokenization.INGramTokenizer _tokenizer;
        private uint _initialId;

        #endregion

        #region Properties

        public static INGramTokenizer DefaultNGramTokenizer { get; } = new NGramTokenizer();
        public static INGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LabeledExampleManager"/> instance.</summary>
        public LabeledExampleManager(INGramTokenizer tokenizer)
        {

            Validator.ValidateObject(tokenizer, nameof(tokenizer));

            _tokenizer = tokenizer;

        }

        /// <summary>Initializes a <see cref="LabeledExampleManager"/> instance using default parameters.</summary>
        public LabeledExampleManager()
            : this(DefaultNGramTokenizer) { }

        #endregion

        #region Methods_public

        public TokenizedExample CreateOrDefault(LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateObject(labeledExample, nameof(labeledExample));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<INGram> ngrams = _tokenizer.DoForRuleSetOrDefault(labeledExample.Text, tokenizerRuleSet);
            if (ngrams != null)
                return new TokenizedExample(labeledExample, ngrams);

            return null;

        }
        public TokenizedExample CreateOrDefault(LabeledExample labeledExample)
            => CreateOrDefault(labeledExample, DefaultTokenizerRuleSet);

        public List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateList(labeledExamples, nameof(labeledExamples));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<TokenizedExample> tokenizedExamples = new List<TokenizedExample>();
            foreach (LabeledExample labeledExample in labeledExamples)
            {

                TokenizedExample tokenizedExample = CreateOrDefault(labeledExample, tokenizerRuleSet);
                if (labeledExample != null)
                    tokenizedExamples.Add(tokenizedExample);

            }

            if (labeledExamples.Count == 0)
                return null;

            return tokenizedExamples;

        }
        public List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples)
            => CreateOrDefault(labeledExamples, DefaultTokenizerRuleSet);

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2022
*/
