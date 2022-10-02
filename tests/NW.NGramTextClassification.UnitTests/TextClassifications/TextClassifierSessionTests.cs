using System;
using NW.NGramTextClassification.TextClassifications;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    [TestFixture]
    public class TextClassifierSessionTests
    {

        #region Fields

        private static TestCaseData[] textClassifierSessionExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierSession(
                                    settings: null,
                                    results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(textClassifierSessionExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierSession(
                                    settings: new TextClassifierSettings(),
                                    results: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("results").Message
                ).SetArgDisplayNames($"{nameof(textClassifierSessionExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierSessionExceptionTestCases))]
        public void TextClassifierSession_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifierSession_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierSession actual
                = new TextClassifierSession(
                        settings: new TextClassifierSettings(),
                        results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00
                    );

            // Assert
            Assert.IsInstanceOf<TextClassifierSession>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 02.10.2022
*/