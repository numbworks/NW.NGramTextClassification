using System;

namespace NW.NGramTextClassification.NGramTokenization
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NGramTokenization"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<ITokenizationStrategy, string, string> ProvidedTokenizationStrategyPatternReturnsZeroMatches =
            (tokenizationStrategy, text) => $"The provided {nameof(ITokenizationStrategy)} pattern ('{tokenizationStrategy.Pattern}') retuns zero matches against the provided text ('{text}').";

        public static Func<int, string, string> ProvidedTextCantBeTokenized = (n, text) => 
        {
            string suffix = n switch
            {
                1 => "using monograms",
                2 => "using bigrams",
                3 => "using trigrams",
                4 => "using fourgrams",
                5 => "using fivegrams",
                _ => $"for N='{n}'"
            };

            return $"The provided text ('{text}') can't be tokenized {suffix}.";

        };

        public static string AtLeastOneArgumentMustBeTrue = "At least one argument must be true.";

        #endregion

    }
}