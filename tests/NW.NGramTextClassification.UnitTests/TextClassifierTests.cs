﻿using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
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
                new ArgumentNullException("tokenizerRuleSet").Message
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
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] predictLabelOrDefaultWhenAllRulesFailedTestCases =
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
                    ObjectMother.CreateThirtyLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases)}_01"),

            new TestCaseData(
                    "hi",
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateThirtyLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };
        private static TestCaseData[] predictLabelOrDefaultWhenUntokenizableExamples =
       {

            new TestCaseData(
                    ObjectMother.CreateThirtyLabeledExamples()[0].Text,
                    ObjectMother.Shared_RuleSet_Five,
                    ObjectMother.Shared_LabeledExamples_Untokenizable,
                    TextClassifier.DefaultTextClassifierResult
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenUntokenizableExamples)}_01")

        };
        private static TestCaseData[] predictLabelOrDefaultWhenOneLabeledExampleAndSuccessfulPrediction =
        {

            new TestCaseData(
                    ObjectMother.CreateThirtyLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateThirtyLabeledExamples().GetRange(0, 1),
                    ObjectMother.CreateThirtyLabeledExamples()[0].Label
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenOneLabeledExampleAndSuccessfulPrediction)}_01")

        };
        private static TestCaseData[] predictLabelOrDefaultWhenThirtyLabeledExamplesAndSuccessfulPrediction =
        {

            new TestCaseData(
                    ObjectMother.CreateThirtyLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateThirtyLabeledExamples(),
                    ObjectMother.TextClassifier_TextClassifierResult_LabeledExamples00
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenThirtyLabeledExamplesAndSuccessfulPrediction)}_01")

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


        [TestCaseSource(nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases))]
        public void PredictLabelOrDefault_ShouldReturnExpectedTextClassifierResult_WhenAllRulesFailed
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> expectedLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components);

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [TestCaseSource(nameof(predictLabelOrDefaultWhenUntokenizableExamples))]
        public void PredictLabelOrDefault_ShouldReturnDefaultClassifierResult_WhenUntokenizableExamples
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AtLeastOneLabeledExampleFailedTokenized

            };

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(
                    initialLogMessages,
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages,
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }


        [TestCaseSource(nameof(predictLabelOrDefaultWhenOneLabeledExampleAndSuccessfulPrediction))]
        public void PredictLabelOrDefault_ShouldReturnExpectedLabel_WhenOneLabeledExampleAndSuccessfulPrediction
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, string expectedLabel)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_PredictedLabelIs(expectedLabel),
                MessageCollection.TextClassifier_PredictionHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.AreEqual(expectedLabel, actual.Label);
            Assert.AreEqual(
                    initialLogMessages, 
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages, 
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }

        [TestCaseSource(nameof(predictLabelOrDefaultWhenThirtyLabeledExamplesAndSuccessfulPrediction))]
        public void PredictLabelOrDefault_ShouldReturnExpectedTextClassifierResult_WhenThirtyLabeledExamplesAndSuccessfulPrediction
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_PredictedLabelIs(expected.Label),
                MessageCollection.TextClassifier_PredictionHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(
                    initialLogMessages,
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages,
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }


        [Test]
        public void GetLabel_ShouldReturnNull_WhenContainsAtLeastOneNonZeroIndexAverageReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0),
                new SimilarityIndexAverage(label: "sv", value: 0)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_FollowingVerificationHasFailed("ContainsAtLeastOneNonZeroIndexAverage")

            };

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnNull_ContainsAtLeastOneDifferentIndexAverageReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0.1),
                new SimilarityIndexAverage(label: "sv", value: 0.1),
                new SimilarityIndexAverage(label: "dk", value: 0.1)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneNonZeroIndexAverage"),
                MessageCollection.TextClassifier_FollowingVerificationHasFailed("ContainsAtLeastOneDifferentIndexAverage")

            };

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnNull_ContainsTwoDifferentHighestIndexAveragesReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.98),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneNonZeroIndexAverage"),
                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneDifferentIndexAverage"),
                MessageCollection.TextClassifier_FollowingVerificationHasFailed("ContainsTwoDifferentHighestIndexAverages")

            };

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_OneSimilarityIndexAverageValue()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98)

            };

            string expected = "sv";
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneNonZeroIndexAverage"),
                MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs(new SimilarityIndexAverage(label: expected, value: 0.98))

            };

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_ThreeDifferentSimilarityIndexAverageValues()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = "sv";
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneNonZeroIndexAverage"),
                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsAtLeastOneDifferentIndexAverage"),
                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful("ContainsTwoDifferentHighestIndexAverages"),
                MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs(new SimilarityIndexAverage(label: expected, value: 0.98))

            };

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods

        private static List<string> CreateWhenAllRulesFailed
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierComponents components)
        {

            string expectedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            List<INGram> expectedNGrams = components.NGramsTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);

            List<string> expectedMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided(expectedText),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided(labeledExamples),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams(expectedNGrams),

                MessageCollection.TextClassifier_AllRulesInProvidedRulesetFailed(text)

            };

            return expectedMessages;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2022
*/