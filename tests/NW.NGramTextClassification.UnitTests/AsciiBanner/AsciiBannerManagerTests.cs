using System;
using NW.NGramTextClassification.AsciiBanner;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.AsciiBanner
{
    [TestFixture]
    public class AsciiBannerManagerTests
    {

        #region Fields

        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new AsciiBannerManager().Create(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
            ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Create_ShouldReturnAnAsciiBannerThatContainsProvidedVersion_WhenInvoked()
        {

            // Arrange
            string version = "1.0.0.0";

            // Act
            string actual = new AsciiBannerManager().Create(version);

            // Assert
            Assert.That(actual, Does.Contain(version));

        }

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 31.01.2024
*/
