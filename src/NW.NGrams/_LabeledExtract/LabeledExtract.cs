using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class LabeledExtract
    {

        // Fields
        // Properties
        public ulong Id { get; }
        public string Label { get; }
        public string Text { get; }
        public List<INGram> TextAsNGrams { get; }

        // Constructors
        public LabeledExtract
            (ulong id, string label, string text, List<INGram> textAsNGrams)
        {

            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentNullException(nameof(label));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));
            if (textAsNGrams == null)
                throw new ArgumentNullException(nameof(textAsNGrams));
            if (textAsNGrams.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(textAsNGrams)));

            Id = id;
            Label = label;
            Text = text;
            TextAsNGrams = textAsNGrams;

        }

        // Methods
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/