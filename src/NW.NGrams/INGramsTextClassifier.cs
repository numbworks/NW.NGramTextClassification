using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public interface INGramsTextClassifier
    {
        ILabeledTextJsonDeserializer LabeledTextJsonDeserializer { get; set; }
        INGramsSimilarityCalculator NGramsSimilarityCalculator { get; set; }
        INGramsTokenizer NGramsTokenizer { get; set; }
        IParametersValidator ParametersValidator { get; set; }

        /// <summary>
        /// It expects the content of a labeled texts Json file, and it returns a List<LabeledTextJson>.
        /// </summary>
        Outcome GetLabeledTexts(string strLabeledTextJson);

        /// <summary>
        /// It converts the provided List<LabeledTextJson> to List<LabeledTextNGrams>, in which each text is converted into a list of NGrams.
        /// </summary>
        Outcome ConvertToNGrams(List<LabeledTextJson> listLabeledTexts, ITokenizationStrategy objTokenizationStrategy);

        /// <summary>
        /// It converts the provided text to a list of NGrams.
        /// </summary>
        Outcome ConvertToNGrams(string strText, ITokenizationStrategy objTokenizationStrategy);

        /// <summary>
        /// It converts the provided List<LabeledTextJson> to List<LabeledTextNGrams>, in which each text is converted into a list of NGrams.
        /// </summary>
        Outcome ConvertToNGrams(List<LabeledTextJson> listLabeledTexts, List<ITokenizationStrategy> listTokenizationStrategies);

        /// <summary>
        /// It converts the provided text to a list of NGrams.
        /// </summary>
        Outcome ConvertToNGrams(string strText, List<ITokenizationStrategy> listTokenizationStrategies);

        /// <summary>
        /// It returns a List<LabeledTextSimilarityIndex> containining the similarities between the provided NGrammed text and each of the labeled texts.
        /// </summary>
        Outcome GetSimilarityIndexes(List<string> listTextNGrams, List<LabeledTextNGrams> listLabeledTextsNGrams);

        /// <summary>
        /// It returns a List<LabeledTextSimilarityAverage> containining the average similarity index for each unique label.
        /// </summary>
        Outcome GetSimilarityAverages(List<LabeledTextSimilarityIndex> listSimilarityIndexes);

        /// <summary>
        /// It returns the label with the highest average similarity index.
        /// </summary>
        Outcome EstimateLabel(List<LabeledTextSimilarityAverage> listSimilarityAverages);

        /// <summary>
        /// It returns the list in tabular format.
        /// </summary>
        Outcome FormatAsTable(List<ILabeledTextSimilarityValue> listSimilarityValues);

        /// <summary>
        /// It returns the object in tabular format.
        /// </summary>
        Outcome FormatAsTable(ILabeledTextSimilarityValue objSimilarityValue);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 23.02.2018 
 * 
 */
