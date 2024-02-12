using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <inheritdoc cref="ILabeledExampleManager"/>
    public class LabeledExampleManager : ILabeledExampleManager
    {

        #region Fields

        private NGramTokenization.INGramTokenizer _tokenizer;

        #endregion

        #region Properties

        public static INGramTokenizer DefaultNGramTokenizer { get; } = new NGramTokenizer();
        public static INGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();
        public static TokenizedExample DefaultTokenizedExample { get; } = null;
        public static List<TokenizedExample> DefaultTokenizedExamples { get; } = null;

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

            return DefaultTokenizedExample;

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
                if (tokenizedExample != null)
                    tokenizedExamples.Add(tokenizedExample);

            }

            if (tokenizedExamples.Count != labeledExamples.Count)
                return DefaultTokenizedExamples;

            return tokenizedExamples;

        }
        public List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples)
            => CreateOrDefault(labeledExamples, DefaultTokenizerRuleSet);

        public List<LabeledExample> CleanLabeledExamples
            (List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet, out List<LabeledExample> removed)
        {

            Validator.ValidateList(labeledExamples, nameof(labeledExamples));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<LabeledExample> clean = new List<LabeledExample>();
            removed = new List<LabeledExample>();

            foreach (LabeledExample labeledExample in labeledExamples)
            {

                TokenizedExample tokenizedExample = CreateOrDefault(labeledExample, tokenizerRuleSet);

                if (tokenizedExample == null)
                    removed.Add(labeledExample);
                else
                    clean.Add(labeledExample);

            }

            return clean;

        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 04.11.2022
*/
