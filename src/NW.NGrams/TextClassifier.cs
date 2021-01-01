using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGrams
{
    public class TextClassifier
    {

        // Fields
        // Properties
        private Func<double, double> _roundingStrategy;
        private INGramsTokenizer _nGramsTokenizer;
        private ISimilarityIndexCalculator _similarityIndexCalculator;

        // Constructors
        public TextClassifier
            (Func<double, double> roundingStrategy,
            INGramsTokenizer nGramsTokenizer,
            ISimilarityIndexCalculator similarityIndexCalculator)
        {

            Validator.ValidateObject(roundingStrategy, nameof(roundingStrategy));
            Validator.ValidateObject(nGramsTokenizer, nameof(nGramsTokenizer));
            Validator.ValidateObject(similarityIndexCalculator, nameof(similarityIndexCalculator));

            _roundingStrategy = roundingStrategy;
            _nGramsTokenizer = nGramsTokenizer;
            _similarityIndexCalculator = similarityIndexCalculator;

        }
        public TextClassifier()
            : this(
                  RoundingStategies.SixDecimalDigits,
                  new NGramsTokenizer(),
                  new SimilarityIndexCalculatorJaccard()) { }

        // Methods
        public TextClassifierResult DoFor<T>(ITokenizationStrategy strategy, string text, List<LabeledExtract> labeledExtracts) where T : INGram
        {

            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateList(labeledExtracts, nameof(labeledExtracts));

            List<T> nGrams = _nGramsTokenizer.DoFor<T>(strategy, text);


            List<SimilarityIndex> similarityIndexes = GetSimilarityIndexes(nGrams, labeledExtracts);
            List<SimilarityIndexAverage> similarityAverages = GetSimilarityAverages(similarityIndexes);

            string label = EstimateLabel(similarityAverages);
            TextClassifierResult estimationResult 
                = new TextClassifierResult(label, similarityIndexes, similarityAverages);

            // if something fail, null should replace label

            return estimationResult;

        }

        // Methods (private)
        private List<SimilarityIndex> GetSimilarityIndexes(List<string> nGrams, List<LabeledExtract> labeledExtracts)
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

            Validator.ValidateList(nGrams, nameof(nGrams));
            Validator.ValidateList(labeledExtracts, nameof(labeledExtracts));

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>();
            for (int i = 0; i < labeledExtracts.Count; i++)
            {

                double similarityIndex = _similarityIndexCalculator.Do(nGrams, labeledExtracts[i].TextAsNGrams);
                similarityIndexes.Add(
                    new SimilarityIndex(
                        labeledExtracts[i].Id,
                        labeledExtracts[i].Label,
                        _roundingStrategy(similarityIndex)
                    ));

            }

            return similarityIndexes;

        }
        private List<SimilarityIndexAverage> GetSimilarityAverages(List<SimilarityIndex> similarityIndexes)
        {

            Validator.ValidateList(similarityIndexes, nameof(similarityIndexes));

            List<string> uniqueLabels = ExtractUniqueLabels(similarityIndexes);

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                List<double> currentIndexes = ExtractSimilarityIndexes(currentLabel, similarityIndexes);
                double currentAverage = CalculateAverage(currentIndexes);

                similarityAverages.Add(
                    new SimilarityIndexAverage(
                        currentLabel,
                        _roundingStrategy(currentAverage)));

            }

            return similarityAverages;

        }
        private string EstimateLabel(List<SimilarityIndexAverage> similarityAverages)
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

            Validator.ValidateList(similarityAverages, nameof(similarityAverages));
            Validate(similarityAverages);

            return GetHighest(similarityAverages).Label;

        }
        private List<string> ExtractUniqueLabels(List<SimilarityIndex> similarityIndexes)
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

            List<string> labels = similarityIndexes.Select(similarityIndex => similarityIndex.Label).ToList();
            List<string> uniqueLabels = new HashSet<string>(labels).ToList();

            return uniqueLabels;

        }
        private List<double> ExtractSimilarityIndexes(string label, List<SimilarityIndex> similarityIndexes)
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

            return similarityIndexes
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
        private bool AreAllZero(List<SimilarityIndexAverage> list)
        {

            /*
             *
             * Label    Average
             * sv       0
             * en       0
             * 
             * 		=> { 0, 0 } => true
             * 
             */

            if (list.Where(Item => Item.Value == 0).Count() == list.Count)
                return true;

            return false;

        }
        private bool AreDistinct(List<SimilarityIndexAverage> list)
        {

            /*
             *
             * Label    Average
             * sv       0.1
             * en       0.1
             * dk       0.1
             * 
             * 		=> { 0.1, 0.1, 0.1 } => 1 => 1 != 3 => false
             * 
             */

            if (list.Select(Item => Item.Value).Distinct().Count() == list.Count)
                return true;

            return false;

        }
        private SimilarityIndexAverage GetHighest(List<SimilarityIndexAverage> list)
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

            return list.OrderByDescending(Item => Item.Value).ToList().First();

        }
        private void Validate(List<SimilarityIndexAverage> similarityAverages)
        {

            if (AreAllZero(similarityAverages))
                throw new Exception(MessageCollection.TheMethodDidntReturnExpectedOutcome.Invoke(nameof(AreAllZero), true));

            if (!AreDistinct(similarityAverages))
                throw new Exception(MessageCollection.TheMethodDidntReturnExpectedOutcome.Invoke(nameof(AreDistinct), false));

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/