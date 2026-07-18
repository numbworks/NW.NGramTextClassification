using System;
using NW.NGramTextClassification.CLI.AsciiBanners;
using NUnit.Framework;

namespace NW.NGramTextClassification.CLI.UnitTests.AsciiBanners
{
    [TestFixture]
    public class AsciiBannerManagerTests
    {

        #region Fields
        private static TestCaseData[] createStandardExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new AsciiBannerManager().CreateStandard(version: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
            ).SetArgDisplayNames($"{nameof(createStandardExceptionTestCases)}_01")

        };
        private static TestCaseData[] createMiniExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new AsciiBannerManager().CreateMini(version: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
            ).SetArgDisplayNames($"{nameof(createMiniExceptionTestCases)}_01")

        };
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Create_ShouldReturnStandardBanner_WhenStandardBannerSmallerThanTerminalWidth()
        {

            // Arrange
            string version = "1.0.0.0";
            uint terminalWidth = 80;

            // Act
            string actual = new AsciiBannerManager().Create(version, terminalWidth);

            // Assert
            Assert.That(actual.Contains(version), Is.True);

        }

        [Test]
        public void Create_ShouldReturnMiniBanner_WhenStandardBannerGreaterThanTerminalWidth()
        {

            // Arrange
            string version = "1.0.0.0";
            uint terminalWidth = 20;

            // Act
            string actual = new AsciiBannerManager().Create(version, terminalWidth);

            // Assert
            Assert.That(actual.Contains(version), Is.True);

        }

        [Test]
        public void CreateStandard_ShouldReturnStandardBannerThatContainsProvidedVersion_WhenInvoked()
        {

            // Arrange
            string version = "1.0.0.0";

            // Act
            string actual = new AsciiBannerManager().CreateStandard(version);

            // Assert
            Assert.That(actual.Contains(version), Is.True);

        }

        [Test]
        public void CreateMini_ShouldReturnMiniBannerThatContainsProvidedVersion_WhenInvoked()
        {

            // Arrange
            string version = "1.0.0.0";
            string expected = string.Join(Environment.NewLine, "********************", "* NWNGRAM v1.0.0.0 *", "********************", string.Empty);

            // Act
            string actual = new AsciiBannerManager().CreateMini(version);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }


        [TestCaseSource(nameof(createStandardExceptionTestCases))]
        public void CreateStandard_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createMiniExceptionTestCases))]
        public void CreateMini_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}
