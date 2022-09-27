using NW.NGramTextClassificationClient.ApplicationSession;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class SessionManagerComponentsTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SessionManagerComponents_ShouldCreateAnObjectOfTypeSessionManagerComponents_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManagerComponents actual
                    = new SessionManagerComponents();

            // Assert
            Assert.IsInstanceOf<SessionManagerComponents>(actual);

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/