using System;
using NUnit.Framework;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests
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
                                    ObjectMother.SimilarityIndexAverage_Value1
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(similarityIndexAverageExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexAverage(
                                    ObjectMother.Validator_StringOnlyWhiteSpaces,
                                    ObjectMother.SimilarityIndexAverage_Value1
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
                        ObjectMother.SimilarityIndexAverage_Label1,
                        ObjectMother.SimilarityIndexAverage_Value1
                    );

            // Assert
            Assert.IsInstanceOf<SimilarityIndexAverage>(actual);

        }

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            // Assert
            Assert.IsTrue(
                string.Equals(
                    ObjectMother.SimilarityIndexAverage_ToString1,
                    ObjectMother.SimilarityIndexAverage1.ToString(),
                    StringComparison.InvariantCulture));

        }

        [TestCaseSource(nameof(similarityIndexAverageExceptionTestCases))]
        public void SimilarityIndexAverage_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 19.09.2021

*/