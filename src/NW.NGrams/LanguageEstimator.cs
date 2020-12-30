using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class LanguageEstimator : ILanguageEstimator
    {

        // Fields
        // Properties
        private INGramsTextClassifier _NGramsTextClassifier;
        private ITokenizationStrategyManager _TokenizationStrategies;

        // Constructors
        public LanguageEstimator(
            INGramsTextClassifier nGramsTextClassifier,
            ITokenizationStrategyManager tokenizationStrategies)
        {

            if (nGramsTextClassifier == null)
                throw new ArgumentNullException(nameof(nGramsTextClassifier));
            if (tokenizationStrategies == null)
                throw new ArgumentNullException(nameof(tokenizationStrategies));

            _NGramsTextClassifier = nGramsTextClassifier;
            _TokenizationStrategies = tokenizationStrategies;

        }
        public LanguageEstimator()
            : this(
                new NGramsTextClassifier(),
                new TokenizationStrategyManager()) { }

        // Methods
        public LanguageEstimationResult Do(string text, List<LabeledTextNGrams> labeledTextsNGrams)
        {

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (labeledTextsNGrams == null)
                throw new ArgumentNullException(nameof(labeledTextsNGrams));
            if (labeledTextsNGrams.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(labeledTextsNGrams)));

            List<string> nGrams = _NGramsTextClassifier.ConvertToNGrams(text, _TokenizationStrategies.Get());
            List<LabeledTextSimilarityIndex> similarityIndexes
                = _NGramsTextClassifier.GetSimilarityIndexes(nGrams, labeledTextsNGrams);
            List<LabeledTextSimilarityAverage> similarityAverages
                = _NGramsTextClassifier.GetSimilarityAverages(similarityIndexes);

            string label = _NGramsTextClassifier.EstimateLabel(similarityAverages);
            LanguageEstimationResult estimationResult 
                = new LanguageEstimationResult(label, similarityIndexes, similarityAverages);

            // if something fail, null should replace label

            return estimationResult;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/