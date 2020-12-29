using RUBN.Shared;

namespace NW.NGrams
{
    public interface ILabeledTextJsonDeserializer
    {

        /// <summary>
        /// It expects JSON containing objects like: { "LabeledTextId": 1, "Label": "sv", "Text": "Vår kund erbjuder..." }.
        /// </summary>
        Outcome Do(string strJson);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 20.01.2018
 * 
 */
