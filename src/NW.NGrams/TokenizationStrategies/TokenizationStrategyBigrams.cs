using System;

namespace NW.NGrams
{
    public class TokenizationStrategyBigrams : ITokenizationStrategy
    {

        // Fields
        // Properties
        public string Pattern { get; }
        public string Delimiter { get; }
        public ushort N { get; }
        public bool ConvertAllToLowercase { get; }

        // Constructors
        public TokenizationStrategyBigrams
            (string pattern, string delimiter, bool convertAllToLowercase)
        {

            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException(nameof(pattern));

            Pattern = pattern;
            Delimiter = delimiter;
            N = 2;
            ConvertAllToLowercase = convertAllToLowercase;

        }
        public TokenizationStrategyBigrams()
            : this(
                  TokenizationStrategyDefaultProperties.Pattern,
                  TokenizationStrategyDefaultProperties.Delimiter,
                  TokenizationStrategyDefaultProperties.ConvertAllToLowercase) { }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
