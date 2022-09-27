using System;
using NW.NGramTextClassificationClient.Application;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class ApplicationSectionsFactoryTests
    {

        #region Fields

        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationSectionsFactory()
                                .Create(
                                    libraryBroker: null,
                                    sessionManagerComponents: new SessionManagerComponents())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("libraryBroker").Message
            ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new ApplicationSectionsFactory()
                                .Create(
                                    libraryBroker: new LibraryBroker(),
                                    sessionManagerComponents: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("sessionManagerComponents").Message
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
        public void ApplicationSectionsFactory_ShouldCreateAnObjectOfTypeApplicationSectionsFactory_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationSectionsFactory actual = new ApplicationSectionsFactory();

            // Assert
            Assert.IsInstanceOf<ApplicationSectionsFactory>(actual);

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeApplicationSections_WhenInvoked()
        {

            // Arrange
            // Act
            ApplicationSections actual
                = new ApplicationSectionsFactory()
                                .Create(
                                    libraryBroker: new LibraryBroker(),
                                    sessionManagerComponents: new SessionManagerComponents());

            // Assert
            Assert.IsInstanceOf<ApplicationSections>(actual);

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