using System;

namespace NW.NGrams
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

            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentNullException(nameof(label));

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
    Last Update: 30.12.2020

*/