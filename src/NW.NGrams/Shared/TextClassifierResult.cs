using System.Collections.Generic;

namespace NW.NGrams
{
    public class TextClassifierResult
    {

        // Fields
        // Properties
        public string Label { get; }
        public List<SimilarityIndex> SimilarityIndexes { get; }
        public List<SimilarityIndexAverage> SimilarityAverages { get; }

        // Constructors	
        public TextClassifierResult(
            string label,
            List<SimilarityIndex> similarityIndexes,
            List<SimilarityIndexAverage> similarityAverages)
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