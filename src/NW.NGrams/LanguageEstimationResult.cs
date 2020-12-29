using System.Collections.Generic;

namespace NW.NGrams
{
    public class LanguageEstimationResult
    {

        // Fields
        // Properties
        public string Language { get; }
        public List<LabeledTextSimilarityIndex> SimilarityIndexes { get; }
        public List<LabeledTextSimilarityAverage> SimilarityAverages { get; }

        // Constructors	
        public LanguageEstimationResult(
            string strLanguage,
            List<LabeledTextSimilarityIndex> listSimilarityIndexes,
            List<LabeledTextSimilarityAverage> listSimilarityAverages)
        {

            Language = strLanguage;
            SimilarityIndexes = listSimilarityIndexes;
            SimilarityAverages = listSimilarityAverages;

        }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 23.08.2018

*/
