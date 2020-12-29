using System;

namespace NW.NGrams
{
    public class TokenizationStrategyMonograms : ITokenizationStrategy
    {

        // Fields
        // Properties
        public string Pattern { get; set; } = "[\\w0-9_]{1,}";
        public string Delimiter { get; set; } = " ";
        public UInt16 N { get; set; } = 1;
        public Boolean ConvertAllToLowercase { get; set; } = true;

        // Constructors
        public TokenizationStrategyMonograms() { }

        // Methods

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 18.02.2018 
 *  Description: It represents a tokenization strategy for monograms.
 *  
 *  Additional information: 
 *      "[\\w0-9_]{1,}": in a Unicode-aware environment such as .NET, 
 *      \w will match any letter of any alphabet (including Swedish ones: åäöÅÄÖ).
 * 
 */
