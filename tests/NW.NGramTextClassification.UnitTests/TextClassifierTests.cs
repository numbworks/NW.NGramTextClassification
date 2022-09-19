using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        #region Fields

        private static TestCaseData[] textClassifierExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        null,
                                        new TextClassifierSettings()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("components").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new TextClassifierComponents(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_02")

        };
        private static TestCaseData[] predictLabelOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: null,
                                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                                        labeledExamples: ObjectMother.Shared_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: ObjectMother.Shared_LabeledExample01.Text,
                                        tokenizerRuleSet: null,
                                        labeledExamples: ObjectMother.Shared_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: ObjectMother.Shared_LabeledExample01.Text,
                                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                                        labeledExamples: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] predictLabelOrDefaultTestCases =
        {

            new TestCaseData(
                    "/",
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateTokenizedExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultTestCases)}_01"),

            new TestCaseData(
                    "hi",
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true     
                        ),
                    ObjectMother.CreateTokenizedExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.CreateTokenizedExamples()[0].LabeledExample.Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateTokenizedExamples(),
                    ObjectMother.TextClassifier_TextClassifierResult_LabeledExamples00
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(predictLabelOrDefaultExceptionTestCases))]
        public void PredictLabel_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifier_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifier actual = new TextClassifier();

            // Assert
            Assert.IsInstanceOf<TextClassifier>(actual);

            Assert.IsInstanceOf<TextClassifierComponents>(TextClassifier.DefaultTextClassifierComponents);
            Assert.IsInstanceOf<TextClassifierSettings>(TextClassifier.DefaultTextClassifierSettings);
            Assert.IsInstanceOf<INGramTokenizerRuleSet>(TextClassifier.DefaultNGramTokenizerRuleSet);
            Assert.IsInstanceOf<TextClassifierResult>(TextClassifier.DefaultTextClassifierResult);

        }

        [TestCaseSource(nameof(predictLabelOrDefaultTestCases))]
        public void PredictLabelOrDefault_ShouldReturnTheExpectedTextClassifierResult_WhenProperArgument
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            Action<string> fakeLoggingAction = (message) => { };
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());
            string truncatedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected,actual)
                );

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