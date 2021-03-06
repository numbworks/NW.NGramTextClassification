﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NW.NGramTextClassification
{
    public class NGramTokenizer : INGramTokenizer
    {

        // Fields
        private IArrayManager _ArrayManager;

        // Properties
        // Constructors
        public NGramTokenizer(IArrayManager arrayManager)
        {

            Validator.ValidateObject(arrayManager, nameof(arrayManager));

            _ArrayManager = arrayManager;

        }
        public NGramTokenizer()
            : this(new ArrayManager()) { }

        // Methods
        public List<INGram> Do
            (string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));

            List<INGram> nGrams = new List<INGram>();

            if (ruleSet.DoForMonograms)
                nGrams.AddRange(DoFor<Monogram>(text, strategy));
            if (ruleSet.DoForBigrams)
                nGrams.AddRange(DoFor<Bigram>(text, strategy));
            if (ruleSet.DoForTrigrams)
                nGrams.AddRange(DoFor<Trigram>(text, strategy));
            if (ruleSet.DoForFourgrams)
                nGrams.AddRange(DoFor<Fourgram>(text, strategy));
            if (ruleSet.DoForFivegrams)
                nGrams.AddRange(DoFor<Fivegram>(text, strategy));

            return nGrams;

        }
        public List<INGram> Do(string text, ITokenizationStrategy strategy)
            => Do(text, strategy, new NGramTokenizerRuleSet());
        public List<INGram> Do(string text, INGramTokenizerRuleSet ruleSet)
            => Do(text, new TokenizationStrategy(), ruleSet);
        public List<INGram> Do(string text)
            => Do(text, new TokenizationStrategy(), new NGramTokenizerRuleSet());

        // Methods (private)
        private T CreateInstance<T>(params object[] args)
            => (T)Activator.CreateInstance(typeof(T), args);
        private ushort GetN<T>()
            => ((INGram)CreateInstance<T>(new TokenizationStrategy(), "whatever_value")).N;
        private List<T> GetTokens<T>(MatchCollection matches, ITokenizationStrategy strategy)
        {

            // "This", "is", ..., "text" => ["This", "is", ..., "text"]
            string[] allWords = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                allWords[i] = matches[i].Value;

            ushort N = GetN<T>();

            List<T> tokens = new List<T>();
            for (uint i = 0; i < allWords.Length; i++)
            {

                // The last x NGrams are shorter in length...
                ushort currentN = N;
                if ((allWords.Length - i) < N)
                    currentN = (ushort)(allWords.Length - i);

                // For N = 3: ["This", "is", "a"], ["is", "a", "sample"], ...
                string[] currentSubset = _ArrayManager.GetSubset(allWords, i, currentN);

                // [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ]
                currentSubset = _ArrayManager.AddDelimiter(currentSubset, strategy.Delimiter);

                // [ "This", " ", "is", " ", "a" ] => "This is a"
                StringBuilder currentToken = new StringBuilder();
                foreach (string word in currentSubset)
                    currentToken.Append(word);

                // "This is a" => "this is a"
                if (strategy.ToLowercase)
                    currentToken = new StringBuilder(currentToken.ToString().ToLower());

                // new Monogram(strategy, "this is a")
                T nGram = CreateInstance<T>(strategy, currentToken.ToString());

                tokens.Add(nGram);

            }

            return tokens;

        }
        private List<T> DoFor<T>(string text, ITokenizationStrategy strategy) where T : INGram
        {

            // "This is a sample text." => "This", "is", ..., "text"
            MatchCollection matches = Regex.Matches(text, strategy.Pattern);
            if (matches.Count == 0)
                throw new Exception(MessageCollection.NGramsTokenizer_ProvidedTokenizationStrategyPatternReturnsZeroMatches.Invoke(strategy));

            return GetTokens<T>(matches, strategy);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/