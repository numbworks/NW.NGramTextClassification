using System.Collections.Generic;

namespace NW.NGrams
{
    public class LanguageEstimationResult
    {

        // Fields
        // Properties
        public string Label { get; }
        public List<LabeledTextSimilarityIndex> SimilarityIndexes { get; }
        public List<LabeledTextSimilarityAverage> SimilarityAverages { get; }

        // Constructors	
        public LanguageEstimationResult(
            string label,
            List<LabeledTextSimilarityIndex> similarityIndexes,
            List<LabeledTextSimilarityAverage> similarityAverages)
        {

            Label = label;
            SimilarityIndexes = similarityIndexes;
            SimilarityAverages = similarityAverages;

        }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/