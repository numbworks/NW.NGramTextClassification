using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.TextClassifications
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.NGramTextClassification.TextClassifications"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static string AttemptingToPredictLabel = "Attempting to predict the label of the provided text...";
        public static Func<string, string> FollowingTextHasBeenProvided =
            (text) => $"The following text has been provided: '{text}'.";
        public static Func<List<LabeledExample>, string> XLabeledExamplesHaveBeenProvided =
            (labeledExamples) => $"'{labeledExamples.Count}' {nameof(LabeledExample)} objects have been provided.";
        public static Func<List<INGram>, string> ProvidedTextHasBeenTokenizedIntoXNGrams =
            (nGrams) => $"The provided text has been tokenized into '{nGrams?.Count ?? 0}' {nameof(INGram)} object.";

        public static string ProvidedLabeledExamplesThruTokenizationProcess 
            = $"The provided {nameof(LabeledExample)} objects have been thru the tokenization process.";
        public static string AtLeastOneLabeledExampleFailedTokenized
            = $"At least one {nameof(LabeledExample)} object failed to be tokenized.";

        public static Func<INGramTokenizerRuleSet, string> FollowingNGramsTokenizerRuleSetWillBeUsed =
            (ruleset) => $"The following '{nameof(INGramTokenizerRuleSet)}' object will be used: '{ruleset}'.";
        public static string TokenizedTextComparedAgainstProvidedTokenizedExamples =
            $"The tokenized text has been successfully compared against the provided list of {nameof(TokenizedExample)} objects.";
        public static Func<List<SimilarityIndex>, string> XSimilarityIndexObjectsHaveBeenComputed =
            (similarityIndexes) => $"'{similarityIndexes.Count}' {nameof(SimilarityIndex)} objects have been computed.";
        public static Func<List<SimilarityIndexAverage>, string> XSimilarityIndexAverageObjectsHaveBeenComputed =
            (indexAverages) => $"'{indexAverages.Count}' {nameof(SimilarityIndexAverage)} objects have been computed.";

        public static Func<string, string> PredictedLabelIs =
            (label) => $"The predicted label is: '{label}'.";
        public static string PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples =
                $"The prediction has failed. Try increasing the amount of provided {nameof(LabeledExample)} objects.";
        public static string PredictionHasBeenSuccessful =
                $"The prediction has been successful.";

        public static Func<TokenizedExample, string> ComparingProvidedTextAgainstFollowingTokenizedExample =
            (tokenizedExample) => $"Comparing the provided text against the following {nameof(TokenizedExample)}: '{tokenizedExample}'...";
        public static Func<double, string> CalculatedSimilarityIndexValueIs =
            (indexValue) => $"The calculated '{nameof(SimilarityIndex)}' value is '{indexValue}'.";
        public static Func<double, string> RoundedSimilarityIndexValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndex)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndex, string> FollowingSimilarityIndexObjectHasBeenAddedToTheList =
            (similarityIndex) => $"The following {nameof(SimilarityIndex)} object has been added to the list: '{similarityIndex}'.";
        public static Func<List<string>, string> FollowingUniqueLabelsHaveBeenFound =
            (uniqueLabels) => $"The following unique labels have been found in the provided {nameof(SimilarityIndex)} list: '{RollOutCollection(uniqueLabels)}'.";
        
        public static Func<string, string> CalculatingIndexAverageForTheFollowingLabel =
            (label) => $"Calculating '{nameof(SimilarityIndexAverage)}' for the following label: '{label}'...";
        public static Func<double, string> CalculatedSimilarityIndexAverageValueIs =
            (averageValue) => $"The calculated '{nameof(SimilarityIndexAverage)}' value is '{averageValue}'.";
        public static Func<double, string> RoundedSimilarityIndexAverageValueIs =
            (roundedValue) => $"The rounded '{nameof(SimilarityIndexAverage)}' value is '{roundedValue}'.";
        public static Func<SimilarityIndexAverage, string> FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList =
            (indexAverage) => $"The following {nameof(SimilarityIndexAverage)} object has been added to the list: '{indexAverage}'.";

        public static Func<string, string> FollowingVerificationHasBeenSuccessful =
            (name) => $"The following verification has been successful: '{name}'.";
        public static Func<string, string> FollowingVerificationHasFailed =
            (name) => $"The following verification has failed: '{name}'.";
        public static Func<SimilarityIndexAverage, string> SimilarityIndexAverageWithTheHighestValueIs =
            (indexAverage) => $"The '{nameof(SimilarityIndexAverage)}' object with the highest value is: '{indexAverage}'.";
        public static Func<string, string> AllRulesInProvidedRulesetFailed
            = (text) => $"All the rules in the provided ruleset failed for the provided text ('{text}'), therefore a 'null' label will be returned.";

        #endregion

        #region Methods

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
    Last Update: 25.09.2022
*/