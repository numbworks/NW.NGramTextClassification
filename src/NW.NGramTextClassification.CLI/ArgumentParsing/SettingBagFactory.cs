using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <inheritdoc cref="ISettingBagFactory"/>
    public class SettingBagFactory : ISettingBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SettingBagFactory"/> instance.</summary>
        public SettingBagFactory() { }

        #endregion

        #region Methods (public)

        public SettingBag Create()
            => new SettingBag();

        public SettingBag Create(ClassifyData classifyData)
        {

            throw new System.NotImplementedException();

        }

        #endregion

    }
}