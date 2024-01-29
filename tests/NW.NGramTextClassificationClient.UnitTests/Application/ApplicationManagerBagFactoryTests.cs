using System;
using NW.NGramTextClassificationClient.Application;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class ApplicationManagerBagFactoryTests
    {

        #region Fields

        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBagFactory()
                                .Create(
                                    libraryBroker: null,
                                    sessionManagerBag: new SessionManagerBag())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("libraryBroker").Message
            ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationManagerBagFactory()
                                .Create(
                                    libraryBroker: new LibraryBroker(),
                                    sessionManagerBag: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManagerBag").Message
            ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ApplicationManagerBagFactory_ShouldCreateAnObjectOfTypeApplicationManagerBagFactory_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationManagerBagFactory actual = new ApplicationManagerBagFactory();

            // Assert
            Assert.IsInstanceOf<ApplicationManagerBagFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeApplicationManagerBag_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationManagerBag actual
                = new ApplicationManagerBagFactory()
                                .Create(
                                    libraryBroker: new LibraryBroker(),
                                    sessionManagerBag: new SessionManagerBag());

            // Assert
            Assert.IsInstanceOf<ApplicationManagerBag>(actual);

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
    Last Update: 26.01.2024
*/