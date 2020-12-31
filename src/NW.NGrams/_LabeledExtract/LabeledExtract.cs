using System.Collections.Generic;

namespace NW.NGrams
{
    public class LabeledExtract
    {

        // Fields
        // Properties
        public ulong Id { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public List<INGram> TextAsNGrams { get; set; }

        // Constructors
        public LabeledExtract() { }

        // Methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/