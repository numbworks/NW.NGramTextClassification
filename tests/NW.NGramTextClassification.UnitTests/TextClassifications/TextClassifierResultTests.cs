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
            Assert.That(
                string.Equals(
                    expected,
                    actual,
                    StringComparison.InvariantCulture),
                Is.True);

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
            Assert.That(actual, Is.InstanceOf<TextClassifierResult>());

            Assert.That(actual.TextSnippet, Is.InstanceOf<TextSnippet>());
            Assert.That(actual.Label, Is.InstanceOf<string>());
            Assert.That(actual.SimilarityIndexes, Is.InstanceOf<List<SimilarityIndex>>());
            Assert.That(actual.SimilarityIndexAverages, Is.InstanceOf<List<SimilarityIndexAverage>>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 31.01.2024
*/