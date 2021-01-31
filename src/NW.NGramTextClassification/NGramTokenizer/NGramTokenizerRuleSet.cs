namespace NW.NGramTextClassification
{
    public class NGramTokenizerRuleSet : INGramTokenizerRuleSet
    {

        // Fields
        // Properties
        public bool DoForMonograms { get; }
        public bool DoForBigrams { get; }
        public bool DoForTrigrams { get; }
        public bool DoForFourgrams { get; }
        public bool DoForFivegrams { get; }

        // Constructors
        public NGramTokenizerRuleSet
            (bool doForMonograms, 
             bool doForBigrams, 
             bool doForTrigrams, 
             bool doForFourgrams, 
             bool doForFivegrams)
        {

            DoForMonograms = doForMonograms;
            DoForBigrams = doForBigrams;
            DoForTrigrams = doForTrigrams;
            DoForFourgrams = doForFourgrams;
            DoForFivegrams = doForFivegrams;

        }
        public NGramTokenizerRuleSet()
            : this(true, true, true, false, false) {}

        // Methods (public)
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(DoForMonograms)}: '{DoForMonograms.ToString()}'",
                    $"{nameof(DoForBigrams)}: '{DoForBigrams.ToString()}'",
                    $"{nameof(DoForTrigrams)}: '{DoForTrigrams.ToString()}'",
                    $"{nameof(DoForFourgrams)}: '{DoForFourgrams.ToString()}'",
                    $"{nameof(DoForFivegrams)}: '{DoForFivegrams.ToString()}'"
                    );

            return $"[ {content} ]";

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.01.2021

*/