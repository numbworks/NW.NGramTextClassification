using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.Messages
{
    ///<summary>Collects all the messages used for logging and exceptions.</summary>
    public static class MessageCollection
    {

        #region Validator

        public static Func<string, string, string> Validator_FirstValueIsGreaterOrEqualThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater or equal than '{variableName2}''s value.";
        public static Func<string, string, string> Validator_FirstValueIsGreaterThanSecondValue
            = (variableName1, variableName2) => $"The '{variableName1}''s value is greater than '{variableName2}''s value.";
        public static Func<string, string> Validator_VariableContainsZeroItems
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> Validator_VariableCantBeLessThanOne
            = (variableName) => $"'{variableName}' can't be less than one.";

        #endregion

        #region NGramsTokenizer

        public static Func<ITokenizationStrategy, string> NGramsTokenizer_ProvidedTokenizationStrategyPatternReturnsZeroMatches =
            (tokenizationStrategy) => $"The provided {nameof(ITokenizationStrategy)} pattern ('{tokenizationStrategy.Pattern}') retuns zero matches against the provided text.";

        #endregion

        #region TextClassifier

        public static string TextClassifier_AttemptingToPredictLabel = "Attempting to predict the label of the provided text...";
        public static Func<string, string> TextClassifier_FollowingTextHasBeenProvided =
            (text) => $"The following text has been provided: '{text}'.";
        public static Func<List<LabeledExample>, string> TextClassifier_XLabeledExamplesHaveBeenProvided =
            (labeledExamples) => $"'{labeledExamples.Count.ToString()}' {nameof(LabeledExample)} objects have been provided.";
        public static Func<List<INGram>, string> TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams =
            (nGrams) => $"The provided text has been tokenized into '{nGrams.Count}' {nameof(INGram)} object.";
        public static Func<INGramTokenizerRuleSet, string> TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed =
            (ruleset) => $"The following '{nameof(INGramTokenizerRuleSet)}' object will be used: '{ruleset}'.";
        public static string TextClassifier_TokenizedTextHasBeenComparedAgainstTheProvidedLabeledExamples =
            $"The tokenized text has been successfully compared against the provided list of {nameof(LabeledExample)} objects.";
        public static Func<List<SimilarityIndex>, string> TextClassifier_XSimilarityIndexObjectsHaveBeenComputed =
            (similarityIndexes) => $"'{similarityIndexes.Count}' {nameof(SimilarityIndex)} objects have been computed.";
        public static Func<List<SimilarityIndexAverage>, string> TextClassifier_XSimilarityIndexAverageObjectsHaveBeenComputed =
            (indexAverages) => $"'{indexAverages.Count}' {nameof(SimilarityIndexAverage)} objects have been computed.";
        public static Func<string, string> TextClassifier_PredictedLabelIs =
            (label) => $"The predicted label is: '{label}'.";
        public static string TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples =
                $"The prediction has failed. Try increasing the amount of provided {nameof(LabeledExample)} objects.";
        public static string TextClassifier_PredictionHasBeenSuccessful =
                $"The prediction has been successful.";
        public static Func<LabeledExample, string> TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample =
            (labeledExample) => $"Comparing the provided text against the following {nameof(LabeledExample)}: '{labeledExample}'...";
        public static Func<double, string> TextClassifier_CalculatedSimilarityIndexValueIs =
            (indexValue) => $"The calculated '{nameof(SimilarityIndex)}' value is '{indexValue}'.";
        public static Func<double, string> TextClassifier_RoundedSimilarityIndexValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndex)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndex, string> TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList =
            (similarityIndex) => $"The following {nameof(SimilarityIndex)} object has been added to the list: '{similarityIndex}'.";
        public static Func<List<string>, string> TextClassifier_FollowingUniqueLabelsHaveBeenFound =
            (uniqueLabels) => $"The following unique labels have been found in the provided {nameof(SimilarityIndex)} list: '{RollOutCollection(uniqueLabels)}'.";
        public static Func<string, string> TextClassifier_CalculatingIndexAverageForTheFollowingLabel =
            (label) => $"Calculating '{nameof(SimilarityIndexAverage)}' for the following label: '{label}'...";
        public static Func<double, string> TextClassifier_CalculatedSimilarityIndexAverageValueIs =
            (averageValue) => $"The calculated '{nameof(SimilarityIndexAverage)}' value is '{averageValue}'.";
        public static Func<double, string> TextClassifier_RoundedSimilarityIndexAverageValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndexAverage)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndexAverage, string> TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList =
            (indexAverage) => $"The following {nameof(SimilarityIndexAverage)} object has been added to the list: '{indexAverage}'.";
        public static Func<string, string> TextClassifier_FollowingVerificationHasBeenSuccessful =
            (name) => $"The following verification has been successful: '{name}'.";
        public static Func<string, string> TextClassifier_FollowingVerificationHasFailed =
            (name) => $"The following verification has failed: '{name}'.";
        public static Func<SimilarityIndexAverage, string> TextClassifier_SimilarityIndexAverageWithTheHighestValueIs =
            (indexAverage) => $"The '{nameof(SimilarityIndexAverage)}' object with the highest value is: '{indexAverage}'.";

        #endregion

        #region Fields

        public static string RollOutCollection(IEnumerable<object> coll)
        {

            List<string> list = new List<string>();

            foreach (object obj in coll)
                list.Add(obj.ToString());

            return $"[{string.Join(", ", list)}]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/