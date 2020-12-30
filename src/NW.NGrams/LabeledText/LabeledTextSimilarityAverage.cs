namespace NW.NGrams
{
    public class LabeledTextSimilarityAverage : ILabeledTextSimilarityValue
    {

        // Fields
        // Properties
        public string Label { get; }
        public double Average { get; }

        // Constructors
        public LabeledTextSimilarityAverage(string label, double average)
        {

            Label = label;
            Average = average;

        }

        // Methods
        public string ToHeader()
        {

            /* Label   Average */
            return string.Format("{0}\t{1}", nameof(Label), nameof(Average));

        }
        public override string ToString()
        {

            /* 1   sv   */
            return string.Format("{0}\t{1}",Label, Average.ToString());

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
