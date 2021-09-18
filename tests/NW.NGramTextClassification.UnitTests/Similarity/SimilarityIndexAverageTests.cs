using System;
using NUnit.Framework;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SimilarityIndexAverageTests
    {

        // Fields
        private static TestCaseData[] similarityIndexAverageExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexAverage(
                                    null,
                                    ObjectMother.SimilarityIndexAverage_Value1
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndexAverage_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(similarityIndexAverageExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexAverage(
                                    ObjectMother.SimilarityIndexAverage_LabelOnlyWhiteSpaces,
                                    ObjectMother.SimilarityIndexAverage_Value1
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndexAverage_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(similarityIndexAverageExceptionTestCases)}_02")

        };

        // SetUp
        // Tests
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

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 22.01.2021

*/