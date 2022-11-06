using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    [TestFixture]
    public class TextClassifierResultTests
    {

        #region Fields

        private static TestCaseData[] toStringTestCases =
        {

            new TestCaseData(
                    new TextClassifierResult(
                        textSnippet: TextSnippets.ObjectMother.TextSnippet,
                        label: LabeledExamples.ObjectMother.ShortLabeledExample01.Label,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                        ),
                    ObjectMother.TextClassifierResult_AsString
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),

            new TestCaseData(
                    new TextClassifierResult(
                        textSnippet: null,
                        label: LabeledExamples.ObjectMother.ShortLabeledExample01.Label,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                        ),
                    ObjectMother.TextClassifierResult_AsStringWithNullTextSnippet
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02"),

            new TestCaseData(
                    new TextClassifierResult(
                        textSnippet: TextSnippets.ObjectMother.TextSnippet,
                        label: null,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                        ),
                    ObjectMother.TextClassifierResult_AsStringWithNullLabel
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_03"),

            new TestCaseData(
                    new TextClassifierResult(
                            textSnippet: null,
                            label: null,
                            indexes: null,
                            indexAverages: null
                        ),
                    ObjectMother.TextClassifierResult_AllNulls
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_04")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(toStringTestCases))]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked(TextClassifierResult textClassifierResult, string expected)
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
                        textSnippet: TextSnippets.ObjectMother.TextSnippet,
                        label: LabeledExamples.ObjectMother.ShortLabeledExample01.Label,
                        indexes: Similarity.ObjectMother.SimilarityIndexes,
                        indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                    );

            // Assert
            Assert.IsInstanceOf<TextClassifierResult>(actual);

            Assert.IsInstanceOf<TextSnippet>(actual.TextSnippet);
            Assert.IsInstanceOf<string>(actual.Label);
            Assert.IsInstanceOf<List<SimilarityIndex>>(actual.SimilarityIndexes);
            Assert.IsInstanceOf<List<SimilarityIndexAverage>>(actual.SimilarityIndexAverages);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.11.2022
*/