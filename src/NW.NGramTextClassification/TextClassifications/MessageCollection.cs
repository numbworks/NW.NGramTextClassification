using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification.TextClassifications
{
    ///<summary>Collects all the messages used for logging and exceptions for <see cref="NW.NGramTextClassification.TextClassifications"/>.</summary>
    public static class MessageCollection
    {

        #region Properties

        public static Func<int, string> ProvidedSnippetsAre = (count) => $"The provided snippets are: '{count}'.";

        public static string AttemptingToClassifyProvidedSnippet = "Attempting to classify the provided snippet of text...";
        public static Func<string, string> FollowingSnippetHasBeenProvided =
            (snippet) => $"The following snippet has been provided: '{snippet}'.";
        public static Func<List<LabeledExample>, string> XLabeledExamplesHaveBeenProvided =
            (labeledExamples) => $"'{labeledExamples.Count}' {nameof(LabeledExample)} objects have been provided.";
        public static Func<List<INGram>, string> ProvidedTextHasBeenTokenizedIntoXNGrams =
            (nGrams) => $"The provided snippet has been tokenized into '{nGrams?.Count ?? 0}' {nameof(INGram)} object.";

        public static string ProvidedLabeledExamplesThruTokenizationProcess 
            = $"The provided {nameof(LabeledExample)} objects have been thru the tokenization process.";
        public static string AtLeastOneLabeledExampleFailedTokenized
            = $"At least one {nameof(LabeledExample)} object failed to be tokenized.";

        public static Func<INGramTokenizerRuleSet, string> FollowingNGramsTokenizerRuleSetWillBeUsed =
            (ruleset) => $"The following '{nameof(INGramTokenizerRuleSet)}' object will be used: '{ruleset}'.";
        public static string TokenizedSnippetComparedAgainstProvidedTokenizedExamples =
            $"The tokenized snippet has been successfully compared against the provided list of {nameof(TokenizedExample)} objects.";
        public static Func<List<SimilarityIndex>, string> XSimilarityIndexObjectsHaveBeenComputed =
            (similarityIndexes) => $"'{similarityIndexes.Count}' {nameof(SimilarityIndex)} objects have been computed.";
        public static Func<List<SimilarityIndexAverage>, string> XSimilarityIndexAverageObjectsHaveBeenComputed =
            (indexAverages) => $"'{indexAverages.Count}' {nameof(SimilarityIndexAverage)} objects have been computed.";

        public static Func<string, string> ResultOfClassificationTaskIs =
            (label) => $"The result of the classification task is: '{label}'.";
        public static string ClassificationTaskHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples =
                $"The classification task has failed. Try increasing the amount of provided {nameof(LabeledExample)} objects.";
        public static string ClassificationTaskHasBeenSuccessful =
                $"The classification task has been successful.";

        public static Func<TokenizedExample, string> ComparingProvidedSnippetAgainstFollowingTokenizedExample =
            (tokenizedExample) => $"Comparing the provided snippet against the following {nameof(TokenizedExample)}: '{tokenizedExample}'...";
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

        public static Func<string, string> FollowingVerificationReturnedTrue =
            (name) => $"The following verification returned true: '{name}'.";
        public static Func<string, string> FollowingVerificationReturnedFalse =
            (name) => $"The following verification returned false: '{name}'.";

        public static Func<SimilarityIndexAverage, string> SimilarityIndexAverageWithTheHighestValueIs =
            (indexAverage) => $"The '{nameof(SimilarityIndexAverage)}' object with the highest value is: '{indexAverage}'.";
        public static Func<string, string> AllRulesInProvidedRulesetFailed = 
            (snippet) => $"All the rules in the provided ruleset failed for the provided snippet ('{snippet}'), therefore a 'null' label will be returned.";

        public static Func<IFileInfoAdapter, string> AttemptingToLoadLabeledExamplesFrom = 
            (jsonFile) => $"Attempting to load a collection of '{nameof(LabeledExample)}' objects from: {jsonFile.FullName}.";
        public static string LabeledExamplesSuccessfullyLoaded = $"A collection of '{nameof(LabeledExample)}' objects has been successfully loaded.";
        public static string LabeledExamplesFailedToLoad = $"A collection of '{nameof(LabeledExample)}' objects failed to load. Default value is returned";

        public static Func<IFileInfoAdapter, string> AttemptingToLoadTextSnippetsFrom =
            (jsonFile) => $"Attempting to load a collection of '{nameof(TextSnippet)}' objects from: {jsonFile.FullName}.";
        public static string TextSnippetsSuccessfullyLoaded = $"A collection of '{nameof(TextSnippet)}' objects has been successfully loaded.";
        public static string TextSnippetsFailedToLoad = $"A collection of '{nameof(TextSnippet)}' objects failed to load. Default value is returned";

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
    Last Update: 12.10.2022
*/