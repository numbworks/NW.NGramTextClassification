using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public interface ILanguageEstimator
    {

        INGramsTextClassifier NGramsTextClassifier { get; set; }
        ITokenizationStrategyManager TokenizationStrategies { get; set; }

        /// <summary>
        /// It returns a LanguageEstimationResult object for the provided text.
        /// It returns "Success" in both case (language is estimated = label, language is not estimated = null label). 
        /// It returns "Failure" just when an exception arises.
        /// </summary>
        Outcome Do(ITextDecisionStrategy objTextDecisionStrategy, List<LabeledTextNGrams> listLabeledTextsNGrams);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 23.08.2018 
 * 
 */
