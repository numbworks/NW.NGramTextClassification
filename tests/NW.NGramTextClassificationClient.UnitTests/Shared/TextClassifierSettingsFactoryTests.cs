using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class TextClassifierSettingsFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void TextClassifierSettingsFactory_ShouldCreateAnObjectOfTypeTextClassifierSettingsFactory_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifierSettingsFactory actual = new TextClassifierSettingsFactory();

            // Assert
            Assert.IsInstanceOf<TextClassifierSettingsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeTextClassifierSettings_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifierSettings actual
                = new TextClassifierSettingsFactory().Create();

            // Assert
            Assert.IsInstanceOf<TextClassifierSettings>(actual);

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