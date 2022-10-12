﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification
{
    /// <inheritdoc cref="ITextClassifier"/>
    public class TextClassifier : ITextClassifier
    {

        #region Fields

        private TextClassifierComponents _components;
        private TextClassifierSettings _settings;

        #endregion

        #region Properties

        public static TextClassifierComponents DefaultTextClassifierComponents { get; } = new TextClassifierComponents();
        public static TextClassifierSettings DefaultTextClassifierSettings { get; } = new TextClassifierSettings();
        public static INGramTokenizerRuleSet DefaultNGramTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();
        public static TextClassifierResult DefaultTextClassifierResult { get; }
            = new TextClassifierResult(null, new List<SimilarityIndex>(), new List<SimilarityIndexAverage>());

        public string Version { get; }
        public string AsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
        public TextClassifier(TextClassifierComponents components, TextClassifierSettings settings)
        {

            Validator.ValidateObject(components, nameof(components));
            Validator.ValidateObject(settings, nameof(settings));

            _components = components;
            _settings = settings;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            AsciiBanner = _components.AsciiBannerManager.Create(Version);

        }

        /// <summary>Initializes a <see cref="TextClassifier"/> instance using default parameters.</summary>
        public TextClassifier()
            : this(DefaultTextClassifierComponents, DefaultTextClassifierSettings) { }

        #endregion

        #region Methods_public

        public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateObject(textSnippet, nameof(textSnippet));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            LogInitialMessages(textSnippet, tokenizerRuleSet, labeledExamples);

            TextClassifierResult result = ClassifySingleOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);
            TextClassifierSession session = CreateSession(_settings, result, Version);

            return session;

        }
        public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, List<LabeledExample> labeledExamples)
                => ClassifyOrDefault(textSnippet, DefaultNGramTokenizerRuleSet, labeledExamples);

        public TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples) 
        {

            Validator.ValidateList(textSnippets, nameof(textSnippets));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            _components.LoggingAction(TextClassifications.MessageCollection.ProvidedSnippetsAre(textSnippets.Count));

            List<TextClassifierResult> results = new List<TextClassifierResult>();
            for (int i = 0; i < textSnippets.Count; i++)
            {

                TextClassifierResult textClassifierResult = ClassifySingleOrDefault(textSnippets[i], tokenizerRuleSet, labeledExamples);
                results.Add(textClassifierResult);

            }

            TextClassifierSession session = CreateSession(_settings, results, Version);

            return session;

        }
        public TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, List<LabeledExample> labeledExamples)
                => ClassifyMany(textSnippets, DefaultNGramTokenizerRuleSet, labeledExamples);

        public void LogAsciiBanner()
            => _components.LoggingActionAsciiBanner(AsciiBanner);

        public List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile)
        {

            Validator.ValidateObject(jsonFile, nameof(jsonFile));
            Validator.ValidateFileExistance(jsonFile);

            _components.LoggingAction(TextClassifications.MessageCollection.AttemptingToLoadLabeledExamplesFrom(jsonFile));

            string content = _components.FileManager.ReadAllText(jsonFile);
            List<LabeledExample> labeledExamples = _components.LabeledExampleSerializer.DeserializeFromJsonOrDefault(content);

            if (labeledExamples == LabeledExampleSerializer.Default)
                _components.LoggingAction(TextClassifications.MessageCollection.LabeledExamplesFailedToLoad);
            else
                _components.LoggingAction(TextClassifications.MessageCollection.LabeledExamplesSuccessfullyLoaded);

            return labeledExamples;

        }
        public List<LabeledExample> LoadLabeledExamplesOrDefault(string filePath)
            => LoadLabeledExamplesOrDefault(_components.FileManager.Create(filePath));

        #endregion

        #region Methods_private

        private void LogInitialMessages(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            string truncated = _components.TextTruncatingFunction(textSnippet.Text, _settings.TruncateTextInLogMessagesAfter);

            _components.LoggingAction(TextClassifications.MessageCollection.AttemptingToClassifyProvidedSnippet);
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingSnippetHasBeenProvided(truncated));
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet));
            _components.LoggingAction(TextClassifications.MessageCollection.XLabeledExamplesHaveBeenProvided(labeledExamples));

        }
        private TextClassifierResult ClassifySingleOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<INGram> nGrams = _components.NGramsTokenizer.DoForRuleSetOrDefault(textSnippet.Text, tokenizerRuleSet);
            _components.LoggingAction(TextClassifications.MessageCollection.ProvidedTextHasBeenTokenizedIntoXNGrams(nGrams));
            if (nGrams == null)
            {

                _components.LoggingAction(TextClassifications.MessageCollection.AllRulesInProvidedRulesetFailed(textSnippet.Text));
                return DefaultTextClassifierResult;

            }

            List<TokenizedExample> tokenizedExamples = _components.LabeledExampleManager.CreateOrDefault(labeledExamples, tokenizerRuleSet);
            _components.LoggingAction(TextClassifications.MessageCollection.ProvidedLabeledExamplesThruTokenizationProcess);
            if (tokenizedExamples == null)
            {

                _components.LoggingAction(TextClassifications.MessageCollection.AtLeastOneLabeledExampleFailedTokenized);
                return DefaultTextClassifierResult;

            }

            return CreateResult(nGrams, tokenizedExamples);

        }
        private TextClassifierResult CreateResult(List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
            _components.LoggingAction(TextClassifications.MessageCollection.TokenizedSnippetComparedAgainstProvidedTokenizedExamples);
            _components.LoggingAction(TextClassifications.MessageCollection.XSimilarityIndexObjectsHaveBeenComputed(indexes));

            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);
            _components.LoggingAction(TextClassifications.MessageCollection.XSimilarityIndexAverageObjectsHaveBeenComputed(indexAverages));

            string label = GetLabel(indexAverages);
            _components.LoggingAction(TextClassifications.MessageCollection.ResultOfClassificationTaskIs(label));

            if (label == null)
                _components.LoggingAction(TextClassifications.MessageCollection.ClassificationTaskHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples);
            else
                _components.LoggingAction(TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful);

            TextClassifierResult result = new TextClassifierResult(label, indexes, indexAverages);

            return result;

        }        
        private TextClassifierSession CreateSession(TextClassifierSettings settings, TextClassifierResult result, string version)
        {

            List<TextClassifierResult> results = new List<TextClassifierResult>();
            results.Add(result);

            return new TextClassifierSession(settings: settings, results: results, version: version);

        }
        private TextClassifierSession CreateSession(TextClassifierSettings settings, List<TextClassifierResult> results, string version)
            => new TextClassifierSession(settings: settings, results: results, version: version);

        private List<SimilarityIndex> GetSimilarityIndexes(List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            /*
                Returns something like: 
                    
                    Id Label Value
                    1  sv    0.01995
                    2  en    0.014888
                    3  en    0.002268
                    ...
             */

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>();
            for (int i = 0; i < tokenizedExamples.Count; i++)
            {

                _components.LoggingAction(TextClassifications.MessageCollection.ComparingProvidedSnippetAgainstFollowingTokenizedExample(tokenizedExamples[i]));

                double indexValue = _components.SimilarityIndexCalculator.Do(nGrams, tokenizedExamples[i].NGrams, _components.RoundingFunction);
                double roundedValue = _components.RoundingFunction(indexValue);

                _components.LoggingAction(TextClassifications.MessageCollection.CalculatedSimilarityIndexValueIs(indexValue));
                _components.LoggingAction(TextClassifications.MessageCollection.RoundedSimilarityIndexValueIs(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(tokenizedExamples[i].LabeledExample.Text, tokenizedExamples[i].LabeledExample.Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _components.LoggingAction(TextClassifications.MessageCollection.FollowingSimilarityIndexObjectHasBeenAddedToTheList(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = GetUniqueLabels(indexes);
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingUniqueLabelsHaveBeenFound(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _components.LoggingAction(TextClassifications.MessageCollection.CalculatingIndexAverageForTheFollowingLabel(currentLabel));

                List<double> indexValues = GetSimilarityIndexValues(currentLabel, indexes);
                double averageValue = CalculateAverage(indexValues);
                double roundedValue = _components.RoundingFunction(averageValue);

                _components.LoggingAction(TextClassifications.MessageCollection.CalculatedSimilarityIndexAverageValueIs(averageValue));
                _components.LoggingAction(TextClassifications.MessageCollection.RoundedSimilarityIndexAverageValueIs(roundedValue));

                SimilarityIndexAverage indexAverage = new SimilarityIndexAverage(currentLabel, roundedValue);
                similarityAverages.Add(indexAverage);

                _components.LoggingAction(TextClassifications.MessageCollection.FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList(indexAverage));

            }

            return similarityAverages;

        }
        private List<string> GetUniqueLabels(List<SimilarityIndex> indexes)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * en       0.11
             * en       0.98
             * 
             *      => { "sv", "en" }
             * 
             */

            List<string> labels = indexes.Select(similarityIndex => similarityIndex.Label).ToList();
            List<string> uniqueLabels = new HashSet<string>(labels).ToList();

            return uniqueLabels;

        }
        private List<double> GetSimilarityIndexValues(string label, List<SimilarityIndex> indexes)
        {

            /*
             * 
             * Label    SimilarityIndex
             * sv       0.19
             * en       0.45
             * en       0.12
             * 
             *      => en: { 0.45, 0.12 }
             * 
             */

            return indexes
                    .Where(similarityIndex => similarityIndex.Label == label)
                    .Select(similarityIndex => similarityIndex.Value)
                    .ToList();

        }
        private double CalculateAverage(List<double> averages)
        {

            /* { 0.19, 0.45 } => 0.32 */

            double sum = 0.0;
            foreach (double dbl in averages)
                sum += dbl;

            return sum / averages.Count;

        }

        private string GetLabel(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * dk       0.27 
             * 
             *      => { Label: "en", Average: 0.45 } => "en" (main use case scenario)
             * 
             */

            if (AreAllIndexAveragesEqualToZero(indexAverages))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreAllIndexAveragesEqualToZero)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreAllIndexAveragesEqualToZero)));

            if (IsSingleLabelAndHigherEqualThanMinimumAccuracy(indexAverages, _settings))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsSingleLabelAndHigherEqualThanMinimumAccuracy)));           
                return LogAndReturnLabel(indexAverages);
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsSingleLabelAndHigherEqualThanMinimumAccuracy)));

            if (IsSingleLabelAndLessThanMinimumAccuracy(indexAverages, _settings))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsSingleLabelAndLessThanMinimumAccuracy)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsSingleLabelAndLessThanMinimumAccuracy)));

            if (AreAllIndexAveragesSameValue(indexAverages))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreAllIndexAveragesSameValue)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreAllIndexAveragesSameValue)));

            indexAverages = OrderByHighest(indexAverages);
            
            if (AreTwoHighestIndexAveragesSameValue(indexAverages))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(AreTwoHighestIndexAveragesSameValue)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(AreTwoHighestIndexAveragesSameValue)));

            if (IsLessThanMinimumAccuracyMultipleLabels(indexAverages, _settings))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedTrue(nameof(IsLessThanMinimumAccuracyMultipleLabels)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationReturnedFalse(nameof(IsLessThanMinimumAccuracyMultipleLabels)));

            return LogAndReturnLabel(indexAverages);

        }
        private bool AreAllIndexAveragesEqualToZero(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0
             * en       0
             * 
             * 		=> { 0, 0 } 
             * 		=> true
             * 
             */

            if (indexAverages.Where(Item => Item.Value == 0).Count() == indexAverages.Count) // All equal to zero
                return true;

            return false;

        }
        private bool IsSingleLabelAndHigherEqualThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, TextClassifierSettings settings)
            => (indexAverages.Count == 1 && indexAverages[0].Value >= settings.MinimumAccuracySingleLabel);
        private bool IsSingleLabelAndLessThanMinimumAccuracy(List<SimilarityIndexAverage> indexAverages, TextClassifierSettings settings)
            => (indexAverages.Count == 1 && indexAverages[0].Value < settings.MinimumAccuracySingleLabel);
        private bool AreAllIndexAveragesSameValue(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0.1
             * en       0.1
             * dk       0.1
             * 
             * 		=> { 0.1, 0.1, 0.1 } 
             * 		=> 1 
             * 		=> 1 == 1
             * 		=> true
             * 
             */

            if (indexAverages.Select(Item => Item.Value).Distinct().Count() == 1) // All with the same value
                return true;

            return false;

        }
        private bool AreTwoHighestIndexAveragesSameValue(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0.98        < 1st
             * en       0.98        < 2nd
             * dk       0.32        < 3rd
             * 
             * 		=> 0.98 == 0.98
             * 		=> true
             * 
             */

            return (indexAverages[0].Value == indexAverages[1].Value);

        }
        private bool IsLessThanMinimumAccuracyMultipleLabels(List<SimilarityIndexAverage> indexAverages, TextClassifierSettings settings)
            => (indexAverages[0].Value < settings.MinimumAccuracyMultipleLabels);
        private string LogAndReturnLabel(List<SimilarityIndexAverage> indexAverages)
        {

            _components.LoggingAction(TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(indexAverages[0]));

            return indexAverages[0].Label;

        }
        private List<SimilarityIndexAverage> OrderByHighest(List<SimilarityIndexAverage> indexAverages)
            => indexAverages.OrderByDescending(Item => Item.Value).ToList();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/