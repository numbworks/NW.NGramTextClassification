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
                        label: LabeledExamples.ObjectMother.LabeledExample01.Label,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                        ),
                    Utilities.ObjectMother.TextClassifierResult01_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                    new TextClassifierResult(
                        label: null,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                        ),
                    Utilities.ObjectMother.TextClassifierResult_AsStringWithNullLabel
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                    new TextClassifierResult(
                        label: null,
                        indexes: null,
                        indexAverages: null
                        ),
                    Utilities.ObjectMother.TextClassifierResult_AllNulls
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
                        label: LabeledExamples.ObjectMother.LabeledExample01.Label,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
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
    Last Update: 25.09.2022
*/