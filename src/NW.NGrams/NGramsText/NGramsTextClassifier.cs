﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGrams
{
    public class NGramsTextClassifier : INGramsTextClassifier
    {

        // Fields
        private Func<double, double> _RoundingStrategy;
        private ILabeledTextJsonDeserializer _LabeledTextJsonDeserializer;
        private INGramsTokenizer _NGramsTokenizer;
        private INGramsSimilarityCalculator _NGramsSimilarityCalculator;
        private Func<List<SimilarityIndexAverage>, bool> _AreAllZerosStrategy;
        private Func<List<SimilarityIndexAverage>, bool> _AreDistinctStrategy;
        private Func<List<SimilarityIndexAverage>, SimilarityIndexAverage> _GetHighestStrategy;

        // Properties
        // Constructors
        public NGramsTextClassifier
            (Func<double, double> roundingStrategy,
            ILabeledTextJsonDeserializer labeledTextJsonDeserializer,
            INGramsTokenizer nGramsTokenizer,
            INGramsSimilarityCalculator nGramsSimilarityCalculator,
            Func<List<SimilarityIndexAverage>, bool> areAllZerosStrategy,
            Func<List<SimilarityIndexAverage>, bool> areDistinctStrategy,
            Func<List<SimilarityIndexAverage>, SimilarityIndexAverage> getHighestStrategy)
        {

            _RoundingStrategy = roundingStrategy;
            _LabeledTextJsonDeserializer = labeledTextJsonDeserializer;
            _NGramsTokenizer = nGramsTokenizer;
            _NGramsSimilarityCalculator = nGramsSimilarityCalculator;
            _AreAllZerosStrategy = areAllZerosStrategy;
            _AreDistinctStrategy = areDistinctStrategy;
            _GetHighestStrategy = getHighestStrategy;

        }
        public NGramsTextClassifier()
            : this(
                  RoundingStategies.SixDecimalDigits,
                  new LabeledTextJsonDeserializer(),
                  new NGramsTokenizer(),
                  new JaccardIndexCalculator(),
                  HighestAverageStrategies.AreAllZeros,
                  HighestAverageStrategies.AreDistinct,
                  HighestAverageStrategies.GetHighest) { }

        // Methods (public)
        public List<LabeledExtract> GetLabeledTexts(string labeledTextJson)
        {

            Validate(labeledTextJson, nameof(labeledTextJson));

            return _LabeledTextJsonDeserializer.Do(labeledTextJson);

        }
        public List<LabeledTextNGrams> ConvertToNGrams(List<LabeledExtract> labeledTexts, ITokenizationStrategy tokenizationStrategy)
        {

            Validate(labeledTexts, nameof(labeledTexts));
            Validate(tokenizationStrategy);

            List <LabeledTextNGrams> labeledTextsNGrams = new List<LabeledTextNGrams>();
            for (int i = 0; i < labeledTexts.Count; i++)
            {

                List<string> currentTokens = _NGramsTokenizer.Do(tokenizationStrategy, labeledTexts[i].Text);

                labeledTextsNGrams.Add(
                    new LabeledTextNGrams(
                        labeledTexts[i].Id,
                        labeledTexts[i].Label,
                        currentTokens));

            }

            return labeledTextsNGrams;

        }
        public List<string> ConvertToNGrams(string text, ITokenizationStrategy tokenizationStrategy)
        {

            Validate(text, nameof(text));
            Validate(tokenizationStrategy);

            return _NGramsTokenizer.Do(tokenizationStrategy, text);

        }
        public List<LabeledTextNGrams> ConvertToNGrams(List<LabeledExtract> labeledTexts, List<ITokenizationStrategy> tokenizationStrategies)
        {

            Validate(labeledTexts, nameof(labeledTexts));
            Validate(tokenizationStrategies);

            List<LabeledTextNGrams> labeledTextsNGrams = new List<LabeledTextNGrams>();
            foreach (ITokenizationStrategy tokenizationStrategy in tokenizationStrategies)
                labeledTextsNGrams.AddRange(
                        ConvertToNGrams(labeledTexts, tokenizationStrategy));

            return labeledTextsNGrams;

        }
        public List<string> ConvertToNGrams(string text, List<ITokenizationStrategy> tokenizationStrategies)
        {

            Validate(text, nameof(text));
            Validate(tokenizationStrategies);

            List<string> nGrams = new List<string>();
            foreach (ITokenizationStrategy tokenizationStrategy in tokenizationStrategies)
                nGrams.AddRange(
                    ConvertToNGrams(text, tokenizationStrategy));

            return nGrams;

        }
        public List<SimilarityIndex> GetSimilarityIndexes(List<string> nGrams, List<LabeledTextNGrams> labeledTextsNGrams)
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

            Validate(nGrams, nameof(nGrams));
            Validate(labeledTextsNGrams, nameof(labeledTextsNGrams));

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>();
            for (int i = 0; i < labeledTextsNGrams.Count; i++)
            {

                double similarityIndex = _NGramsSimilarityCalculator.Do(nGrams, labeledTextsNGrams[i].NGrams);
                similarityIndexes.Add(
                    new SimilarityIndex(
                        labeledTextsNGrams[i].LabeledTextId,
                        labeledTextsNGrams[i].Label,
                        _RoundingStrategy(similarityIndex)
                    ));

            }

            return similarityIndexes;

        }
        public List<SimilarityIndexAverage> GetSimilarityAverages(List<SimilarityIndex> similarityIndexes)
        {

            Validate(similarityIndexes, nameof(similarityIndexes));

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
                        _RoundingStrategy(currentAverage)));

            }

            return similarityAverages;

        }
        public string EstimateLabel(List<SimilarityIndexAverage> similarityAverages)
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

            Validate(similarityAverages, nameof(similarityAverages));
            Validate(similarityAverages, _AreAllZerosStrategy, _AreDistinctStrategy);

            return _GetHighestStrategy(similarityAverages).Label;

        }
        public string FormatAsTable(List<ISimilarityValue> similarityValues)
        {

            /*
             * 
             * LabeledTextId   Label    SimilarityIndex
             * 1                sv       0.21
             * 2                en       0.98
             * ..
             * 
             * or:
             * 
             * Label    Average
             * sv       0.19
             * en       0.45
             * 
             */

            Validate(similarityValues, nameof(similarityValues));

            string table = string.Empty;
            for (int i = 0; i < similarityValues.Count; i++)
            {
                if (i == 0)
                    table = string.Format("{0}{1}", similarityValues[i].ToHeader(), Environment.NewLine);

                if (i == (similarityValues.Count - 1))
                    table += similarityValues[i].ToString();
                else
                    table += string.Format("{0}{1}", similarityValues[i].ToString(), Environment.NewLine);

            }

            return table;

        }
        public string FormatAsTable(ISimilarityValue similarityValue)
        {

            if (similarityValue == null)
                throw new ArgumentNullException(nameof(similarityValue));

            string table = string.Format("{0}{1}", similarityValue.ToHeader(), Environment.NewLine);
            table += string.Format("{0}", similarityValue.ToString());

            return table;

        }

        // Methods (private)
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

        private void Validate(string str, string variableName)
        {

            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(variableName);

        }
        private void Validate(ITokenizationStrategy tokenizationStrategy)
        {

            if (tokenizationStrategy == null)
                throw new ArgumentNullException(nameof(tokenizationStrategy));

        }
        private void Validate<T>(List<T> list, string variableName)
        {

            if (list == null)
                throw new ArgumentNullException(variableName);
            if (list.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }
        private void Validate(List<ITokenizationStrategy> tokenizationStrategies)
        {

            if (tokenizationStrategies == null)
                throw new ArgumentNullException(nameof(tokenizationStrategies));
            if (tokenizationStrategies.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(tokenizationStrategies)));

            foreach (ITokenizationStrategy tokenizationStrategy in tokenizationStrategies)
                Validate(tokenizationStrategy);

        }
        private void Validate(
            List<SimilarityIndexAverage> similarityAverages,
            Func<List<SimilarityIndexAverage>, bool> areAllZerosStrategy,
            Func<List<SimilarityIndexAverage>, bool> areDistinctStrategy
            )
        {

            if (areAllZerosStrategy(similarityAverages))
                throw new Exception(MessageCollection.TheFunctionDidntReturnExpectedOutcome.Invoke(nameof(areAllZerosStrategy), true));

            if (!areDistinctStrategy(similarityAverages))
                throw new Exception(MessageCollection.TheFunctionDidntReturnExpectedOutcome.Invoke(nameof(areDistinctStrategy), false));

        }

    }

}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/