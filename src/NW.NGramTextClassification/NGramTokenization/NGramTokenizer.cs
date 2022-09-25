using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NW.NGramTextClassification.Arrays;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.NGramTokenization
{
    /// <inheritdoc cref="INGramTokenizer"/>
    public class NGramTokenizer : INGramTokenizer
    {

        #region Fields

        private IArrayManager _arrayManager;
        private ITokenizationStrategy _tokenizationStrategy;

        #endregion

        #region Properties

        public static IArrayManager DefaultArrayManager { get; } = new ArrayManager();
        public static ITokenizationStrategy DefaultTokenizationStrategy { get; } = new TokenizationStrategy();

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="NGramTokenizer"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/> 
        public NGramTokenizer(IArrayManager arrayManager, ITokenizationStrategy tokenizationStrategy)
        {

            Validator.ValidateObject(arrayManager, nameof(arrayManager));
            Validator.ValidateObject(tokenizationStrategy, nameof(tokenizationStrategy));

            _arrayManager = arrayManager;
            _tokenizationStrategy = tokenizationStrategy;

        }

        /// <summary>Initializes a <see cref="NGramTokenizer"/> instance using default parameters.</summary>
        public NGramTokenizer()
            : this(DefaultArrayManager, DefaultTokenizationStrategy) { }

        #endregion

        #region Methods_public

        public ushort GetN<T>() where T : INGram
            => ((INGram)CreateInstance<T>(DefaultTokenizationStrategy, "whatever_value")).N;

        public List<Monogram> DoForMonogram(string text)
            => DoFor<Monogram>(text);
        public List<Bigram> DoForBigram(string text)
            => DoFor<Bigram>(text);
        public List<Trigram> DoForTrigram(string text)
            => DoFor<Trigram>(text);
        public List<Fourgram> DoForFourgram(string text)
            => DoFor<Fourgram>(text);
        public List<Fivegram> DoForFivegram(string text)
            => DoFor<Fivegram>(text);

        public List<INGram> DoForRuleSet(string text, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<INGram> ngrams = new List<INGram>();

            if (tokenizerRuleSet.DoForMonogram)
            {

                List<Monogram> current = DoFor<Monogram>(text);
                ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForBigram)
            {

                List<Bigram> current = DoFor<Bigram>(text);
                ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForTrigram)
            {

                List<Trigram> current = DoFor<Trigram>(text);
                ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForFourgram)
            {

                List<Fourgram> current = DoFor<Fourgram>(text);
                ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForFivegram)
            {

                List<Fivegram> current = DoFor<Fivegram>(text);
                ngrams.AddRange(current);

            }

            return ngrams;

        }
        public List<INGram> DoForRuleSetOrDefault(string text, INGramTokenizerRuleSet tokenizerRuleSet)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));

            List<INGram> ngrams = new List<INGram>();

            if (tokenizerRuleSet.DoForMonogram)
            {

                List<Monogram> current;
                bool status = TryDoFor(text, out current);
                if (status)
                    ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForBigram)
            {

                List<Bigram> current;
                bool status = TryDoFor(text, out current);
                if (status)
                    ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForTrigram)
            {

                List<Trigram> current;
                bool status = TryDoFor(text, out current);
                if (status)
                    ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForFourgram)
            {

                List<Fourgram> current;
                bool status = TryDoFor(text, out current);
                if (status)
                    ngrams.AddRange(current);

            }
            if (tokenizerRuleSet.DoForFivegram)
            {

                List<Fivegram> current;
                bool status = TryDoFor(text, out current);
                if (status)
                    ngrams.AddRange(current);

            }

            if (ngrams.Count == 0)
                return null;

            return ngrams;

        }

        #endregion

        #region Methods_private

        private T CreateInstance<T>(params object[] args)
            => (T)Activator.CreateInstance(typeof(T), args);
        private MatchCollection GetMatches(string text)
        {

            // "This is a sample text." => "This", "is", ..., "text"
            MatchCollection matches = Regex.Matches(text, _tokenizationStrategy.Pattern);

            return matches;

        }
        private string[] ConvertToArray(MatchCollection matches)
        {

            string[] allWords = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                allWords[i] = matches[i].Value;

            return allWords;

        }
        private void Validate<T>(string text) where T : INGram
        {

            Validator.ValidateStringNullOrWhiteSpace<ArgumentNullException>(text, nameof(text));

            MatchCollection matches = GetMatches(text);
            if (matches.Count == 0)
                throw new ArgumentException(Messages.MessageCollection.NGramsTokenizer_ProvidedTokenizationStrategyPatternReturnsZeroMatches(_tokenizationStrategy));

            ushort N = GetN<T>();
            Validator.ThrowIfFirstIsGreater<ArgumentException>(N, nameof(N), matches.Count, nameof(matches.Count));

        }
        private List<T> DoFor<T>(string text) where T : INGram
        {

            /*
             
                1. For N = 3: ["This", "is", "a"], ["is", "a", "sample"], ...
                2. [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ]
                3. [ "This", " ", "is", " ", "a" ] => "This is a"
                4. "This is a" => "this is a"
                5. new Trigram(strategy, "this is a")

            */

            Validate<T>(text);

            ushort N = GetN<T>();
            MatchCollection matches = GetMatches(text);
            string[] allWords = ConvertToArray(matches);

            List<T> tokens = new List<T>();
            for (uint i = 0; i < allWords.Length; i++)
            {

                ushort currentN = N;
                if ((allWords.Length - i) < N)
                    currentN = (ushort)(allWords.Length - i); // The last x NGrams are shorter in length

                string[] currentSubset = _arrayManager.GetSubset(allWords, i, currentN);
                currentSubset = _arrayManager.AddDelimiter(currentSubset, _tokenizationStrategy.Delimiter);

                StringBuilder currentToken = new StringBuilder();
                foreach (string word in currentSubset)
                    currentToken.Append(word);

                if (_tokenizationStrategy.ToLowercase)
                    currentToken = new StringBuilder(currentToken.ToString().ToLower());

                T nGram = CreateInstance<T>(_tokenizationStrategy, currentToken.ToString());

                tokens.Add(nGram);

            }

            return tokens;

        }
        private bool TryDoFor<T>(string text, out List<T> ngrams) where T : INGram
        {

            try
            {

                ngrams = DoFor<T>(text);
                return true;

            }
            catch
            {


                ngrams = null;
                return false;

            }

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2022
*/