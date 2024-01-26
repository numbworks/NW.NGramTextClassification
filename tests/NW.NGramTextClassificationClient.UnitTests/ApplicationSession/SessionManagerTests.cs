using System;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class SessionManagerTests
    {

        #region Fields

        private static TestCaseData[] sessionManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SessionManager(null, new DependencyBag())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("libraryBroker").Message
            ).SetArgDisplayNames($"{nameof(sessionManagerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new SessionManager(new LibraryBroker(), null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("dependencyBag").Message
            ).SetArgDisplayNames($"{nameof(sessionManagerExceptionTestCases)}_02")

        };
        private static TestCaseData[] addExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SessionManager(
                                libraryBroker: new LibraryBroker(),
                                dependencyBag: new DependencyBag()
                            ).Add(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("app").Message
            ).SetArgDisplayNames($"{nameof(addExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(sessionManagerExceptionTestCases))]
        public void SessionManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(addExceptionTestCases))]
        public void Add_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SessionManager_ShouldCreateAnObjectOfTypeSessionManager_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManager actual
                = new SessionManager(
                        libraryBroker: new LibraryBroker(),
                        dependencyBag: new DependencyBag());

            // Assert
            Assert.IsInstanceOf<SessionManager>(actual);

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