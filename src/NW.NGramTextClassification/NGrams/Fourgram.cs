namespace NW.NGramTextClassification.NGrams
{
    /// <inheritdoc cref="ANGram"/>
    public class Fourgram : ANGram, INGram
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Fourgram"/> instance.</summary>
        public Fourgram(ITokenizationStrategy strategy, string value)
            : base(4, strategy, value) { }

        /// <summary>Initializes a <see cref="Fourgram"/> instance using default parameters.</summary>
        public Fourgram(string value)
            : base(4, new TokenizationStrategy(), value) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/