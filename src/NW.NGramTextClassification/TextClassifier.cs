using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
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

        public TextClassifierResult ClassifyOrDefault(string snippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateStringNullOrWhiteSpace(snippet, nameof(snippet));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            LogInitialMessages(snippet, tokenizerRuleSet, labeledExamples);

            TextClassifierResult textClassifierResult = ClassifySingleOrDefault(snippet, tokenizerRuleSet, labeledExamples);

            return textClassifierResult;

        }
        public TextClassifierResult ClassifyOrDefault(string snippet, List<LabeledExample> labeledExamples)
                => ClassifyOrDefault(snippet, DefaultNGramTokenizerRuleSet, labeledExamples);

        public List<TextClassifierResult> ClassifyMany(List<string> snippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples) 
        {

            Validator.ValidateList(snippets, nameof(snippets));
            ValidateSnippets(snippets);
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            _components.LoggingAction(TextClassifications.MessageCollection.ProvidedSnippetsAre(snippets.Count));

            List<TextClassifierResult> textClassifierResults = new List<TextClassifierResult>();
            for (int i = 0; i < snippets.Count; i++)
            {

                TextClassifierResult textClassifierResult = ClassifySingleOrDefault(snippets[i], tokenizerRuleSet, labeledExamples);
                textClassifierResults.Add(textClassifierResult);

            }
                
            return textClassifierResults;

        }
        public List<TextClassifierResult> ClassifyMany(List<string> snippets, List<LabeledExample> labeledExamples)
                => ClassifyMany(snippets, DefaultNGramTokenizerRuleSet, labeledExamples);

        public void LogAsciiBanner()
            => _components.LoggingActionAsciiBanner(AsciiBanner);

        #endregion

        #region Methods_private

        private void ValidateSnippets(List<string> snippets)
        {

            for (int i = 0; i < snippets.Count; i++)
                Validator.ValidateStringNullOrWhiteSpace(snippets[i], TextClassifications.MessageCollection.SnippetsIndex(i));

        }

        private void LogInitialMessages(string snippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            snippet = _components.TextTruncatingFunction(snippet, _settings.TruncateTextInLogMessagesAfter);

            _components.LoggingAction(TextClassifications.MessageCollection.AttemptingToClassifyProvidedSnippet);
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingSnippetHasBeenProvided(snippet));
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet));
            _components.LoggingAction(TextClassifications.MessageCollection.XLabeledExamplesHaveBeenProvided(labeledExamples));

        }
        private TextClassifierResult ClassifySingleOrDefault(string snippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            List<INGram> nGrams = _components.NGramsTokenizer.DoForRuleSetOrDefault(snippet, tokenizerRuleSet);
            _components.LoggingAction(TextClassifications.MessageCollection.ProvidedTextHasBeenTokenizedIntoXNGrams(nGrams));
            if (nGrams == null)
            {

                _components.LoggingAction(TextClassifications.MessageCollection.AllRulesInProvidedRulesetFailed(snippet));
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
             *      => { Label: "en", Average: 0.45 } => "en"
             * 
             */

            if (AreAllIndexAveragesEqualToZero(indexAverages))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasFailed(nameof(AreAllIndexAveragesEqualToZero)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasBeenSuccessful(nameof(AreAllIndexAveragesEqualToZero)));

            if (indexAverages.Count == 1)
            {
                _components.LoggingAction(TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(indexAverages[0]));
                return indexAverages[0].Label;
            }

            if (AreAllIndexAveragesSameValue(indexAverages))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasFailed(nameof(AreAllIndexAveragesSameValue)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasBeenSuccessful(nameof(AreAllIndexAveragesSameValue)));

            List<SimilarityIndexAverage> orderedByhighest = OrderByHighest(indexAverages);
            
            if (!AreTwoHighestIndexAveragesDifferent(orderedByhighest[0].Value, orderedByhighest[1].Value))
            {
                _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasFailed(nameof(AreTwoHighestIndexAveragesDifferent)));
                return null;
            }
            _components.LoggingAction(TextClassifications.MessageCollection.FollowingVerificationHasBeenSuccessful(nameof(AreTwoHighestIndexAveragesDifferent)));

            _components.LoggingAction(TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(orderedByhighest[0]));

            return orderedByhighest[0].Label;

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
        private bool AreTwoHighestIndexAveragesDifferent(double first, double second)
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

            return (first != second);

        }
        private List<SimilarityIndexAverage> OrderByHighest(List<SimilarityIndexAverage> indexAverages)
            => indexAverages.OrderByDescending(Item => Item.Value).ToList();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.09.2022
*/