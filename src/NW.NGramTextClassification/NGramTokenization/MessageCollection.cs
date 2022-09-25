using System;

namespace NW.NGramTextClassification.NGramTokenization
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NGramTokenization"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<ITokenizationStrategy, string> ProvidedTokenizationStrategyPatternReturnsZeroMatches =
            (tokenizationStrategy) => $"The provided {nameof(ITokenizationStrategy)} pattern ('{tokenizationStrategy.Pattern}') retuns zero matches against the provided text.";

        public static string AtLeastOneArgumentMustBeTrue = "At least one argument must be true.";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/