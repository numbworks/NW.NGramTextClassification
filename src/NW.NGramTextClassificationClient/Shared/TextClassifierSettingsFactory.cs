using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ITextClassifierSettingsFactory"/>
    public class TextClassifierSettingsFactory : ITextClassifierSettingsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierSettingsFactory"/> instance.</summary>
        public TextClassifierSettingsFactory() { }

        #endregion

        #region Methods_public

        public TextClassifierSettings Create()
            => new TextClassifierSettings();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/