using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;

namespace NW.NGramTextClassificationClient.UnitTests.Utilities
{

    public class FakeComponentCollectionFactory : IComponentCollectionFactory
    {

        #region Fields

        #endregion

        #region Properties

        private ComponentCollection _fakeComponentCollection;

        #endregion

        #region Constructors

        public FakeComponentCollectionFactory(ComponentCollection fakeComponentCollection)
        {

            _fakeComponentCollection = fakeComponentCollection;

        }

        #endregion

        #region Methods_public

        public ComponentCollection Create()
                => _fakeComponentCollection;

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.01.2024
*/