namespace NW.NGramTextClassification.NGrams
{
    /// <inheritdoc cref="ANGram"/>
    public class Fivegram : ANGram, INGram
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="Fivegram"/> instance.</summary>
        public Fivegram(ITokenizationStrategy strategy, string value)
            : base(5, strategy, value) { }

        /// <summary>Initializes a <see cref="Fivegram"/> instance using default parameters.</summary>
        public Fivegram(string value)
            : base(5, new TokenizationStrategy(), value) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/