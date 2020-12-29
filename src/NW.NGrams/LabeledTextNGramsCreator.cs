using System;
using System.Collections.Generic;
using RUBN.Shared;
using System.Linq;

namespace NW.NGrams
{
    public class LabeledTextNGramsCreator : ILabeledTextNGramsCreator
    {

        // Fields
        // Properties
        public INGramsTextClassifier NGramsTextClassifier { get; set; } = new NGramsTextClassifier();
        public string LabeledTextJson { get; }
        public ITokenizationStrategies TokenizationStrategies { get; } = new DefaultTokenizationStrategies();

        // Constructors
        public LabeledTextNGramsCreator(string strLabeledTextJson)
        {

            LabeledTextJson = strLabeledTextJson;

        }

        // Methods
        public Outcome Do(string[] arrLabels = null)
        {

            string msgSuccess = "A List<LabeledTextNGrams> has been created from the provided file path.";
            string errFailure = "It hasn't been possible to create a List<LabeledTextNGrams> from the provided file path.";

            try
            {

                Outcome objReturn = NGramsTextClassifier.GetLabeledTexts(LabeledTextJson);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();
                List<LabeledTextJson> listLabeledTexts = (List<LabeledTextJson>)objReturn.Result;

                if (arrLabels != null)
                    if (arrLabels.Length > 0)
                        listLabeledTexts = listLabeledTexts.Where(obj => arrLabels.Contains(obj.Label)).ToList();

                objReturn = NGramsTextClassifier.ConvertToNGrams(listLabeledTexts, TokenizationStrategies.Get());
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();
                List<LabeledTextNGrams> listLabeledTextsNGrams = objReturn.Result as List<LabeledTextNGrams>;

                return OutcomeBuilder.CreateSuccess(msgSuccess, listLabeledTextsNGrams).Get();

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
 *  Last Update: 24.08.2018
 * 
 */
