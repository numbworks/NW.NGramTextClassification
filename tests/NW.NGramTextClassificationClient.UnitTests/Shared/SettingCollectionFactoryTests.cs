using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class SettingCollectionFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SettingCollectionFactory_ShouldCreateAnObjectOfTypeSettingCollectionFactory_WhenInvoked()
        {

            // Arrange
            // Act
            SettingCollectionFactory actual = new SettingCollectionFactory();

            // Assert
            Assert.IsInstanceOf<SettingCollectionFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeSettingCollection_WhenInvoked()
        {

            // Arrange
            // Act
            SettingCollection actual
                = new SettingCollectionFactory().Create();

            // Assert
            Assert.IsInstanceOf<SettingCollection>(actual);

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
    Last Update: 26.01.2024
*/