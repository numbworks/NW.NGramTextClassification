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
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Label)}: '{Label}'",
                    $"{nameof(Value)}: '{Value.ToString()}'"
                    );

            return $"[ {content} ]";

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2021

*/