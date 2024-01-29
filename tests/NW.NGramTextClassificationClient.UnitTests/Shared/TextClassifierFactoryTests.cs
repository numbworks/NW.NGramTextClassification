using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class TextClassifierFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void TextClassifierFactory_ShouldCreateAnObjectOfTypeTextClassifierFactory_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifierFactory actual = new TextClassifierFactory();

            // Assert
            Assert.IsInstanceOf<TextClassifierFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeTextClassifier_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifier actual 
                = new TextClassifierFactory().Create(componentBag: new ComponentBag(), settingBag: new SettingBag());

            // Assert
            Assert.IsInstanceOf<TextClassifier>(actual);

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