using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
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
        
            #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
        public TextClassifier(TextClassifierComponents components, TextClassifierSettings settings)
        {

            Validator.ValidateObject(components, nameof(components));
            Validator.ValidateObject(settings, nameof(settings));

            _components = components;
            _settings = settings;

        }

        /// <summary>Initializes a <see cref="TextClassifier"/> instance using default parameters.</summary>
        public TextClassifier()
            : this(DefaultTextClassifierComponents, DefaultTextClassifierSettings) { }

        #endregion

        #region Methods_public

        public TextClassifierResult PredictLabelOrDefault(string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            ValidateAndBeginPrediction(text, tokenizerRuleSet, labeledExamples);

            List<INGram> nGrams = _components.NGramsTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(nGrams));
            if (nGrams == null)
            {

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_AllRulesInProvidedRulesetFailed.Invoke(text));
                return DefaultTextClassifierResult;

            }

            List<TokenizedExample> tokenizedExamples = _components.LabeledExampleManager.CreateOrDefault(labeledExamples, tokenizerRuleSet);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_ProvidedLabeledExamplesThruTokenizationProcess);
            if (tokenizedExamples == null)
            {

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_AtLeastOneLabeledExampleFailedTokenized);
                return DefaultTextClassifierResult;

            }

            return CreateResult(nGrams, tokenizedExamples);

        }
        public TextClassifierResult PredictLabelOrDefault(string text, List<LabeledExample> labeledExamples)
                => PredictLabelOrDefault(text, DefaultNGramTokenizerRuleSet, labeledExamples);

        #endregion

        #region Methods_private

        private void ValidateAndBeginPrediction(string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_AttemptingToPredictLabel);

            string truncatedText = _components.TextTruncatingFunction.Invoke(text, _settings.TruncateTextInLogMessagesAfter);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText));

            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(tokenizerRuleSet));
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(labeledExamples));

        }
        private TextClassifierResult CreateResult(List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
        {

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_TokenizedTextComparedAgainstProvidedTokenizedExamples);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XSimilarityIndexObjectsHaveBeenComputed.Invoke(indexes));

            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XSimilarityIndexAverageObjectsHaveBeenComputed(indexAverages));

            string label = GetLabel(indexAverages);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictedLabelIs(label));

            if (label == null)
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples);
            else
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasBeenSuccessful);

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

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingTokenizedExample.Invoke(tokenizedExamples[i]));

                double indexValue = _components.SimilarityIndexCalculator.Do(nGrams, tokenizedExamples[i].NGrams, _components.RoundingFunction);
                double roundedValue = _components.RoundingFunction.Invoke(indexValue);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(indexValue));
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(tokenizedExamples[i].LabeledExample.Text, tokenizedExamples[i].LabeledExample.Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = GetUniqueLabels(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingUniqueLabelsHaveBeenFound.Invoke(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(currentLabel));

                List<double> indexValues = GetSimilarityIndexValues(currentLabel, indexes);
                double averageValue = CalculateAverage(indexValues);
                double roundedValue = _components.RoundingFunction.Invoke(averageValue);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_CalculatedSimilarityIndexAverageValueIs.Invoke(averageValue));
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_RoundedSimilarityIndexAverageValueIs.Invoke(roundedValue));

                SimilarityIndexAverage indexAverage = new SimilarityIndexAverage(currentLabel, roundedValue);
                similarityAverages.Add(indexAverage);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(indexAverage));

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

            if (!ContainsAtLeastOneNonZeroIndexAverage(indexAverages))
            {
                _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasFailed(nameof(ContainsAtLeastOneNonZeroIndexAverage)));
                return null;
            }
            _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful(nameof(ContainsAtLeastOneNonZeroIndexAverage)));

            if (indexAverages.Count == 1)
            {
                _components.LoggingAction(MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs(indexAverages[0]));
                return indexAverages[0].Label;
            }

            if (!ContainsAtLeastOneDifferentIndexAverage(indexAverages))
            {
                _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasFailed(nameof(ContainsAtLeastOneDifferentIndexAverage)));
                return null;
            }
            _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful(nameof(ContainsAtLeastOneDifferentIndexAverage)));

            List<SimilarityIndexAverage> orderedByhighest = OrderByHighest(indexAverages);
            if (!ContainsTwoDifferentHighestIndexAverages(orderedByhighest[0].Value, orderedByhighest[1].Value))
            {
                _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasFailed(nameof(ContainsTwoDifferentHighestIndexAverages)));
                return null;
            }
            _components.LoggingAction(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful(nameof(ContainsTwoDifferentHighestIndexAverages)));

            _components.LoggingAction(MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs(orderedByhighest[0]));

            return orderedByhighest[0].Label;

        }
        private bool ContainsAtLeastOneNonZeroIndexAverage(List<SimilarityIndexAverage> indexAverages)
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

            if (indexAverages.Where(Item => Item.Value == 0).Count() == indexAverages.Count) // all equal to zero
                return false;

            return true;

        }
        private bool ContainsAtLeastOneDifferentIndexAverage(List<SimilarityIndexAverage> indexAverages)
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
             * 		=> false
             * 
             */

            if (indexAverages.Select(Item => Item.Value).Distinct().Count() == 1) // all with the same value
                return false;

            return true;

        }
        private bool ContainsTwoDifferentHighestIndexAverages(double first, double second)
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
    Last Update: 24.09.2022
*/