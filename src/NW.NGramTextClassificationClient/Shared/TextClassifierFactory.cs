using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ITextClassifierFactory"/>
    public class TextClassifierFactory : ITextClassifierFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierFactory"/> instance.</summary>
        public TextClassifierFactory() { }

        #endregion

        #region Methods_public

        public TextClassifier Create(TextClassifierComponents components, TextClassifierSettings settings)
            => new TextClassifier(components, settings);

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/