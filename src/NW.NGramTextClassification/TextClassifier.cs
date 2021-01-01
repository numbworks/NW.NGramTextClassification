using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification
{
    public class TextClassifier : ITextClassifier
    {

        // Fields
        // Properties
        private Func<double, double> _roundingStrategy;
        private INGramTokenizer _nGramsTokenizer;
        private ISimilarityIndexCalculator _similarityIndexCalculator;

        // Constructors
        public TextClassifier
            (Func<double, double> roundingStrategy,
            INGramTokenizer nGramsTokenizer,
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
                  new NGramTokenizer(),
                  new SimilarityIndexCalculatorJaccard()) { }

        // Methods
        public TextClassifierResult Predict
            (string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts)
        {

            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateObject(ruleSet, nameof(ruleSet));
            Validator.ValidateList(labeledExtracts, nameof(labeledExtracts));

            List<INGram> nGrams = _nGramsTokenizer.Do(text, strategy, ruleSet);
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
        public TextClassifierResult Predict
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
        private List<SimilarityIndexAverage> GetSimilarityIndexAverages(List<SimilarityIndex> indexes)
        {

            Validator.ValidateList(indexes, nameof(indexes));

            List<string> uniqueLabels = ExtractUniqueLabels(indexes);

            List<SimilarityIndexAverage> similarityAverages = new List<SimilarityIndexAverage>();
            for (int i = 0; i < uniqueLabels.Count; i++)
            {

                string currentLabel = uniqueLabels[i];
                List<double> currentIndexes = ExtractSimilarityIndexes(currentLabel, indexes);
                double currentAverage = CalculateAverage(currentIndexes);

                similarityAverages.Add(
                    new SimilarityIndexAverage(
                        currentLabel,
                        _roundingStrategy(currentAverage)));

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

            Validator.ValidateList(indexAverages, nameof(indexAverages));
            Validator.ValidateSimilarityIndexAverages(indexAverages);

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
    Last Update: 31.12.2020

*/