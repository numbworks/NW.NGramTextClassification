using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ILabeledTextNGramsCreator
    {

        /// <summary>
        /// It creates a A List<LabeledTextNGrams> out of Resources/LabeledTextsJson.txt.
        /// 'arrLabels' let you filter by label(s) to consider.
        /// For ex. {"en", "sv" } will include just the labeled texts with Label="sv" and Label="en".
        /// Default (null) will include all of them.
        /// </summary>
        List<LabeledTextNGrams> Do(string labeledTextJson, string[] labels = null);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/