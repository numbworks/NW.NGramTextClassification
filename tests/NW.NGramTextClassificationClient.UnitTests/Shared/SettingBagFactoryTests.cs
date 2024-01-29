using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class SettingBagFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SettingBagFactory_ShouldCreateAnObjectOfTypeSettingBagFactory_WhenInvoked()
        {

            // Arrange
            // Act
            SettingBagFactory actual = new SettingBagFactory();

            // Assert
            Assert.IsInstanceOf<SettingBagFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeSettingBag_WhenInvoked()
        {

            // Arrange
            // Act
            SettingBag actual
                = new SettingBagFactory().Create();

            // Assert
            Assert.IsInstanceOf<SettingBag>(actual);

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