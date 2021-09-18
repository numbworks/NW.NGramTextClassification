using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.NGrams
{
    /// <inheritdoc cref="ANGram"/>
    public class Monogram : ANGram, INGram
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Monogram"/> instance.</summary>
        public Monogram(ITokenizationStrategy strategy, string value)
            : base(1, strategy, value) { }

        /// <summary>Initializes a <see cref="Monogram"/> instance using default parameters.</summary>
        public Monogram(string value)
            : base(1, new TokenizationStrategy(), value) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/