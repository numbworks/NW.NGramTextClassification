using NW.NGramTextClassification;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="IComponentCollectionFactory/>
    public class ComponentCollectionFactory : IComponentCollectionFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ComponentCollectionFactory"/> instance.</summary>
        public ComponentCollectionFactory() { }

        #endregion

        #region Methods_public

        public ComponentCollection Create()
            => new ComponentCollection();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.01.2024
*/