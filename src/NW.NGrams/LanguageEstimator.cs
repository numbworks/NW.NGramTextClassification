using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public class LanguageEstimator : ILanguageEstimator
    {

        // Fields
        // Properties
        public INGramsTextClassifier NGramsTextClassifier { get; set; } = new NGramsTextClassifier();
        public ITokenizationStrategyManager TokenizationStrategies { get; set; } = new TokenizationStrategyManager();

        // Constructors
        public LanguageEstimator() { }

        // Methods
        public Outcome Do(ITextDecisionStrategy objTextDecisionStrategy, List<LabeledTextNGrams> listLabeledTextsNGrams)
        {

            /*
             * Strategy:
             * 
             * 1) If EstimateLabel() == Success
             *    We want to return Success and the label (string) returned as objReturn.Result.
             *    We can omit EstimateLabel()'s messages.	  
             *    
             * 2) If EstimateLabel() == Failure
             *    We want to return Success and a null label.
             *    We don't want to omit EstimateLabel()'s messages, in order to log why language hasn't been estimated.
             * 
             */

            string msgSuccessLabel = "The language for the provided text has been successfully estimated.";
            string msgSuccessLists = "It hasn't been possible to estimate the language for the provided text, but similarity indexes and averages are provided.";
            string errFailure = "It hasn't been possible to estimate the language nor to provide the similarity indexes and averages for the provided text.";

            try
            {

                string strText = objTextDecisionStrategy.GetText();

                Outcome objReturn = NGramsTextClassifier.ConvertToNGrams(strText, TokenizationStrategies.Get());
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();
                List<string> listNGrams = objReturn.Result as List<string>;

                objReturn = NGramsTextClassifier.GetSimilarityIndexes(listNGrams, listLabeledTextsNGrams);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();
                List<LabeledTextSimilarityIndex> listSimilarityIndexes 
                    = objReturn.Result as List<LabeledTextSimilarityIndex>;

                objReturn = NGramsTextClassifier.GetSimilarityAverages(listSimilarityIndexes);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();
                List<LabeledTextSimilarityAverage> listSimilarityAverages
                    = objReturn.Result as List<LabeledTextSimilarityAverage>;

                objReturn = NGramsTextClassifier.EstimateLabel(listSimilarityAverages);
                if (objReturn.IsSuccess())
                    return OutcomeBuilder.CreateSuccess(msgSuccessLabel,
                        new LanguageEstimationResult((string)objReturn.Result, listSimilarityIndexes, listSimilarityAverages)).Get();
                else
                    return OutcomeBuilder.CreateSuccess(msgSuccessLists,
                        new LanguageEstimationResult(null, listSimilarityIndexes, listSimilarityAverages)).Append(objReturn.Messages).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 23.08.2018
 * 
 */
