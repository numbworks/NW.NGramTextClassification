using System;
using System.Collections.Generic;
using System.Dynamic;
using NW.Shared.Files;
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

        public static Func<Type, IFileInfoAdapter, string> AttemptingToLoadObjectsFrom =
            (type, jsonFile) => $"Attempting to load a collection of '{type.Name}' objects from: {jsonFile.FullName}.";
        public static Func<Type, string> ObjectsSuccessfullyLoaded =
            (type) => $"A collection of '{type.Name}' objects has been successfully loaded.";
        public static Func<Type, string> ObjectsFailedToLoad =
            (type) => $"A collection of '{type.Name}' objects failed to load. Default value is returned.";

        public static Func<Type, IFileInfoAdapter, string> AttemptingToLoadObjectFrom =
            (type, jsonFile) => $"Attempting to load a '{type.Name}' object from: {jsonFile.FullName}.";
        public static Func<Type, string> ObjectSuccessfullyLoaded =
            (type) => $"A '{type.Name}' object has been successfully loaded.";
        public static Func<Type, string> ObjectFailedToLoad =
            (type) => $"A '{type.Name}' object failed to load. Default value is returned.";

        public static Func<Type, IFileInfoAdapter, string> AttemptingToSaveObjectsAs =
            (type, jsonFile) => $"Attempting to save the provided collection of '{type.Name}' objects as: {jsonFile.FullName}.";
        public static Func<Type, string> ObjectsSuccessfullySaved =
            (type) => $"The provided collection of '{type.Name}' objects has been successfully saved.";
        public static Func<Type, string> ObjectsFailedToSave =
            (type) => $"The provided collection of '{type.Name}' objects failed to save.";

        public static Func<Type, IFileInfoAdapter, string> AttemptingToSaveObjectAs =
            (type, jsonFile) =>
                {

                    if (type == typeof(ExpandoObject))
                        return $"Attempting to save the provided '{typeof(TextClassifierSession).Name}' object as: {jsonFile.FullName}.";

                    return $"Attempting to save the provided '{type.Name}' object as: {jsonFile.FullName}.";

                };
        public static Func<Type, string> ObjectSuccessfullySaved =
            (type) =>
                {

                    if (type == typeof(ExpandoObject))
                        return $"The provided '{typeof(TextClassifierSession).Name}' object has been successfully saved.";

                    return $"The provided '{type.Name}' object has been successfully saved.";

                };
        public static Func<Type, string> ObjectFailedToSave =
            (type) =>
                {

                    if (type == typeof(ExpandoObject))
                        return $"The provided '{typeof(TextClassifierSession).Name}' object failed to save.";

                    return $"The provided '{type.Name}' object failed to save.";

                };

        public static Func<Type, string> ThereIsNoStrategyOutOfType =
             (type) => $"There is no built-in strategy to create a filename out of a '{type.Name}' object.";

        public static string AttemptingToCleanLabeledExamples = 
            $"Attempting to clean the provided {nameof(LabeledExample)} objects...";
        public static string ProvidedLabeledExamplesThruCleaningProcess = 
            $"The provided {nameof(LabeledExample)} objects have been thru the cleaning process.";
        public static Func<LabeledExample, string> ThisLabeledExampleHasBeenRemoved =
            (labeledExample) => $"This {nameof(LabeledExample)} object has been removed: '{labeledExample.Text}'.";
        public static string NoLabeledExampleHasBeenRemoved =
            $"No {nameof(LabeledExample)} object has been removed.";

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
    Last Update: 07.11.2022
*/