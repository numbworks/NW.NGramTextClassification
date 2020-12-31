using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ILabeledTextJsonDeserializer
    {

        /// <summary>
        /// It expects a JSON string that looks like: { "LabeledTextId": 1, "Label": "sv", "Text": "Vår kund erbjuder..." }.
        /// </summary>
        List<LabeledExtract> Do(string json);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/