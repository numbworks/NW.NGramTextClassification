using System;
using NW.NGramTextClassificationClient.ApplicationAbout;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class AboutManagerTests
    {

        #region Fields

        private static TestCaseData[] aboutManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new AboutManager(libraryBroker: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("libraryBroker").Message
            ).SetArgDisplayNames($"{nameof(aboutManagerExceptionTestCases)}_01")

        };
        private static TestCaseData[] addExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new AboutManager(libraryBroker: new LibraryBroker()).Add(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("app").Message
            ).SetArgDisplayNames($"{nameof(addExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(aboutManagerExceptionTestCases))]
        public void AboutManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(addExceptionTestCases))]
        public void Add_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void AboutManager_ShouldCreateAnObjectOfTypeAboutManager_WhenInvoked()
        {

            // Arrange
            // Act
            AboutManager actual = new AboutManager(libraryBroker: new LibraryBroker());

            // Assert
            Assert.IsInstanceOf<AboutManager>(actual);

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