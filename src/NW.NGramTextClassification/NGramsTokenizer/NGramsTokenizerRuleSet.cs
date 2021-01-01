namespace NW.NGramTextClassification
{
    public class NGramsTokenizerRuleSet : INGramsTokenizerRuleSet
    {

        // Fields
        // Properties
        public bool DoForMonograms { get; }
        public bool DoForBigrams { get; }
        public bool DoForTrigrams { get; }

        // Constructors
        public NGramsTokenizerRuleSet
            (bool doForMonograms, bool doForBigrams, bool doForTrigrams)
        {

            DoForMonograms = doForMonograms;
            DoForBigrams = doForBigrams;
            DoForTrigrams = doForTrigrams;

        }
        public NGramsTokenizerRuleSet()
            : this(true, true, true) {}

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/