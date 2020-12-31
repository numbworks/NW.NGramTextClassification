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
        public List<string> Do(ITokenizationStrategy strategy, string text)
        {

            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (strategy.N < 1)
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(strategy.N)));

            // "This is a sample text." => "This", "is", ..., "text"
            MatchCollection matches = Regex.Matches(text, strategy.Pattern);
            if (matches.Count == 0)
                throw new Exception(MessageCollection.TheProvidedTokenizationStrategyPatternReturnsZeroMatches.Invoke(strategy));

            return GetTokens(strategy, matches);

        }

        // Methods (private)
        private List<string> GetTokens(ITokenizationStrategy strategy, MatchCollection matches)
        {

            // "This", "is", ..., "text" => ["This", "is", ..., "text"]
            string[] allWords = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                allWords[i] = matches[i].Value;

            List<string> tokens = new List<string>();
            for (uint i = 0; i < allWords.Length; i++)
            {

                // The last x NGrams are shorter in length...
                ushort currentN = strategy.N;
                if ((allWords.Length - i) < strategy.N)
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
                    tokens.Add(currentToken.ToString().ToLower());

                tokens.Add(currentToken.ToString());

            }

            return tokens;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/