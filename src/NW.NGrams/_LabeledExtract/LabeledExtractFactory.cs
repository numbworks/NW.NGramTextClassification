using System;
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
        // Constructors
        public LabeledExtractFactory
            (INGramsTokenizer tokenizer, uint initialId)
        {

            ValidateObject(tokenizer, nameof(tokenizer));

            _tokenizer = tokenizer;
            _initialId = initialId;

        }

        // Methods (public)
        public LabeledExtract CreateFor<T>
            (ulong id, string label, ITokenizationStrategy strategy, string text) where T : INGram
        {

            ValidateString(label, nameof(label));
            ValidateObject(strategy, nameof(strategy));
            ValidateString(text, nameof(text));

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

            ValidateString(label, nameof(label));
            ValidateObject(ruleSet, nameof(ruleSet));
            ValidateObject(strategy, nameof(strategy));
            ValidateString(text, nameof(text));

            List<INGram> nGrams = _tokenizer.DoForRuleset(ruleSet, strategy, text);
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

            ValidateList(tuples, nameof(tuples));
            ValidateObject(strategy, nameof(strategy));

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

            ValidateList(tuples, nameof(tuples));
            ValidateObject(ruleSet, nameof(ruleSet));
            ValidateObject(strategy, nameof(strategy));

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
        private T CreateException<T>(string message) where T : Exception
            => (T)Activator.CreateInstance(typeof(T), message);

        private void ValidateObject<T>(object obj, string variableName) where T : Exception
        {

            if (obj == null)
                throw CreateException<T>(variableName);

        }
        private void ValidateObject(object obj, string variableName)
            => ValidateObject<ArgumentNullException>(obj, variableName);

        private void ValidateString<T>(string str, string variableName) where T : Exception
        {

            if (string.IsNullOrWhiteSpace(str))
                throw CreateException<T>(variableName);

        }
        private void ValidateString(string str, string variableName)
            => ValidateString<ArgumentNullException>(str, variableName);

        private void ValidateList<T, U>(List<U> list, string variableName) where T : Exception
        {

            if (list == null)
                throw CreateException<T>(variableName);
            if (list.Count == 0)
                throw CreateException<T>(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }
        private void ValidateList<U>(List<U> list, string variableName)
            => ValidateList<ArgumentNullException, U>(list, variableName);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
