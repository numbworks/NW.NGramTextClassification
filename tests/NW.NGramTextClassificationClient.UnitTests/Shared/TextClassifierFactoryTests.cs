using NW.NGramTextClassification;
using NW.NGramTextClassification.Bags;
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
            Assert.That(actual, Is.InstanceOf<TextClassifierFactory>());

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeTextClassifier_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassifier actual 
                = new TextClassifierFactory().Create(componentBag: new ComponentBag(), settingBag: new SettingBag());

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassifier>());

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
    Last Update: 01.02.2024
*/