using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NUnit.Framework;

namespace NW.NGramTextClassification.CLI.UnitTests.ArgumentParsing
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
            Assert.That(actual, Is.InstanceOf<ComponentBagFactory>());

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeComponentBag_WhenInvoked()
        {

            // Arrange
            // Act
            ComponentBag actual
                = new ComponentBagFactory().Create();

            // Assert
            Assert.That(actual, Is.InstanceOf<ComponentBag>());

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}