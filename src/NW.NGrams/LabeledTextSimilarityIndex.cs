using System;

namespace NW.NGrams
{
    public class LabeledTextSimilarityIndex : ILabeledTextSimilarityValue
    {

        // Fields
        // Properties
        public UInt64 LabeledTextId { get; set; }
        public string Label { get; set; }
        public double SimilarityIndex { get; set; }

        // Constructors
        public LabeledTextSimilarityIndex(UInt64 uintId, string strLabel, double dblSimilarityIndex)
        {

            LabeledTextId = uintId;
            Label = strLabel;
            SimilarityIndex = dblSimilarityIndex; 

        }

        // Methods
        public string ToHeader()
        {

            /* LabeledTextId   Label   SimilarityIndex */
            return String.Format("{0}\t{1}\t{2}", nameof(LabeledTextId), nameof(Label), nameof(SimilarityIndex));

        }
        public override string ToString()
        {

            /* 1   sv   0.89 */
            return String.Format("{0}\t{1}\t{2}", LabeledTextId.ToString(), Label, SimilarityIndex.ToString());

        }

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 17.01.2018 
 *  Description: It defines a Similarity Index for a labeled text.
 * 
 */
