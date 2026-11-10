using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifiers;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
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

        #region Methods (public)

        public TextClassifier Create(ComponentBag componentBag, SettingBag settingBag)
            => new TextClassifier();

        #endregion

        #region Methods (private)
        #endregion

    }
}