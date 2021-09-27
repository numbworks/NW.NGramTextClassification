using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierResultTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                    new TextClassifierResult(
                        ObjectMother.Shared_Text1_Label,
                        ObjectMother.TextClassifierResult_SimilarityIndexes1,
                        ObjectMother.TextClassifierResult_SimilarityIndexAverages1
                        ),
                    ObjectMother.TextClassifierResult_ToString1
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                    new TextClassifierResult(
                        null,
                        ObjectMother.TextClassifierResult_SimilarityIndexes1,
                        ObjectMother.TextClassifierResult_SimilarityIndexAverages1
                        ),
                    ObjectMother.TextClassifierResult_ToString1WithNullLabel
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                    new TextClassifierResult(
                        null,
                        null,
                        null
                        ),
                    ObjectMother.TextClassifierResult_AllNulls
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked
            (TextClassifierResult textClassifierResult, string expected)
        {

            // Arrange
            // Act
            string actual = textClassifierResult.ToString();

            // Assert
            Assert.IsTrue(
                string.Equals(
                    expected,
                    actual,
                    StringComparison.InvariantCulture));

        }

        [Test]
        public void TextClassifierResult_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierResult actual
                = new TextClassifierResult(
                        ObjectMother.Shared_Text1_Label,
                        ObjectMother.TextClassifierResult_SimilarityIndexes1,
                        ObjectMother.TextClassifierResult_SimilarityIndexAverages1
                    );

            // Assert
            Assert.IsInstanceOf<TextClassifierResult>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2021
*/