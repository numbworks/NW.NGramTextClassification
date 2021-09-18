using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.NGrams
{
    /// <inheritdoc cref="ANGram"/>
    public class Bigram : ANGram, INGram
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Bigram"/> instance.</summary>
        public Bigram(ITokenizationStrategy strategy, string value)
            : base(2, strategy, value) { }

        /// <summary>Initializes a <see cref="Bigram"/> instance using default parameters.</summary>
        public Bigram(string value)
            : base(2, new TokenizationStrategy(), value) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/