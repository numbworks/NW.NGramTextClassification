using RUBN.Shared;

namespace NW.NGrams
{
    public interface ILabeledTextNGramsCreator
    {
        string LabeledTextJson { get; }
        INGramsTextClassifier NGramsTextClassifier { get; set; }
        ITokenizationStrategies TokenizationStrategies { get; }

        /// <summary>
        /// It creates a A List<LabeledTextNGrams> out of Resources/LabeledTextsJson.txt.
        /// 'arrLabels' let you filter by label(s) to consider.
        /// For ex. {"en", "sv" } will include just the labeled texts with Label="sv" and Label="en".
        /// Default (null) will include all of them.
        /// </summary>
        Outcome Do(string[] arrLabels = null);
    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 24.08.2018 
 * 
 */
