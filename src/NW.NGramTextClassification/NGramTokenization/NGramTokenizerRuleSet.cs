namespace NW.NGramTextClassification.NGramTokenization
{
    /// <inheritdoc cref="INGramTokenizerRuleSet"/>
    public class NGramTokenizerRuleSet : INGramTokenizerRuleSet
    {

        #region Fields
        #endregion

        #region Properties

        public static bool DefaultDoForMonograms { get; } = true;
        public static bool DefaultDoForBigrams { get; } = true;
        public static bool DefaultDoForTrigrams { get; } = true;
        public static bool DefaultDoForFourgrams { get; } = true;
        public static bool DefaultDoForFivegrams { get; } = true;

        public bool DoForMonograms { get; }
        public bool DoForBigrams { get; }
        public bool DoForTrigrams { get; }
        public bool DoForFourgrams { get; }
        public bool DoForFivegrams { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="NGramTokenizerRuleSet"/> instance.</summary>
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

        /// <summary>Initializes a <see cref="NGramTokenizerRuleSet"/> instance using default parameters.</summary>
        public NGramTokenizerRuleSet()
            : this(
                  DefaultDoForMonograms, 
                  DefaultDoForBigrams,
                  DefaultDoForTrigrams,
                  DefaultDoForFourgrams,
                  DefaultDoForFivegrams
                  ) { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(DoForMonograms)}: '{DoForMonograms}'",
                    $"{nameof(DoForBigrams)}: '{DoForBigrams}'",
                    $"{nameof(DoForTrigrams)}: '{DoForTrigrams}'",
                    $"{nameof(DoForFourgrams)}: '{DoForFourgrams}'",
                    $"{nameof(DoForFivegrams)}: '{DoForFivegrams}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/