namespace NW.NGramTextClassification.NGrams
{
    /// <inheritdoc cref="ANGram"/>
    public class Trigram : ANGram, INGram
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Trigram"/> instance.</summary>
        public Trigram(ITokenizationStrategy strategy, string value)
            : base(3, strategy, value) { }

        /// <summary>Initializes a <see cref="Trigram"/> instance using default parameters.</summary>
        public Trigram(string value)
            : base(3, new TokenizationStrategy(), value) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/