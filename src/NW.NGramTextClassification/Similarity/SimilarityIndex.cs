namespace NW.NGramTextClassification
{
    public class SimilarityIndex
    {

        // Fields
        // Properties
        public ulong Id { get; }
        public string Label { get; }
        public double Value { get; }

        // Constructors
        public SimilarityIndex(ulong id, string label, double value)
        {

            Validator.ValidateStringNullOrWhiteSpace(label, nameof(label));

            Id = id;
            Label = label;
            Value = value; 

        }

        // Methods
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Id)}: '{Id}'",
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
    Last Update: 31.12.2020

*/