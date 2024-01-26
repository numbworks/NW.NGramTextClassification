using NW.NGramTextClassification;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class ComponentCollectionFactoryTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void ComponentCollectionFactory_ShouldCreateAnObjectOfTypeComponentCollectionFactory_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentCollectionFactory actual = new ComponentCollectionFactory();

            // Assert
            Assert.IsInstanceOf<ComponentCollectionFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeComponentCollection_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentCollection actual
                = new ComponentCollectionFactory().Create();

            // Assert
            Assert.IsInstanceOf<ComponentCollection>(actual);

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