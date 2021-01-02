using System.Collections.Generic;

namespace NW.NGramTextClassification
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
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Label)}: '{Label ?? "null"}'",
                    $"{nameof(SimilarityIndexes)}: '{SimilarityIndexes.Count.ToString()}'", // can't be null due of ValidateList()
                    $"{nameof(SimilarityIndexAverages)}: '{SimilarityIndexAverages.Count.ToString()}'" // can't be null due of ValidateList()
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/