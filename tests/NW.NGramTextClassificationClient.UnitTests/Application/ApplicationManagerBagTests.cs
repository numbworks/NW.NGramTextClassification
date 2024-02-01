using System;
using NW.NGramTextClassificationClient.Application;
using NW.NGramTextClassificationClient.ApplicationAbout;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class ApplicationManagerBagTests
    {

        #region Fields

        private static TestCaseData[] applicationSectionsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBag(
                                aboutManager: null,
                                sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerBag()))
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("aboutManager").Message
            ).SetArgDisplayNames($"{nameof(applicationSectionsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBag(
                                aboutManager: new AboutManager(new LibraryBroker()),
                                sessionManager: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManager").Message
            ).SetArgDisplayNames($"{nameof(applicationSectionsExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(applicationSectionsExceptionTestCases))]
        public void ApplicationManagerBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ApplicationManagerBag_ShouldCreateAnObjectOfTypeApplicationManagerBag_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationManagerBag actual
                = new ApplicationManagerBag(
                        aboutManager: new AboutManager(new LibraryBroker()),
                        sessionManager: new SessionManager(new LibraryBroker(), new SessionManagerBag()));

            // Assert
            Assert.That(actual, Is.InstanceOf<ApplicationManagerBag>());
            Assert.That(actual.AboutManager, Is.InstanceOf<IAboutManager>());
            Assert.That(actual.SessionManager, Is.InstanceOf<ISessionManager>());

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