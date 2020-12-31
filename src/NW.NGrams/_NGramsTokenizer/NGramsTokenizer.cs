using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NW.NGrams
{
    public class NGramsTokenizer : INGramsTokenizer
    {

        // Fields
        private IArrayManager _ArrayManager;

        // Properties
        // Constructors
        public NGramsTokenizer(IArrayManager arrayManager)
        {

            if (arrayManager == null)
                throw new ArgumentNullException(nameof(arrayManager));

            _ArrayManager = arrayManager;

        }
        public NGramsTokenizer()
            : this(new ArrayManager()) { }

        // Methods
        public List<T> Do<T>(ITokenizationStrategy strategy, string text) where T : INGram
        {

            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            // "This is a sample text." => "This", "is", ..., "text"
            MatchCollection matches = Regex.Matches(text, strategy.Pattern);
            if (matches.Count == 0)
                throw new Exception(MessageCollection.TheProvidedTokenizationStrategyPatternReturnsZeroMatches.Invoke(strategy));

            return GetTokens<T>(strategy, matches);

        }
        public List<T> Do<T>(string text) where T : INGram
            => Do<T>(new TokenizationStrategy(), text);

        public List<INGram> DoForMany
            (INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy, string text)
        {

            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            List<INGram> nGrams = new List<INGram>();

            if (ruleSet.DoForMonograms)
                nGrams.AddRange(Do<Monogram>(strategy, text));
            if (ruleSet.DoForBigrams)
                nGrams.AddRange(Do<Bigram>(strategy, text));
            if (ruleSet.DoForTrigrams)
                nGrams.AddRange(Do<Trigram>(strategy, text));

            return nGrams;

        }
        public List<INGram> DoForMany(ITokenizationStrategy strategy, string text)
                => DoForMany(new NGramsTokenizerRuleSet(), strategy, text);
        public List<INGram> DoForMany(string text)
                => DoForMany(new NGramsTokenizerRuleSet(), new TokenizationStrategy(), text);

        // Methods (private)
        private T CreateInstance<T>(params object[] args)
            => (T)Activator.CreateInstance(typeof(T), args);
        private ushort GetN<T>()
            => ((INGram)CreateInstance<T>()).N;
        private List<T> GetTokens<T>(ITokenizationStrategy strategy, MatchCollection matches)
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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/