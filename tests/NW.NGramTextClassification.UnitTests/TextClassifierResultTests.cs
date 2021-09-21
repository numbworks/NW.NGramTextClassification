﻿using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.Similarity;
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
                ).SetArgDisplayNames($"{nameof(toStringTestCases)}_02")

        };
        private static TestCaseData[] textClassifierResultExceptionTestCases =
        {

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierResult(
                                    ObjectMother.Shared_Text1_Label,
                                    null,
                                    ObjectMother.TextClassifierResult_SimilarityIndexAverages1
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierResult_VariableName_Indexes).Message
                ).SetArgDisplayNames($"{nameof(textClassifierResultExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierResult(
                                    ObjectMother.Shared_Text1_Label,
                                    new List<SimilarityIndex>(),
                                    ObjectMother.TextClassifierResult_SimilarityIndexAverages1
                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.TextClassifierResult_VariableName_Indexes)
                ).SetArgDisplayNames($"{nameof(textClassifierResultExceptionTestCases)}_02"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierResult(
                                    ObjectMother.Shared_Text1_Label,
                                    ObjectMother.TextClassifierResult_SimilarityIndexes1,
                                    null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierResult_VariableName_IndexAverages).Message
                ).SetArgDisplayNames($"{nameof(textClassifierResultExceptionTestCases)}_03"),
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierResult(
                                    ObjectMother.Shared_Text1_Label,
                                    ObjectMother.TextClassifierResult_SimilarityIndexes1,
                                    new List<SimilarityIndexAverage>()
                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.TextClassifierResult_VariableName_IndexAverages)
                ).SetArgDisplayNames($"{nameof(textClassifierResultExceptionTestCases)}_04")

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

        [TestCaseSource(nameof(textClassifierResultExceptionTestCases))]
        public void TextClassifierResult_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
    Last Update: 18.09.2021
*/