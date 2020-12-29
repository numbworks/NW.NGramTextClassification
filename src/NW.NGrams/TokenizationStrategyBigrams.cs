using System;

namespace NW.NGrams
{
    public class TokenizationStrategyBigrams : ITokenizationStrategy
    {

        // Fields
        // Properties
        public string Pattern { get; set; } = "[\\w0-9_]{1,}";
        public string Delimiter { get; set; } = " ";
        public UInt16 N { get; set; } = 2;
        public Boolean ConvertAllToLowercase { get; set; } = true;

        // Constructors
        public TokenizationStrategyBigrams() { }

        // Methods

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 13.07.2018 
 *  Description: It represents a tokenization strategy for bigrams.
 * 
 */
