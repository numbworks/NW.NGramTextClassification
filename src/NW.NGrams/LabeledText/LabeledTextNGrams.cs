using System.Collections.Generic;

namespace NW.NGrams
{
    public class LabeledTextNGrams
    {

        // Fields
        // Properties
        public ulong LabeledTextId { get; set; }
        public string Label { get; set; }
        public List<string> NGrams { get; set; }

        // Constructors
        public LabeledTextNGrams(ulong id, string label, List<string> nGrams)
        {

            LabeledTextId = id;
            Label = label;
            NGrams = nGrams;

        }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
