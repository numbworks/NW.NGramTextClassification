using System.Collections.Generic;

namespace NW.NGrams
{
    public class TextClassifierResult
    {

        // Fields
        // Properties
        public string Label { get; }
        public List<SimilarityIndex> SimilarityIndexes { get; }
        public List<SimilarityIndexAverage> SimilarityIndexAverages { get; }

        // Constructors	
        public TextClassifierResult(
            string label,
            List<SimilarityIndex> indexes,
            List<SimilarityIndexAverage> indexAverages)
        {

            // Label can also be null when estimation didn't return any value
            Validator.ValidateList(indexes, nameof(indexes));
            Validator.ValidateList(indexAverages, nameof(indexAverages));

            Label = label;
            SimilarityIndexes = indexes;
            SimilarityIndexAverages = indexAverages;

        }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/