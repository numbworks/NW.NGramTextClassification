using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierComponentsTests
    {

        // Fields
        private static TestCaseData[] textClassifierComponentsExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        null,
                                        new SimilarityIndexCalculatorJaccard(),
                                        TextClassifierComponents.DefaultRoundingFunction,
                                        TextClassifierComponents.DefaultTextTruncatingFunction,
                                        TextClassifierComponents.DefaultLoggingAction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierComponents_VariableName_NGramsTokenizer).Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        new NGramTokenizer(),
                                        null,
                                        TextClassifierComponents.DefaultRoundingFunction,
                                        TextClassifierComponents.DefaultTextTruncatingFunction,
                                        TextClassifierComponents.DefaultLoggingAction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierComponents_VariableName_SimilarityIndexCalculator).Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_02"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        new NGramTokenizer(),
                                        new SimilarityIndexCalculatorJaccard(),
                                        null,
                                        TextClassifierComponents.DefaultTextTruncatingFunction,
                                        TextClassifierComponents.DefaultLoggingAction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierComponents_VariableName_RoundingFunction).Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_03"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        new NGramTokenizer(),
                                        new SimilarityIndexCalculatorJaccard(),
                                        TextClassifierComponents.DefaultRoundingFunction,
                                        null,
                                        TextClassifierComponents.DefaultLoggingAction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierComponents_VariableName_TextTruncatingFunction).Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_04"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        new NGramTokenizer(),
                                        new SimilarityIndexCalculatorJaccard(),
                                        TextClassifierComponents.DefaultRoundingFunction,
                                        TextClassifierComponents.DefaultTextTruncatingFunction,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifierComponents_VariableName_LoggingAction).Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_05")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(textClassifierComponentsExceptionTestCases))]
        public void TextClassifierComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifierComponents_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierComponents actual = new TextClassifierComponents();

            // Assert
            Assert.IsInstanceOf<TextClassifierComponents>(actual);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 26.01.2021

*/
