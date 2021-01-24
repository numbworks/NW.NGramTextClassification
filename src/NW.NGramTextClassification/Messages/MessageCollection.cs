using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public static class MessageCollection
    {

        // Validator
        public static Func<string, string, string> FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";

        // NGramsTokenizer
        public static Func<ITokenizationStrategy, string> TheProvidedTokenizationStrategyPatternReturnsZeroMatches =
            (tokenizationStrategy) => $"The provided {nameof(ITokenizationStrategy)} pattern ('{tokenizationStrategy.Pattern}') retuns zero matches against the provided text.";

        // TextClassifier
        public static string AttemptingToPredictLabel ="Attempting to predict the label of the provided text...";
        public static Func<string, string> TheFollowingTextHasBeenProvided =
            (text) => $"The following text has been provided: '{text}'.";
        public static Func<List<LabeledExample>, string> XLabeledExamplesHaveBeenProvided =
            (labeledExamples) => $"'{labeledExamples.Count.ToString()}' {nameof(LabeledExample)} objects have been provided.";
        public static Func<List<INGram>, string> TheProvidedTextHasBeenTokenizedIntoXNGrams =
            (nGrams) => $"The provided text has been tokenized into '{nGrams.Count.ToString()}' {nameof(INGram)} object.";
        public static Func<ITokenizationStrategy, string> TheFollowingTokenizationStrategyWillBeUsed =
            (strategy) => $"The following '{nameof(ITokenizationStrategy)}' object will be used: '{strategy.ToString()}'.";
        public static Func<INGramTokenizerRuleSet, string> TheFollowingNGramsTokenizerRuleSetWillBeUsed =
            (ruleset) => $"The following '{nameof(INGramTokenizerRuleSet)}' object will be used: '{ruleset.ToString()}'.";
        public static string TheTokenizedTextHasBeenComparedAgainstTheProvidedLabeledExamples =
            $"The tokenized text has been successfully compared against the provided list of {nameof(LabeledExample)} objects.";
        public static Func<List<SimilarityIndex>, string> XSimilarityIndexObjectsHaveBeenComputed =
            (similarityIndexes) => $"'{similarityIndexes.Count.ToString()}' {nameof(SimilarityIndex)} objects have been computed.";
        public static Func<List<SimilarityIndexAverage>, string> XSimilarityIndexAverageObjectsHaveBeenComputed =
            (indexAverages) => $"'{indexAverages.Count.ToString()}' {nameof(SimilarityIndexAverage)} objects have been computed.";
        public static Func<string, string> ThePredictedLabelIs =
            (label) => $"The predicted label is: '{label}'.";
        public static string ThePredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples =
                $"The prediction has failed. Try increasing the amount of provided {nameof(LabeledExample)} objects.";
        public static string ThePredictionHasBeenSuccessful =
                $"The prediction has been successful.";

        public static Func<LabeledExample, string> ComparingProvidedTextAgainstFollowingLabeledExample =
            (labeledExample) => $"Comparing the provided text against the following {nameof(LabeledExample)}: '{labeledExample.ToString()}'...";
        public static Func<double, string> TheCalculatedSimilarityIndexValueIs =
            (indexValue) => $"The calculated '{nameof(SimilarityIndex)}' value is '{indexValue}'.";
        public static Func<double, string> TheRoundedSimilarityIndexValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndex)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndex, string> TheFollowingSimilarityIndexObjectHasBeenAddedToTheList =
            (similarityIndex) => $"The following {nameof(SimilarityIndex)} object has been added to the list: '{similarityIndex.ToString()}'.";
        public static Func<List<string>, string> TheFollowingUniqueLabelsHaveBeenFound =
            (uniqueLabels) => $"The following unique labels have been found in the provided {nameof(SimilarityIndex)} list: '{RollOutCollection(uniqueLabels)}'.";
        public static Func<string, string> CalculatingIndexAverageForTheFollowingLabel =
            (label) => $"Calculating '{nameof(SimilarityIndexAverage)}' for the following label: '{label}'...";
        public static Func<double, string> TheCalculatedSimilarityIndexAverageValueIs =
            (averageValue) => $"The calculated '{nameof(SimilarityIndexAverage)}' value is '{averageValue}'.";
        public static Func<double, string> TheRoundedSimilarityIndexAverageValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndexAverage)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndexAverage, string> TheFollowingSimilarityIndexAverageObjectHasBeenAddedToTheList =
            (indexAverage) => $"The following {nameof(SimilarityIndexAverage)} object has been added to the list: '{indexAverage.ToString()}'.";
        public static Func<string, string> FollowingVerificationHasBeenSuccessful =
            (name) => $"The following verification has been successful: '{name}'.";
        public static Func<string, string> FollowingVerificationHasFailed =
            (name) => $"The following verification has failed: '{name}'.";
        public static Func<SimilarityIndexAverage, string> TheSimilarityIndexAverageWithTheHighestValueIs = 
            (indexAverage) => $"The '{nameof(SimilarityIndexAverage)}' object with the highest value is: '{indexAverage}'.";

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