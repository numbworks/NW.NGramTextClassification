using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.CLI.ArgumentParsing;

namespace NW.NGramTextClassification.CLI.UnitTests.Utilities
{

    public class FakeComponentBagFactory : IComponentBagFactory
    {

        #region Fields

        #endregion

        #region Properties

        private ComponentBag _fakeComponentBag;

        #endregion

        #region Constructors

        public FakeComponentBagFactory(ComponentBag fakeComponentBag)
        {

            _fakeComponentBag = fakeComponentBag;

        }

        #endregion

        #region Methods_public

        public ComponentBag Create()
                => _fakeComponentBag;

        #endregion

    }
}