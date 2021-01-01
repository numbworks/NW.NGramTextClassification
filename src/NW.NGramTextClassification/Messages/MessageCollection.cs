using System;
using System.Collections.Generic;

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

        // TextClassifier
        public static Func<string, bool, string> FollowingVerificationHasBeenSuccessful =
            (name, expected) => $"The following verification has been successful: '{name}'.";
        public static Func<string, bool, string> FollowingVerificationHasFailed =
            (name, expected) => $"The following verification has failed: '{name}'.";
        public static Func<LabeledExtract, string> ComparingProvidedTextAgainstFollowingLabeledExample =
            (labeledExtract) => $"Comparing the provided text against the following {nameof(LabeledExtract)}: '{labeledExtract.ToString()}'...";
        public static Func<double, string> TheCalculatedSimilarityIndexValueIs =
            (indexValue) => $"The calculated Similarity Index value is '{indexValue}'.";
        public static Func<double, string> TheRoundedSimilarityIndexValueIs =
            (roundedValue) => $"The rounded Similarity Index value is '{roundedValue}'.";
        public static Func<SimilarityIndex, string> TheFollowingSimilarityIndexObjectHasBeenAddedToTheList =
            (similarityIndex) => $"The following {nameof(SimilarityIndex)} object has been added to the list: '{similarityIndex.ToString()}'.";

        // Methods
        public static string RollOutCollection(IEnumerable<object> coll)
        {

            List<string> list = new List<string>();

            foreach (object obj in coll)
                list.Add(obj.ToString());

            return $"[{string.Join(", ", list)}]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2021

*/