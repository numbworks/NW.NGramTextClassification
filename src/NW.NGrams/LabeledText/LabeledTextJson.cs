namespace NW.NGrams
{
    public class LabeledTextJson
    {

        // Fields
        // Properties
        public ulong LabeledTextId { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }

        // Constructors
        public LabeledTextJson() { }

        // Methods
        public override string ToString()
        {

            return string.Concat(
                "{ \"",
                 nameof(LabeledTextId),
                 "\": \"",
                 LabeledTextId.ToString(),
                 "\", \"",
                 nameof(Label),
                 "\": \"",
                 Label,
                 "\", \"",
                 nameof(Text),
                 "\": \"",
                 GetShorter(Text),
                 "\" }"); 

        }

        // Methods (private)
        private string GetShorter(string text)
        {

            if (string.IsNullOrWhiteSpace(text))
                return text;
            if (text.Length < 10)
                return text;

            return Text.Substring(0, 10) + " [...]";

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/