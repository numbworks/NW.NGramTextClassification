using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;

namespace NW.NGramTextClassificationClient.UnitTests.Utilities
{

    public class FakeTextClassifierComponentsFactory : ITextClassifierComponentsFactory
    {

        #region Fields

        #endregion

        #region Properties

        private TextClassifierComponents _fakeComponents;

        #endregion

        #region Constructors

        public FakeTextClassifierComponentsFactory(TextClassifierComponents fakeComponents)
        {

            _fakeComponents = fakeComponents;

        }

        #endregion

        #region Methods_public

        public TextClassifierComponents Create()
                => _fakeComponents;

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/