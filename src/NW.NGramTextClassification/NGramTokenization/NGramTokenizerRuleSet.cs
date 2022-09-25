using System;

namespace NW.NGramTextClassification.NGramTokenization
{
    /// <inheritdoc cref="INGramTokenizerRuleSet"/>
    public class NGramTokenizerRuleSet : INGramTokenizerRuleSet
    {

        #region Fields
        #endregion

        #region Properties

        public static bool DefaultDoForMonogram { get; } = true;
        public static bool DefaultDoForBigram { get; } = true;
        public static bool DefaultDoForTrigram { get; } = true;
        public static bool DefaultDoForFourgram { get; } = true;
        public static bool DefaultDoForFivegram { get; } = true;

        public bool DoForMonogram { get; }
        public bool DoForBigram { get; }
        public bool DoForTrigram { get; }
        public bool DoForFourgram { get; }
        public bool DoForFivegram { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="NGramTokenizerRuleSet"/> instance.</summary>
        /// <exception cref="ArgumentException"></exception>
        public NGramTokenizerRuleSet(bool doForMonogram, bool doForBigram, bool doForTrigram, bool doForFourgram, bool doForFivegram)
        {

            Validate(doForMonogram, doForBigram, doForTrigram, doForFourgram, doForFivegram);

            DoForMonogram = doForMonogram;
            DoForBigram = doForBigram;
            DoForTrigram = doForTrigram;
            DoForFourgram = doForFourgram;
            DoForFivegram = doForFivegram;

        }

        /// <summary>Initializes a <see cref="NGramTokenizerRuleSet"/> instance using default parameters.</summary>
        public NGramTokenizerRuleSet()
            : this(
                  DefaultDoForMonogram, 
                  DefaultDoForBigram,
                  DefaultDoForTrigram,
                  DefaultDoForFourgram,
                  DefaultDoForFivegram
                  ) { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(DoForMonogram)}: '{DoForMonogram}'",
                    $"{nameof(DoForBigram)}: '{DoForBigram}'",
                    $"{nameof(DoForTrigram)}: '{DoForTrigram}'",
                    $"{nameof(DoForFourgram)}: '{DoForFourgram}'",
                    $"{nameof(DoForFivegram)}: '{DoForFivegram}'"
                    );

            return $"[ {content} ]";

        }

        #endregion

        #region Methods_private

        private void Validate(bool doForMonogram, bool doForBigram, bool doForTrigram, bool doForFourgram, bool doForFivegram)
        {

            if (doForMonogram == false 
                    && doForBigram == false
                    && doForTrigram == false
                    && doForFourgram == false
                    && doForFivegram == false)
                throw new ArgumentException(MessageCollection.AtLeastOneArgumentMustBeTrue);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2021
*/