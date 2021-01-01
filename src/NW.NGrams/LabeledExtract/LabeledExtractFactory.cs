using System.Collections.Generic;

namespace NW.NGrams
{
    public class LabeledExtractFactory : ILabeledExtractFactory
    {

        // Fields
        private INGramsTokenizer _tokenizer;
        private uint _initialId;

        // Properties
        public static uint DefaultInitialId { get; } = 1;

        // Constructors
        public LabeledExtractFactory
            (INGramsTokenizer tokenizer, uint initialId)
        {

            Validator.ValidateObject(tokenizer, nameof(tokenizer));

            _tokenizer = tokenizer;
            _initialId = initialId;

        }
        public LabeledExtractFactory()
            : this(new NGramsTokenizer(), DefaultInitialId) { }

        // Methods (public)
        public LabeledExtract Create
            (ulong id, string label, string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));

            List<INGram> nGrams = _tokenizer.Do(text, strategy, ruleSet);
            LabeledExtract labeledExtract = new LabeledExtract(id, label, text, nGrams);

            return labeledExtract;

        }
        public LabeledExtract Create(ulong id, string label, string text, ITokenizationStrategy strategy)
            => Create(id, label, text, strategy, new NGramsTokenizerRuleSet());
        public LabeledExtract Create(ulong id, string label, string text)
            => Create(id, label, text, new TokenizationStrategy());

        public List<LabeledExtract> Create
            (List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet)
        {

            Validator.ValidateList(tuples, nameof(tuples));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));

            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>();

            uint currentId = _initialId;
            foreach ((string label, string text) tuple in tuples)
            {

                LabeledExtract labeledExtract = Create(currentId, tuple.label, tuple.text, strategy, ruleSet);
                labeledExtracts.Add(labeledExtract);

                currentId++;

            }

            return labeledExtracts;

        }
        public List<LabeledExtract> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy)
            => Create(tuples, strategy, new NGramsTokenizerRuleSet());
        public List<LabeledExtract> Create(List<(string label, string text)> tuples)
            => Create(tuples, new TokenizationStrategy());

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/
