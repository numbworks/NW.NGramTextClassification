namespace NW.NGrams
{
    public class LabeledTextSimilarityIndex : ILabeledTextSimilarityValue
    {

        // Fields
        // Properties
        public ulong LabeledTextId { get; }
        public string Label { get; }
        public double SimilarityIndex { get; }

        // Constructors
        public LabeledTextSimilarityIndex(ulong id, string label, double similarityIndex)
        {

            LabeledTextId = id;
            Label = label;
            SimilarityIndex = similarityIndex; 

        }

        // Methods
        public string ToHeader()
        {

            /* LabeledTextId   Label   SimilarityIndex */
            return string.Format("{0}\t{1}\t{2}", nameof(LabeledTextId), nameof(Label), nameof(SimilarityIndex));

        }
        public override string ToString()
        {

            /* 1   sv   0.89 */
            return string.Format("{0}\t{1}\t{2}", LabeledTextId.ToString(), Label, SimilarityIndex.ToString());

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
