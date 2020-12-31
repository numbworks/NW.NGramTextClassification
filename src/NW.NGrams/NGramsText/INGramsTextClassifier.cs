using System.Collections.Generic;

namespace NW.NGrams
{
    public interface INGramsTextClassifier
    {

        /// <summary>
        /// It expects the content of a labeled texts Json file, and it returns a List<LabeledTextJson>.
        /// </summary>
        List<LabeledExtract> GetLabeledTexts(string labeledTextJson);

        /// <summary>
        /// It converts the provided List<LabeledTextJson> to List<LabeledTextNGrams>, in which each text is converted into a list of NGrams.
        /// </summary>
        List<LabeledTextNGrams> ConvertToNGrams(List<LabeledExtract> labeledTexts, ITokenizationStrategy tokenizationStrategy);

        /// <summary>
        /// It converts the provided text to a list of NGrams.
        /// </summary>
        List<string> ConvertToNGrams(string text, ITokenizationStrategy tokenizationStrategy);

        /// <summary>
        /// It converts the provided List<LabeledTextJson> to List<LabeledTextNGrams>, in which each text is converted into a list of NGrams.
        /// </summary>
        List<LabeledTextNGrams> ConvertToNGrams(List<LabeledExtract> labeledTexts, List<ITokenizationStrategy> tokenizationStrategies);

        /// <summary>
        /// It converts the provided text to a list of NGrams.
        /// </summary>
        List<string> ConvertToNGrams(string text, List<ITokenizationStrategy> tokenizationStrategies);

        /// <summary>
        /// It returns a List<LabeledTextSimilarityIndex> containining the similarities between the provided NGrammed text and each of the labeled texts.
        /// </summary>
        List<LabeledTextSimilarityIndex> GetSimilarityIndexes(List<string> textNGrams, List<LabeledTextNGrams> labeledTextsNGrams);

        /// <summary>
        /// It returns a List<LabeledTextSimilarityAverage> containining the average similarity index for each unique label.
        /// </summary>
        List<LabeledTextSimilarityAverage> GetSimilarityAverages(List<LabeledTextSimilarityIndex> similarityIndexes);

        /// <summary>
        /// It returns the label with the highest average similarity index.
        /// </summary>
        string EstimateLabel(List<LabeledTextSimilarityAverage> similarityAverages);

        /// <summary>
        /// It returns the list in tabular format.
        /// </summary>
        string FormatAsTable(List<ILabeledTextSimilarityValue> similarityValues);

        /// <summary>
        /// It returns the object in tabular format.
        /// </summary>
        string FormatAsTable(ILabeledTextSimilarityValue similarityValue);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/