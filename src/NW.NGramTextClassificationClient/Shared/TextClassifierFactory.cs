using NW.NGramTextClassification;
using NW.NGramTextClassification.Bags;

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

        public TextClassifier Create(ComponentBag componentBag, SettingBag settingBag)
            => new TextClassifier(componentBag, settingBag);

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/