using System;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class LibraryBrokerTests
    {

        #region Fields

        private static TestCaseData[] libraryBrokerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: null,
                                settingsFactory: new TextClassifierSettingsFactory(),
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("componentsFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: new TextClassifierComponentsFactory(),
                                settingsFactory: null,
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("settingsFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: new TextClassifierComponentsFactory(),
                                settingsFactory: new TextClassifierSettingsFactory(),
                                textClassifierFactory: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(libraryBrokerExceptionTestCases))]
        public void LibraryBroker_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LibraryBroker_ShouldCreateAnObjectOfTypeLibraryBroker_WhenInvoked()
        {

            // Arrange
            // Act
            LibraryBroker actual = new LibraryBroker();

            // Assert
            Assert.IsInstanceOf<LibraryBroker>(actual);
            Assert.IsInstanceOf<int>(LibraryBroker.Success);
            Assert.IsInstanceOf<int>(LibraryBroker.Failure);
            Assert.IsInstanceOf<string>(LibraryBroker.SeparatorLine);
            Assert.IsInstanceOf<Func<string, string>>(LibraryBroker.ErrorMessageFormatter);

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