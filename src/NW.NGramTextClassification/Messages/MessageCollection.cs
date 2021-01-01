using System;

namespace NW.NGramTextClassification
{
    public static class MessageCollection
    {

        // ArrayManager
        public static Func<string, string> VariableContainsZeroItems { get; }
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> VariableCantBeLessThanOne { get; }
            = (variableName) => $"'{variableName}' can't be less than one.";

        // NGramsTokenizer
        public static Func<ITokenizationStrategy, string> TheProvidedTokenizationStrategyPatternReturnsZeroMatches =
            (tokenizationStrategy) => $"The provided {nameof(ITokenizationStrategy)} pattern ('{tokenizationStrategy.Pattern}') retuns zero matches against the provided text.";

        // NGramsTextClassifier
        public static Func<string, bool, string> TheMethodDidntReturnExpectedOutcome =
            (name, expected) => $"The '{name}' method didn't return the expected outcome ('{expected}').";

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/