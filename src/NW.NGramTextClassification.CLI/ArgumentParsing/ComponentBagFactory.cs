using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IComponentBagFactory"/>
    public class ComponentBagFactory : IComponentBagFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ComponentBagFactory"/> instance.</summary>
        public ComponentBagFactory() { }

        #endregion

        #region Methods (public)

        public ComponentBag Create()
            => new ComponentBag();

        #endregion

    }
}