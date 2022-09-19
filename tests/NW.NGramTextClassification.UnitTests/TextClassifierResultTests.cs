using System;
using System.Collections.Generic;
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
                        label: ObjectMother.Shared_LabeledExample01.Label,
                        indexes: ObjectMother.TextClassifierResult_SimilarityIndexes,
                        indexAverages: ObjectMother.TextClassifierResult_SimilarityIndexAverages
                        ),
                    ObjectMother.TextClassifierResult_ToString1
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                    new TextClassifierResult(
                        label: null,
                        indexes: ObjectMother.TextClassifierResult_SimilarityIndexes,
                        ObjectMother.TextClassifierResult_SimilarityIndexAverages
                        ),
                    ObjectMother.TextClassifierResult_ToString1WithNullLabel
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                    new TextClassifierResult(
                        label: null,
                        indexes: null,
                        indexAverages: null
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
                        label: ObjectMother.Shared_LabeledExample01.Label,
                        indexes: ObjectMother.TextClassifierResult_SimilarityIndexes,
                        indexAverages: ObjectMother.TextClassifierResult_SimilarityIndexAverages
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
    Last Update: 19.09.2022
*/