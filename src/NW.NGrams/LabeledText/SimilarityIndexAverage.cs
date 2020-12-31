using System;

namespace NW.NGrams
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

            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentNullException(nameof(label));

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