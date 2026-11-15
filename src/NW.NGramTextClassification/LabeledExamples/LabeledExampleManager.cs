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

        private INGramTokenizer _tokenizer;

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

        #region Methods (public)

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

        public TokenizedExample CreateOrDefault(LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet)
            => CreateTokenizedExample(labeledExample, tokenizerRuleSet, _tokenizer.DoForRuleSetOrDefault, () => DefaultTokenizedExample);
        public TokenizedExample CreateOrDefault(LabeledExample labeledExample)
            => CreateOrDefault(labeledExample, DefaultTokenizerRuleSet);
        public List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
            => CreateTokenizedExamples(labeledExamples, tokenizerRuleSet, CreateOrDefault, () => DefaultTokenizedExamples);
        public List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples)
            => CreateOrDefault(labeledExamples, DefaultTokenizerRuleSet);
                
        public TokenizedExample Create(LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet)
            => CreateTokenizedExample(labeledExample, tokenizerRuleSet, _tokenizer.DoForRuleSet, () => null);
        public TokenizedExample Create(LabeledExample labeledExample)
            => Create(labeledExample, DefaultTokenizerRuleSet);
        public List<TokenizedExample> Create(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet)
            => CreateTokenizedExamples(labeledExamples, tokenizerRuleSet, Create, () => null);
        public List<TokenizedExample> Create(List<LabeledExample> labeledExamples)
            => Create(labeledExamples, DefaultTokenizerRuleSet);

        #endregion

        #region Methods (private)

        private TokenizedExample CreateTokenizedExample(
            LabeledExample labeledExample,
            INGramTokenizerRuleSet tokenizerRuleSet,
            Func<string, INGramTokenizerRuleSet, List<INGram>> tokenizeFunction,
            Func<TokenizedExample> defaultValueFactory)
        {

            Validator.ValidateObject(labeledExample, nameof(labeledExample));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<INGram> ngrams = tokenizeFunction(labeledExample.Text, tokenizerRuleSet);

            if (ngrams != null)
                return new TokenizedExample(labeledExample, ngrams);

            return defaultValueFactory?.Invoke();

        }
        
        private List<TokenizedExample> CreateTokenizedExamples(
            List<LabeledExample> labeledExamples,
            INGramTokenizerRuleSet tokenizerRuleSet,
            Func<LabeledExample, INGramTokenizerRuleSet, TokenizedExample> createFunction,
            Func<List<TokenizedExample>> defaultValueFactory)
        {

            Validator.ValidateList(labeledExamples, nameof(labeledExamples));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<TokenizedExample> tokenizedExamples = new();

            foreach (LabeledExample labeledExample in labeledExamples)
            {
                TokenizedExample tokenizedExample = createFunction(labeledExample, tokenizerRuleSet);

                if (tokenizedExample != null)
                    tokenizedExamples.Add(tokenizedExample);
            }

            if (tokenizedExamples.Count != labeledExamples.Count)
                return defaultValueFactory?.Invoke();

            return tokenizedExamples;

        }

        #endregion

    }
}
