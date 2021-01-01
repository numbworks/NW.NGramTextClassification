namespace NW.NGramTextClassification
{
    public class SimilarityIndex
    {

        // Fields
        // Properties
        public ulong LabeledExtractId { get; }
        public string Label { get; }
        public double Value { get; }

        // Constructors
        public SimilarityIndex(ulong id, string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

            LabeledExtractId = id;
            Label = label;
            Value = value; 

        }

        // Methods
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/