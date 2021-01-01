namespace NW.NGramTextClassification
{
    public class SimilarityIndexAverage
    {

        // Fields
        // Properties
        public string Label { get; }
        public double Value { get; }

        // Constructors
        public SimilarityIndexAverage(string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

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