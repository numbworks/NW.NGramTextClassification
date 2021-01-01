using System.Collections.Generic;

namespace NW.NGramTextClassification
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

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));
            Validator.ValidateStringNullOrWhiteSpace(text, nameof(text));
            Validator.ValidateList(textAsNGrams, nameof(textAsNGrams));

            Id = id;
            Label = label;
            Text = text;
            TextAsNGrams = textAsNGrams;

        }

        // Methods
        // Methods (private)
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
                    $"{nameof(Label)}: '{Label}'",
                    $"{nameof(Text)}: '{Text}'",
                    $"{nameof(TextAsNGrams)}: '{TextAsNGrams?.Count.ToString() ?? "null"}'"
                    );

            return $"[ {content} ]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/