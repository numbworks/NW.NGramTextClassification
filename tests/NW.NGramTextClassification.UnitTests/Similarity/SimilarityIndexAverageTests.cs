using System;
using NUnit.Framework;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests.Similarity
{
    [TestFixture]
    public class SimilarityIndexAverageTests
    {

        #region Fields

        private static TestCaseData[] similarityIndexAverageExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexAverage(
                                    null,
                                    ObjectMother.SimilarityIndexAverage01_Value
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(similarityIndexAverageExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexAverage(
                                    Validation.ObjectMother.StringOnlyWhiteSpaces,
                                    ObjectMother.SimilarityIndexAverage01_Value
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(similarityIndexAverageExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SimilarityIndexAverage_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SimilarityIndexAverage actual
                = new SimilarityIndexAverage(
                        ObjectMother.SimilarityIndexAverage01_Label,
                        ObjectMother.SimilarityIndexAverage01_Value
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<SimilarityIndexAverage>());

        }

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            // Assert
            Assert.That(
                string.Equals(
                    ObjectMother.SimilarityIndexAverage01_AsString,
                    ObjectMother.SimilarityIndexAverage01.ToString(),
                    StringComparison.InvariantCulture),
                Is.True);

        }

        [TestCaseSource(nameof(similarityIndexAverageExceptionTestCases))]
        public void SimilarityIndexAverage_ShouldThrowACertainException_WhenUnproperArguments
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