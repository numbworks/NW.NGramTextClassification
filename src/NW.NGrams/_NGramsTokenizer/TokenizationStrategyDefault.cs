using System;

namespace NW.NGrams
{
    public class TokenizationStrategy : ITokenizationStrategy
    {

        // Fields
        // Properties
        public string Pattern { get; }
        public string Delimiter { get; }
        public bool ToLowercase { get; }

        // Constructors
        public TokenizationStrategy(string pattern, string delimiter, bool toLowercase)
        {

            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException(nameof(pattern));
            if (string.IsNullOrEmpty(delimiter))
                throw new ArgumentNullException(nameof(delimiter)); // Whitespace is a valid delimiter

            Pattern = pattern;
            Delimiter = delimiter;
            ToLowercase = toLowercase;

        }
        public TokenizationStrategy()
            : this("[\\w0-9_]{1,}", " ", true) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
