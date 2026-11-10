using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifiers;
using NW.Shared.Files;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Bags
{
    [TestFixture]
    public class ComponentBagTests
    {

        #region Fields

        private static TestCaseData[] componentBagDefaultValueTestCases =
        {

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    fileManager: null,
                                    textClassifier: new TextClassifier(),
                                    loggingAction: ComponentBag.DefaultLoggingAction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.FileManager, Is.InstanceOf<IFileManager>()))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_01"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    fileManager: new FileManager(),
                                    textClassifier: null,
                                    loggingAction: ComponentBag.DefaultLoggingAction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.TextClassifier, Is.InstanceOf<ITextClassifier>()))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_02"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    fileManager: new FileManager(),
                                    textClassifier: new TextClassifier(),
                                    loggingAction: null
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.LoggingAction, Is.EqualTo(ComponentBag.DefaultLoggingAction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagDefaultValueTestCases))]
        public void ComponentBag_ShouldUseDefaultValue_WhenParameterIsNull(Func<ComponentBag> initFunction, Action<ComponentBag> assertAction)
        {

            // Arrange
            // Act
            ComponentBag componentBag = initFunction();

            // Assert
            assertAction(componentBag);

        }

        [Test]
        public void ComponentBag_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ComponentBag actual = new ComponentBag();

            // Assert
            Assert.That(actual, Is.InstanceOf<ComponentBag>());

            Assert.That(actual.FileManager, Is.InstanceOf<IFileManager>());
            Assert.That(actual.TextClassifier, Is.InstanceOf<ITextClassifier>());
            Assert.That(actual.LoggingAction, Is.InstanceOf<Action<string>>());

            Assert.That(ComponentBag.DefaultLoggingActionDateFormat, Is.InstanceOf<string>());
            Assert.That(ComponentBag.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultLoggingActionUTC, Is.InstanceOf<Action<string>>());

        }

        [Test]
        public void DefaultLoggingAction_ShouldPrintToConsole_WhenInvoked()
        {

            // Arrange
            // Act
            // Assert
            Assert.DoesNotThrow(() => ComponentBag.DefaultLoggingAction("Test message"));

        }

        #endregion

        #region TearDown
        #endregion

    }
}