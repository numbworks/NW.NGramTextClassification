using System;

namespace NW.NGrams
{
    public class LabeledTextSimilarityAverage : ILabeledTextSimilarityValue
    {

        // Fields
        // Properties
        public string Label { get; set; }
        public double Average { get; set; }

        // Constructors
        public LabeledTextSimilarityAverage(string strLabel, double dblAverage)
        {

            Label = strLabel;
            Average = dblAverage;

        }

        // Methods
        public string ToHeader()
        {

            /* Label   Average */
            return String.Format("{0}\t{1}", nameof(Label), nameof(Average));

        }
        public override string ToString()
        {

            /* 1   sv   */
            return String.Format("{0}\t{1}",Label, Average.ToString());

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 17.01.2018 
 *  Description: It defines a Similarity Average for a labeled text.
 * 
 */
