using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ILanguageEstimator
    {

        /// <summary>
        /// It returns a LanguageEstimationResult object for the provided text.
        /// </summary>
        LanguageEstimationResult Do(string text, List<LabeledTextNGrams> labeledTextsNGrams);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
