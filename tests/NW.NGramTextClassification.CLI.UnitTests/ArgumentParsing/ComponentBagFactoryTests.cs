using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NUnit.Framework;
using System;

namespace NW.NGramTextClassification.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class ComponentBagFactoryTests
    {

        #region Fields

        private static TestCaseData[] componentBagFactoryCreateTestCases =
        {

            new TestCaseData(
                false,
                new Action<ComponentBag>(actual => Assert.That(actual.NowFunction, Is.EqualTo(ComponentBag.DefaultNowFunction)))
            ).SetArgDisplayNames($"{nameof(componentBagFactoryCreateTestCases)}_01"),

            new TestCaseData(
                true,
                new Action<ComponentBag>(actual => Assert.That(actual.NowFunction, Is.EqualTo(ComponentBag.DefaultUTCNowFunction)))
            ).SetArgDisplayNames($"{nameof(componentBagFactoryCreateTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagFactoryCreateTestCases))]
        public void Create_ShouldCreateTheExpectedComponentBag_WhenInvoked(bool enableUTC, Action<ComponentBag> assertAction)
        {

            // Arrange
            // Act
            ComponentBag actual = new ComponentBagFactory().Create(enableUTC: enableUTC);

            // Assert
            Assert.That(actual, Is.InstanceOf<ComponentBag>());
            assertAction(actual);

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}