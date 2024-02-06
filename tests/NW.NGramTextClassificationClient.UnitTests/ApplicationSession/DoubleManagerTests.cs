using NW.NGramTextClassificationClient.ApplicationSession;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class DoubleManagerTests
    {

        #region Fields

        private static TestCaseData[] isValidTestCases =
        {

            new TestCaseData("Some message", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_01"),

            new TestCaseData(null, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_02"),

            new TestCaseData(string.Empty, false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_03"),

            new TestCaseData("2.0", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_04"),

            new TestCaseData("-0.3", false)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_05"),

            new TestCaseData("0.0", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_06"),

            new TestCaseData("0.3", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_07"),

            new TestCaseData("1.0", true)
                .SetArgDisplayNames($"{nameof(isValidTestCases)}_08")

        };
        private static TestCaseData[] parseOrDefaultTestCases =
        {

            new TestCaseData(null, null)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_01"),

            new TestCaseData("0.0", 0.0)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_02"),

            new TestCaseData("0.3", 0.3)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_03"),

            new TestCaseData("1.0", 1.0)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_04"),

            new TestCaseData("2.0", 2.0)
                .SetArgDisplayNames($"{nameof(parseOrDefaultTestCases)}_05"),

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(isValidTestCases))]
        public void IsValid_ShouldReturnExpectedBoolean_WhenProperArgument(string value, bool expected)
        {

            // Arrange
            // Act
            bool actual = new DoubleManager().IsValid(value);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [TestCaseSource(nameof(parseOrDefaultTestCases))]
        public void ParseOrDefault_ShouldReturnExpectedDouble_WhenProperArgument(string value, double? expected)
        {

            // Arrange
            // Act
            double? actual = new DoubleManager().ParseOrDefault(value);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void DoubleManager_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            DoubleManager actual = new DoubleManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<DoubleManager>());

            Assert.That(DoubleManager.MininumValue, Is.InstanceOf<double>());
            Assert.That(DoubleManager.MaximumValue, Is.InstanceOf<double>());

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 01.02.2024
*/
