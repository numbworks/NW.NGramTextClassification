using System.Collections.Generic;
using System.Linq;

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
        public LabeledExtract CreateFor<T>
            (ulong id, string label, ITokenizationStrategy strategy, string text) where T : INGram
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));

            List<INGram> nGrams = _tokenizer.DoFor<T>(strategy, text).Select(item => (INGram)item).ToList();
            LabeledExtract labeledExtract = new LabeledExtract(id, label, text, nGrams);

            return labeledExtract;

        }
        public LabeledExtract CreateFor<T>
            (ulong id, string label, string text) where T : INGram
            => CreateFor<T>(id, label, new TokenizationStrategy(), text);

        public LabeledExtract CreateForRuleset
            (ulong id, string label, INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy, string text)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));

            List<INGram> nGrams = _tokenizer.Do(ruleSet, strategy, text);
            LabeledExtract labeledExtract = new LabeledExtract(id, label, text, nGrams);

            return labeledExtract;

        }
        public LabeledExtract CreateForRuleset(ulong id, string label, ITokenizationStrategy strategy, string text)
            => CreateForRuleset(id, label, new NGramsTokenizerRuleSet(), strategy, text);
        public LabeledExtract CreateForRuleset(ulong id, string label, string text)
            => CreateForRuleset(id, label, new NGramsTokenizerRuleSet(), new TokenizationStrategy(), text);

        public List<LabeledExtract> CreateFor<T>
            (List<(string label, string text)> tuples, ITokenizationStrategy strategy) where T : INGram
        {

            Validator.ValidateList(tuples, nameof(tuples));
            Validator.ValidateObject(strategy, nameof(strategy));

            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>();

            uint currentId = _initialId;
            foreach ((string label, string text) tuple in tuples)
            {

                LabeledExtract labeledExtract = CreateFor<T>(currentId, tuple.label, strategy, tuple.text);
                labeledExtracts.Add(labeledExtract);

                currentId++;

            }

            return labeledExtracts;

        }
        public List<LabeledExtract> CreateFor<T>(List<(string label, string text)> tuples) where T : INGram
            => CreateFor<T>(tuples, new TokenizationStrategy());

        public List<LabeledExtract> CreateForRuleset
            (List<(string label, string text)> tuples, INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy)
        {

            Validator.ValidateList(tuples, nameof(tuples));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));
            Validator.ValidateObject(strategy, nameof(strategy));

            List<LabeledExtract> labeledExtracts = new List<LabeledExtract>();

            uint currentId = _initialId;
            foreach ((string label, string text) tuple in tuples)
            {

                LabeledExtract labeledExtract = CreateForRuleset(currentId, tuple.label, ruleSet, strategy, tuple.text);
                labeledExtracts.Add(labeledExtract);

                currentId++;

            }

            return labeledExtracts;

        }
        public List<LabeledExtract> CreateForRuleset(List<(string label, string text)> tuples, ITokenizationStrategy strategy)
            => CreateForRuleset(tuples, new NGramsTokenizerRuleSet(), strategy);
        public List<LabeledExtract> CreateForRuleset(List<(string label, string text)> tuples)
            => CreateForRuleset(tuples, new NGramsTokenizerRuleSet(), new TokenizationStrategy());

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
