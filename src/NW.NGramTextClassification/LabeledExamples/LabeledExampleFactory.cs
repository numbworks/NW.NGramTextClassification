using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;

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

        public LabeledExample Create
            (ulong id, string label, string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));

            List<INGram> nGrams = _tokenizer.Do(text, strategy, ruleSet);
            LabeledExample labeledExample = new LabeledExample(id, label, text, nGrams);

            return labeledExample;

        }
        public LabeledExample Create(ulong id, string label, string text, ITokenizationStrategy strategy)
            => Create(id, label, text, strategy, new NGramTokenizerRuleSet());
        public LabeledExample Create(ulong id, string label, string text)
            => Create(id, label, text, new TokenizationStrategy());

        public List<LabeledExample> Create
            (List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet)
        {

            Validator.ValidateList(tuples, nameof(tuples));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));

            List<LabeledExample> labeledExamples = new List<LabeledExample>();

            uint currentId = _initialId;
            foreach ((string label, string text) tuple in tuples)
            {

                LabeledExample labeledExample = Create(currentId, tuple.label, tuple.text, strategy, ruleSet);
                labeledExamples.Add(labeledExample);

                currentId++;

            }

            return labeledExamples;

        }
        public List<LabeledExample> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy)
            => Create(tuples, strategy, new NGramTokenizerRuleSet());
        public List<LabeledExample> Create(List<(string label, string text)> tuples)
            => Create(tuples, new TokenizationStrategy());

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
