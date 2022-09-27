using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class TextClassifierComponentsFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void TextClassifierComponentsFactory_ShouldCreateAnObjectOfTypeTextClassifierComponentsFactory_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifierComponentsFactory actual = new TextClassifierComponentsFactory();

            // Assert
            Assert.IsInstanceOf<TextClassifierComponentsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeTextClassifierComponents_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifierComponents actual
                = new TextClassifierComponentsFactory().Create();

            // Assert
            Assert.IsInstanceOf<TextClassifierComponents>(actual);

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