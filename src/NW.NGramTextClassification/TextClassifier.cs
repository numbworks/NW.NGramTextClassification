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
            : this(new TextClassifierComponents(), new TextClassifierSettings()) { }

        #endregion

        #region Methods_public

        public TextClassifierResult PredictLabel(string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(tokenizerRuleSet, nameof(tokenizerRuleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_AttemptingToPredictLabel);
            string truncatedText = _components.TextTruncatingFunction.Invoke(text, _settings.TruncateTextInLogMessagesAfter);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText));
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(tokenizerRuleSet));
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(labeledExamples));

            List<INGram> nGrams = _components.NGramsTokenizer.DoForRuleset(text, tokenizerRuleSet);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(nGrams));

            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, labeledExamples);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_TokenizedTextHasBeenComparedAgainstTheProvidedLabeledExamples);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XSimilarityIndexObjectsHaveBeenComputed.Invoke(indexes));

            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_XSimilarityIndexAverageObjectsHaveBeenComputed(indexAverages));

            string label = PredictLabel(indexAverages);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictedLabelIs.Invoke(label));

            if (label == null)
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples);
            else
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasBeenSuccessful);

            TextClassifierResult result = new TextClassifierResult(label, indexes, indexAverages);

            return result;

        }
        public TextClassifierResult PredictLabel(string text, List<LabeledExample> labeledExamples)
                => PredictLabel(text, new NGramTokenizerRuleSet(), labeledExamples);

        #endregion

        #region Methods_private

        private List<SimilarityIndex> GetSimilarityIndexes(List<INGram> nGrams, List<LabeledExample> labeledExamples)
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
            for (int i = 0; i < labeledExamples.Count; i++)
            {

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(labeledExamples[i]));

                double indexValue = _components.SimilarityIndexCalculator.Do(nGrams, labeledExamples[i].TextAsNGrams, _components.RoundingFunction);
                double roundedValue = _components.RoundingFunction.Invoke(indexValue);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(indexValue));
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(labeledExamples[i].Id, labeledExamples[i].Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = ExampleUniqueLabels(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingUniqueLabelsHaveBeenFound.Invoke(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(currentLabel));

                List<double> indexValues = ExampleSimilarityIndexes(currentLabel, indexes);
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
        private string PredictLabel(List<SimilarityIndexAverage> indexAverages)
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

            if (!ContainsAtLeastOneIndexAverageThatIsNotZero(indexAverages))
            {
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasFailed.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsNotZero)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsNotZero)));

            if (!ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers(indexAverages))
            {
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasFailed.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers)));

            if (indexAverages.Count == 1)
            {
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs.Invoke(indexAverages[0]));
                return indexAverages[0].Label;
            }

            List<SimilarityIndexAverage> orderedByhighest = OrderByHighest(indexAverages);
            if (!ContainsTwoHighestIndexAveragesThatArentEqual(orderedByhighest[0].Value, orderedByhighest[1].Value))
            {
                _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasFailed.Invoke(nameof(ContainsTwoHighestIndexAveragesThatArentEqual)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsTwoHighestIndexAveragesThatArentEqual)));

            _components.LoggingAction.Invoke(MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs.Invoke(orderedByhighest[0]));

            return orderedByhighest[0].Label;

        }
        private List<string> ExampleUniqueLabels(List<SimilarityIndex> indexes)
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
        private List<double> ExampleSimilarityIndexes(string label, List<SimilarityIndex> indexes)
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
        private static bool ContainsAtLeastOneIndexAverageThatIsNotZero(List<SimilarityIndexAverage> indexAverages)
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
        private static bool ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers(List<SimilarityIndexAverage> indexAverages)
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
        private static bool ContainsTwoHighestIndexAveragesThatArentEqual(double first, double second)
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
    Last Update: 19.09.2021
*/