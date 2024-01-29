using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class ComponentBagFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void ComponentBagFactory_ShouldCreateAnObjectOfTypeComponentBagFactory_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentBagFactory actual = new ComponentBagFactory();

            // Assert
            Assert.IsInstanceOf<ComponentBagFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeComponentBag_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentBag actual
                = new ComponentBagFactory().Create();

            // Assert
            Assert.IsInstanceOf<ComponentBag>(actual);

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
    Last Update: 25.01.2024
*/