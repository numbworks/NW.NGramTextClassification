using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ITextClassifierComponentsFactory/>
    public class TextClassifierComponentsFactory : ITextClassifierComponentsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierComponentsFactory"/> instance.</summary>
        public TextClassifierComponentsFactory() { }

        #endregion

        #region Methods_public

        public TextClassifierComponents Create()
            => new TextClassifierComponents();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/