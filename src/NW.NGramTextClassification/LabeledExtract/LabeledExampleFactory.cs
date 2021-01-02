using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public class LabeledExampleFactory : ILabeledExampleFactory
    {

        // Fields
        private INGramTokenizer _tokenizer;
        private uint _initialId;

        // Properties
        public static uint DefaultInitialId { get; } = 1;

        // Constructors
        public LabeledExampleFactory
            (INGramTokenizer tokenizer, uint initialId)
        {

            Validator.ValidateObject(tokenizer, nameof(tokenizer));

            _tokenizer = tokenizer;
            _initialId = initialId;

        }
        public LabeledExampleFactory()
            : this(new NGramTokenizer(), DefaultInitialId) { }

        // Methods (public)
        public LabeledExample Create
            (ulong id, string label, string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet)
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
            (List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet)
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

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/
