using System;
using System.Collections.Generic;
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
            (string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));
            Validator.ValidateList(labeledExtracts, nameof(labeledExtracts));

            List<INGram> nGrams = _components.NGramsTokenizer.Do(text, strategy, ruleSet);
            List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, labeledExtracts);
            List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);

            string label = EstimateLabel(indexAverages);
            TextClassifierResult result 
                = new TextClassifierResult(label, indexes, indexAverages);

            // if something fail, null should replace label

            return result;

        }
        public TextClassifierResult Predict
            (string text, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts)
                => Predict(text, new TokenizationStrategy(), ruleSet, labeledExtracts);
        public TextClassifierResult PredictLabel
            (string text, List<LabeledExtract> labeledExtracts)
                => Predict(text, new NGramTokenizerRuleSet(), labeledExtracts);

        // Methods (private)
        private List<SimilarityIndex> GetSimilarityIndexes
            (List<INGram> nGrams, List<LabeledExtract> labeledExtracts)
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
            for (int i = 0; i < labeledExtracts.Count; i++)
            {

                _components.LoggingAction.Invoke(MessageCollection.ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(labeledExtracts[i]));

                double indexValue = _components.SimilarityIndexCalculator.Do(nGrams, labeledExtracts[i].TextAsNGrams, _components.RoundingFunction);
                double roundedValue = _components.RoundingFunction.Invoke(indexValue);

                _components.LoggingAction.Invoke(MessageCollection.TheCalculatedSimilarityIndexValueIs.Invoke(indexValue));
                _components.LoggingAction.Invoke(MessageCollection.TheRoundedSimilarityIndexValueIs.Invoke(roundedValue));

                SimilarityIndex similarityIndex = new SimilarityIndex(labeledExtracts[i].Id, labeledExtracts[i].Label, roundedValue);
                similarityIndexes.Add(similarityIndex);

                _components.LoggingAction.Invoke(MessageCollection.TheFollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(similarityIndex));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            List<string> uniqueLabels = ExtractUniqueLabels(indexes);
            _components.LoggingAction.Invoke(MessageCollection.TheFollowingUniqueLabelsHaveBeenFound.Invoke(uniqueLabels));

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                _components.LoggingAction.Invoke(MessageCollection.CalculatingIndexAverageForTheFollowingLabel.Invoke(currentLabel));

                List<double> indexValues = ExtractSimilarityIndexes(currentLabel, indexes);
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
             * 
             *      => { Label: "en", Average: 0.45 } => "en"
             * 
             */

            if (HasOnlyZeros(indexAverages))
            {
                _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasFailed.Invoke(nameof(HasOnlyZeros)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasBeenSuccessful.Invoke(nameof(HasOnlyZeros)));

            if (HasEveryLabelSameAverage(indexAverages))
            {
                _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasFailed.Invoke(nameof(HasEveryLabelSameAverage)));
                return null;
            }
            _components.LoggingAction.Invoke(MessageCollection.FollowingVerificationHasBeenSuccessful.Invoke(nameof(HasEveryLabelSameAverage)));

            // What if the two highest values are the same?

            return GetHighest(indexAverages).Label;

        }
        private List<string> ExtractUniqueLabels(List<SimilarityIndex> indexes)
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
        private List<double> ExtractSimilarityIndexes(string label, List<SimilarityIndex> indexes)
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
        private static bool HasOnlyZeros(List<SimilarityIndexAverage> indexAverages)
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

            if (indexAverages.Where(Item => Item.Value == 0).Count() == indexAverages.Count)
                return true;

            return false;

        }
        private static bool HasEveryLabelSameAverage(List<SimilarityIndexAverage> indexAverages)
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

            if (indexAverages.Select(Item => Item.Value).Distinct().Count() == 1)
                return true;

            return false;

        }
        private SimilarityIndexAverage GetHighest(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * 
             *      => { Label: "en", Average: 0.45 }
             * 
             */

            return indexAverages.OrderByDescending(Item => Item.Value).ToList().First();

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2021

*/