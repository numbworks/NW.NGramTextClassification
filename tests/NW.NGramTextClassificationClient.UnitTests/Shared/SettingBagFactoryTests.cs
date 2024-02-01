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
            Assert.That(actual, Is.InstanceOf<SettingBagFactory>());

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeSettingBag_WhenInvoked()
        {

            // Arrange
            // Act
            SettingBag actual
                = new SettingBagFactory().Create();

            // Assert
            Assert.That(actual, Is.InstanceOf<SettingBag>());

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