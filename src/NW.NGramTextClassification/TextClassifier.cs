﻿using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification
{
    public class TextClassifier : ITextClassifier
    {

        // Fields
        private TextClassifierComponents _components;

        // Properties
        // Constructors
        public TextClassifier(TextClassifierComponents components)
        {

            Validator.ValidateObject(components, nameof(components));

            _components = components;

        }
        public TextClassifier()
            : this(new TextClassifierComponents()) { }

        // Methods
        public TextClassifierResult Predict
            (string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet, List<LabeledExample> labeledExamples)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));
            Validator.ValidateList(labeledExamples, nameof(labeledExamples));

            List<INGram> nGrams = _components.NGramsTokenizer.Do(text, strategy, ruleSet);
            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, labeledExamples);
            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);

            string label = EstimateLabel(indexAverages);
            TextClassifierResult result 
                = new TextClassifierResult(label, indexes, indexAverages);

            // if something fail, null should replace label

            return result;

        }
        public TextClassifierResult Predict
            (string text, INGramsTokenizerRuleSet ruleSet, List<LabeledExample> labeledExamples)
                => Predict(text, new TokenizationStrategy(), ruleSet, labeledExamples);
        public TextClassifierResult PredictLabel
            (string text, List<LabeledExample> labeledExamples)
                => Predict(text, new NGramTokenizerRuleSet(), labeledExamples);

        // Methods (private)
        private List<SimilarityIndex> GetSimilarityIndexes
            (List<INGram> nGrams, List<LabeledExample> labeledExamples)
        {

            /*
             * 
             * 1) It takes a text - for ex. "Vår kund erbjuder...";
             * 2) it takes a List<string> of NGrammed labeled texts - for ex.:
             *
             *      1, "sv", { "Är du genuint", "du genuint intresserad", "genuint intresserad av", ... }
             *      ...
             * 
             * 3) it returns something like: 
             * 
             *      LabeledTextId Label Similarity
             *      1               sv    0.89
             *      2               en    0.13
             *      3               en    0.45   
             *      ...
             * 
             */

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>();
            for (int i = 0; i < labeledExamples.Count; i++)
            {

                _components.LoggingAction.Invoke(MessageCollection.ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(labeledExamples[i]));

                double indexValue = _components.SimilarityIndexCalculator.Do(nGrams, labeledExamples[i].TextAsNGrams, _components.RoundingFunction);
                double roundedValue = _components.RoundingFunction.Invoke(indexValue);

                _components.LoggingAction.Invoke(MessageCollection.TheCalculatedSimilarityIndexValueIs.Invoke(indexValue));
                _components.LoggingAction.Invoke(MessageCollection.TheRoundedSimilarityIndexValueIs.Invoke(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(labeledExamples[i].Id, labeledExamples[i].Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _components.LoggingAction.Invoke(MessageCollection.TheFollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = ExampleUniqueLabels(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TheFollowingUniqueLabelsHaveBeenFound.Invoke(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _components.LoggingAction.Invoke(MessageCollection.CalculatingIndexAverageForTheFollowingLabel.Invoke(currentLabel));

                List<double> indexValues = ExampleSimilarityIndexes(currentLabel, indexes);
                double averageValue = CalculateAverage(indexValues);
                double roundedValue = _components.RoundingFunction.Invoke(averageValue);

                _components.LoggingAction.Invoke(MessageCollection.TheCalculatedSimilarityIndexAverageValueIs.Invoke(averageValue));
                _components.LoggingAction.Invoke(MessageCollection.TheRoundedSimilarityIndexAverageValueIs.Invoke(roundedValue));

                SimilarityIndexAverage indexAverage = new SimilarityIndexAverage(currentLabel, roundedValue);
                similarityAverages.Add(indexAverage);

                _components.LoggingAction.Invoke(MessageCollection.TheFollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(indexAverage));

            }

            return similarityAverages;

        }
        private string EstimateLabel(List<SimilarityIndexAverage> indexAverages)
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
                _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasFailed.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsNotZero)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsNotZero)));

            if (!ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers(indexAverages))
            {
                _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasFailed.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers)));

            if (indexAverages.Count == 1)
            {
                _components.LoggingAction.Invoke(MessageCollection.TheSimilarityIndexAverageWithTheHighestValueIs.Invoke(indexAverages[0]));
                return indexAverages[0].Label;
            }

            List<SimilarityIndexAverage> orderedByhighest = OrderByHighest(indexAverages);
            if (!ContainsTwoHighestIndexAveragesThatArentEqual(orderedByhighest[0].Value, orderedByhighest[1].Value))
            {
                _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasFailed.Invoke(nameof(ContainsTwoHighestIndexAveragesThatArentEqual)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasBeenSuccessful.Invoke(nameof(ContainsTwoHighestIndexAveragesThatArentEqual)));

            _components.LoggingAction.Invoke(MessageCollection.TheSimilarityIndexAverageWithTheHighestValueIs.Invoke(orderedByhighest[0]));

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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2021

*/