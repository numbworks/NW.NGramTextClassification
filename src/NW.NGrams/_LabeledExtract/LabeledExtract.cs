﻿using System.Collections.Generic;

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

            Validator.ValidateString(label, nameof(label));
            Validator.ValidateString(text, nameof(text));
            Validator.ValidateList(textAsNGrams, nameof(textAsNGrams));

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