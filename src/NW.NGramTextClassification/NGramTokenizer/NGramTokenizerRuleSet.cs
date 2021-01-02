namespace NW.NGramTextClassification
{
    public class NGramTokenizerRuleSet : INGramsTokenizerRuleSet
    {

        // Fields
        // Properties
        public bool DoForMonograms { get; }
        public bool DoForBigrams { get; }
        public bool DoForTrigrams { get; }

        // Constructors
        public NGramTokenizerRuleSet
            (bool doForMonograms, bool doForBigrams, bool doForTrigrams)
        {

            DoForMonograms = doForMonograms;
            DoForBigrams = doForBigrams;
            DoForTrigrams = doForTrigrams;

        }
        public NGramTokenizerRuleSet()
            : this(true, true, true) {}

        // Methods (public)
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(DoForMonograms)}: '{DoForMonograms.ToString()}'",
                    $"{nameof(DoForBigrams)}: '{DoForBigrams.ToString()}'",
                    $"{nameof(DoForTrigrams)}: '{DoForTrigrams.ToString()}'"
                    );

            return $"[ {content} ]";

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/